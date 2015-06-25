namespace PA.Services.Crawler
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using PA.Services.Crawler.Transfer;
	
	public interface ICrawlerService
    {
		Task<List<PageLinkDto>> CheckPageLinks();
    }
}
