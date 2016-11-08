namespace PA.Services.Crawler
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Transfer;

    public class CrawlerService : ICrawlerService
    {
        private readonly Uri domainName;

        private Queue<string> uncheckedValidLinks; // queue of absolute URIs

        private HashSet<string> checkedLinks;

        private List<PageLinkDto> PageLinks;

        public CrawlerService(Uri domainName)
        {
            this.domainName = domainName;
            this.uncheckedValidLinks = new Queue<string>();
            this.checkedLinks = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            this.PageLinks = new List<PageLinkDto>();
        }

        // TO DO:
        // Make sure use of Uri's is consistent

        public async Task<List<PageLinkDto>> CheckPageLinks()
        {
            string currentPageLink;
            this.uncheckedValidLinks.Enqueue(string.Concat(this.domainName.Scheme, "://", this.domainName.Host));

            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:49.0) Gecko/20100101 Firefox/49.0");

                do
                {
                    // get next unchecked link from queue
                    currentPageLink = this.uncheckedValidLinks.Dequeue();

                    try
                    {
                        // get the HTML from the URL
                        var pageHtml = await client.GetStringAsync(currentPageLink);

                        // add current url to checked list
                        this.checkedLinks.Add(currentPageLink);

                        // get all the unique link URLs that are in the HTML
                        var pageLinks = this.GetPageLinks(pageHtml);

                        // remove links already checked
                        pageLinks.ExceptWith(this.checkedLinks);

                        if (pageLinks.Count == 0)
                        {
                            continue;
                        }

                        // get the status for each link and add to the list of checked links
                        await this.CheckPageLinks(client, pageLinks, currentPageLink);
                    }
                    catch (HttpRequestException ex)
                    {
                        this.PageLinks.Add(new PageLinkDto
                        {
                            Link = new Uri(currentPageLink),
                            PageUrl = new Uri(currentPageLink),
                            StatusMessage = ex.Message
                        });
                    }
                    catch
                    {
                        // log this
                        throw;
                    }
                }

                while (this.uncheckedValidLinks.Count > 0);
            }

            return this.PageLinks;
        }

        private void AddLinkResponsesToLists(string currentPageLink, HttpResponseMessage response)
        {
            try
            {
                var requestedUri = response.RequestMessage.RequestUri;
                var contentType = response.Content.Headers.ContentType.MediaType;

                // add pages that contain HTML (excluding pages for external sites) to the list of pages that need their links checked
                if (response.IsSuccessStatusCode && contentType == "text/html" && requestedUri.Host.Equals(this.domainName.Host))
                {
                    this.uncheckedValidLinks.Enqueue(requestedUri.AbsoluteUri);
                }

                this.PageLinks.Add(new PageLinkDto
                {
                    Link = response.RequestMessage.RequestUri,
                    PageUrl = new Uri(currentPageLink),
                    StatusCode = (int)response.StatusCode,
                    StatusMessage = response.ReasonPhrase,
                    ContentType = contentType
                });
            }
            catch (Exception ex)
            {
                this.PageLinks.Add(new PageLinkDto
                {
                    Link = response.RequestMessage.RequestUri,
                    PageUrl = new Uri(currentPageLink),
                    StatusCode = (int)response.StatusCode,
                    StatusMessage = string.Format("Response StatusText: {0}, Exception: {1}", response.ReasonPhrase, ex.Message),
                });
            }
        }

        private HashSet<string> GetPageLinks(string pageHtml)
        {
            var pageLinks = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var pattern = "(href|src)=\"(.*?)\"";
            var matches = Regex.Matches(pageHtml, pattern, RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                var link = match.Groups[2].Value;

                if (!string.IsNullOrWhiteSpace(link))
                {
                    Uri uri;
                    if (Uri.TryCreate(this.domainName, link, out uri) || Uri.TryCreate(link, UriKind.Absolute, out uri))
                    {
                        pageLinks.Add(uri.AbsoluteUri);
                    }
                    else
                    {
                        this.PageLinks.Add(new PageLinkDto
                        {
                            StatusCode = 400,
                            StatusMessage = string.Format("Link is not a well-formed Uri: " + link),
                        });
                    }
                }
            }

            return pageLinks;
        }

        private async Task CheckPageLinks(HttpClient client, HashSet<string> pageLinks, string currentPageLink)
        {
            var tasks = this.GetHeadRequestsAsListOfTasks(client, pageLinks, currentPageLink);

            var linkResponses = Task.WhenAll(tasks);

            try
            {
                await linkResponses;
                tasks.ForEach(t => this.AddLinkResponsesToLists(currentPageLink, t.Result));
            }
            catch
            {
                linkResponses.Exception.Handle(exception =>
                {
                    this.PageLinks.Add(new PageLinkDto
                    {
                        //Link = response.RequestMessage.RequestUri,
                        PageUrl = new Uri(currentPageLink),
                        StatusCode = 500,
                        StatusMessage = string.Concat("Exception: ", exception.Message)
                    });

                    return true;
                });
            }

            // now that all pageLinks have been checked, add to list of checked links
            this.checkedLinks.UnionWith(pageLinks);
        }

        private List<Task<HttpResponseMessage>> GetHeadRequestsAsListOfTasks(HttpClient client, HashSet<string> pageLinks, string currentPageLink)
        {
            // get response headers to check if links are valid
            var tasks = new List<Task<HttpResponseMessage>>();

            // check response headers
            foreach (var link in pageLinks)
            {
                try
                {
                    tasks.Add(client.SendAsync(new HttpRequestMessage
                    {
                        Method = HttpMethod.Head,
                        RequestUri = new Uri(link)
                    }));
                }
                catch (Exception ex)
                {
                    this.PageLinks.Add(new PageLinkDto
                    {
                        Link = new Uri(link),
                        PageUrl = new Uri(currentPageLink),
                        StatusMessage = ex.Message
                    });
                }
            }

            return tasks;
        }
    }
}
