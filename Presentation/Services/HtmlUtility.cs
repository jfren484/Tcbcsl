using HtmlAgilityPack;
using System;
using System.IO;

namespace Tcbcsl.Presentation.Services
{
    public static class HtmlUtility
    {
        public static string ConvertToPlainText(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }

        private static void ConvertContentTo(HtmlNode node, TextWriter outText)
        {
            foreach (var subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText);
            }
        }

        private static void ConvertTo(HtmlNode node, TextWriter outText)
        {
            string html;
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    // don't output comments
                    break;

                case HtmlNodeType.Document:
                    ConvertContentTo(node, outText);
                    break;

                case HtmlNodeType.Text:
                    // script and style must not be output
                    var parentName = node.ParentNode.Name;
                    if (parentName == "script" || parentName == "style")
                        break;

                    // get text
                    html = ((HtmlTextNode)node).Text;

                    // is it in fact a special closing node output as text?
                    if (HtmlNode.IsOverlappedClosingElement(html))
                        break;

                    // check the text is meaningful and not a bunch of whitespaces
                    if (html.Trim().Length > 0)
                    {
                        outText.Write(HtmlEntity.DeEntitize(html));
                    }
                    break;

                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                        case "br":
                            outText.Write(Environment.NewLine);
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText);
                    }

                    switch (node.Name)
                    {
                        case "a":
                            if (node.Attributes["href"] != null)
                            {
                                outText.Write($" ({node.Attributes["href"].Value})");
                            }
                            break;
                    }
                    break;
            }
        }
    }
}