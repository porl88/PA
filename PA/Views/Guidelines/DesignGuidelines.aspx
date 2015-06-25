
<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">

	<style type="text/css">
	/*<![CDATA[*/
		input[type="text"]:focus, input[type="password"]:focus, textarea:focus, select:focus
		{
			background-color: #FFFF00;
			border-color: #FF0000;
			border-style: solid;
		}
		
		.selection1::selection {
			background-color: #FE4902;
			color: #FFF;
		}

		.selection1::-moz-selection {
			background-color: #FE4902;
			color: #FFF;
		}

		.selection2::selection {
			background-color: Lime;
			font-weight: bold;
			color: #000;
		}

		.selection2::-moz-selection {
			background-color: Lime;
			font-weight: bold;
			color: #000;
		}

	/*]]>*/
	</style>
	
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="LeftColumn" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" Runat="Server">
<h1 class="standard">Website Design Guidelines</h1>
	<ul class="standard">
		<li><a href="/Reference/Design/PageLayout.aspx">Page Layout</a></li>
	</ul>
	<dl class="standard">
		<dt>Site Dimensions</dt>
		<dd>
			<ul>
				<li>Width: 1000px</li>
			</ul>
		</dd>
		<dt>Images</dt>
		<dd>
			<p>General rule: photographic images to be created as JPEG (.jpg), all other images to be created as a Portable Network Graphic (.png). Exception: if using transparency, use Graphics Interchange Format (.gif) instead of .png - early versions of Internet Explorer do not support transparency.</p>
			<p>Need a Photoshop (.psd) image for the following items so that new buttons can be created, the background color can be changed, etc.:</p>
			<ul>
				<li>all button images</li>
				<li>all icon images</li>
				<li>site logo</li>
			</ul>
		</dd>
		<dt>Default Font</dt>
		<dd>
			<ul>
				<li>Font Type - must use a web-safe font - a sans-serif font is preferable as it is more readable on a web page - Verdana is the most readable on a web page - <a href="/Reference/Design/WebSafeFonts.aspx">click here for more info on web-safe fonts</a></li>
				<li>Font Colour - must be high-contrast (e.g. black on a white background, white on a black background)</li>
				<li>Font Size</li>
			</ul>
		</dd>
		<dt>Fonts for Headers</dt>
		<dd>
			<ul>
				<li>Font Type - Can use any font, but must also supply a default font of an equivalent size from one of the web-safe fonts. This is because early browsers can only use fonts installed on the client's machine.</li>
				<li>can use 1 background image - must allow for header adjusting to different widths and heights</li>
				<li>can use <span style="font-variant:small-caps">Small Caps</span></li>
			</ul>
		</dd>
		<dt>Hyperlinks</dt>
		<dd>
			<ul>
				<li>Unvisited Style (the default colour)</li>
				<li>Visited Style</li>
				<li>Hover/Focus Style (hover is diplayed when the mouse is moved over, focus is displayed when you use the tab button to select the link)</li>
				<li>Active Style (displayed after the link has been clicked)</li>
			</ul>
		</dd>
		<dt>Selected Text Colour</dt>
		<dd>
			<p>Need a colour that is displayed when text is selected e.g. use your mouse to select the following examples:</p>
			<ul>
				<li><span class="selection1">Example 1</span></li>
				<li><span class="selection2">Example 2</span></li>
			</ul>
			<p>Can change the background colour and/or font colour.</p>
			<p>Does not work in Internet Explorer or older browsers.</p>
		</dd>
		<dt>Favicon Design</dt>
		<dd>
			<p>A Favicon is a small image that appears on the address bar and on the tab buttons of most browsers.</p>
			<p>To create a Favicon, simply create a 16x16 pixel bitmap image (.bmp), then change the extension to .ico</p>
		</dd>
		<dt>Designs for Standard Elements Used Throughout the Site</dt>
		<dd>
			<ul>
				<li>Tables
					<ul>
						<li>design for the following tables:
							<ul>
								<li>table with header row on top</li>
								<li>table with header row on left-hand side</li>
							</ul>
							<%--<table>
								<caption>Caption</caption>
								<thead><th>Header Cell</th></thead>
								<tbody><td>Body Cell</td></tbody>
								<tfoot><td>Footer Cell</td></tfoot>
							</table>--%>
						</li>
						<li>including styling for the following elements: &lt;Caption&gt;, &lt;th&gt;</li>
					</ul>
				</li>
				<li>Forms
					<ul>
						<li>including styling for the following elements: &lt;fieldset&gt;</li>
						<li>can have alternative styling (e.g. background colour/image, border) for input tags that have been selected e.g. select this input tag: <input type="text" /> - does not work with older browsers</li>
					</ul>
				</li>
				<li>Unordered Lists
					<ul>
						<li>can use images for bullet points</li>
						<li>or you can use a style - <a href="/Reference/XHTML/ListsUnordered.aspx">click here for more information on ordered lists</a></li>
						<li>can be nested, with different images/styles for each level of nesting</li>
					</ul>
				</li>
				<li>Ordered Lists
					<ul>
						<li><a href="/Reference/XHTML/ListsOrdered.aspx">click here for more information on ordered lists</a></li>
					</ul>
				</li>
			</ul>
		</dd>
	</dl>
	
</asp:Content>

