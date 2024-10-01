using System;
using System.Net.Http;
using System.Net.Http.Headers;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;


namespace QandA.WebScrape
{
    public class WebScrape
    {
      /*  public IActionResult Index()
        {
            // your code snippet from the post 

            return scrape(Address);

              //  return blah;
             // string return is mapped into the Task<string>
        }*/

        public static void RunScrape(string Address)
        {
           // var blah = await scrape(Address);
          //  return blah;
           // //return blah;
        }
        public static async Task<string> scrape(string address)
      // public static string scrape(string address)
        {
            //"https://realty-mole-property-api.p.rapidapi.com/properties?address=426%20West%2021st%20Street%2C%20Carroll%2C%20IA%2C%20USA"
            address = address.Replace(" ", "%20");
            string url = string.Format("https://realty-mole-property-api.p.rapidapi.com/properties?address={0}", address);
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
    {
        { "X-RapidAPI-Key", "3a1e9269ffmshbb17786a62c2e31p114672jsn84c1eab70686" },
        { "X-RapidAPI-Host", "realty-mole-property-api.p.rapidapi.com" },
    },
            };

            var response = client.SendAsync(request).Result;
            var body = await response.Content.ReadAsStringAsync();
            return body.ToString();
            

       //     return "[{\"addressLine1\":\"426 W 21st St\",\"city\":\"Carroll\",\"state\":\"IA\",\"zipCode\":\"51401\",\"formattedAddress\":\"426 W 21st St, Carroll, IA 51401\",\"bedrooms\":3,\"squareFootage\":1200,\"yearBuilt\":1969,\"features\":{\"cooling\":true,\"coolingType\":\"Commercial\",\"exteriorType\":\"Aluminum / Vinyl Siding\",\"floorCount\":1,\"foundationType\":\"Block\",\"garage\":true,\"garageType\":\"Attached\",\"heating\":true,\"roofType\":\"Asphalt\",\"roomCount\":5},\"county\":\"Carroll\",\"assessorID\":\"0613376010\",\"legalDescription\":\"BROMERT ADDITION BLOCK 2 W 70 FT OF LOT 4 & E 10 FT OF LOT 5\",\"subdivision\":\"BROMERT ADDITION\",\"ownerOccupied\":true,\"bathrooms\":1,\"lotSize\":8712,\"propertyType\":\"Single Family\",\"taxAssessment\":{\"2022\":{\"value\":135040,\"land\":23280,\"improvements\":111760}},\"propertyTaxes\":{\"2021\":{\"total\":1778}},\"owner\":{\"names\":[\"PAUL J WIELAND\"],\"mailingAddress\":{\"id\":\"426-W-21st-St,-Carroll,-IA-51401\",\"addressLine1\":\"426 W 21st St\",\"city\":\"Carroll\",\"state\":\"IA\",\"zipCode\":\"51401\"}},\"id\":\"426-W-21st-St,-Carroll,-IA-51401\",\"longitude\":-94.870195,\"latitude\":42.082069}]";
           // return response.Content.ToString();*/

          /*  using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                  var response = await client.GetStringAsync().ConfigureAwait(false);

                var body = await response.Content.ReadAsString();
               // File.Create("C:/")
                return body;
            }
           



            /*
            HtmlNode node = null;
            int attempts = 0;
            HtmlDocument htmlDoc = null;
            string html = "";
            var splitted = street.Split(' ', 4);


            while (node == null || attempts == splitted.Length)
            {
                var splitted = street.Split(' ', 4);
                //var html = string.Format("https://carroll.iowaassessors.com/results.php?mode=basic&history=-1&ipin=&idba=&ideed=&icont=&ihnum=817&iaddr=capistrano+ave&ilegal=&iacre1=&iacre2=&iphoto=0", number, street);
         
                 html = string.Format(@"https://carroll.iowaassessors.com/results.php?mode=basic&history=-1&ipin=&idba=&ideed=&icont=&ihnum=" + number + "&iaddr=" + splitted[attempts] + "&ilegal=&iacre1=&iacre2=&iphoto=0", number, street);
                
                HtmlWeb web = new HtmlWeb();

                htmlDoc = web.Load(html);
                //  var htmlDoc = web.Load("C:\\Users\\ServerAdmin\\Documents\\blah.html");
                //  htmlDoc.Save("C:\\Users\\ServerAdmin\\Documents\\blah.html");
                // var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");
                node = htmlDoc.DocumentNode.Descendants().Where(n => n.Id == "pclGeneralInfo").FirstOrDefault();
                attempts++;

            }

            if (node != null)
            {

                HtmlNode node1 = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("residentialData")).FirstOrDefault();
                HtmlNode node2 = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("land")).FirstOrDefault();


                //  Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);

                List<string> allHtmlNodes = new List<string>();

                allHtmlNodes.Add(node.InnerHtml);
                allHtmlNodes.Add(node1.InnerHtml);
                allHtmlNodes.Add(node2.InnerHtml);
                return allHtmlNodes;


            }
            else
            {
                return null;
            }


            //zillow.com/homes/426-W-21st-St-Carroll,-IA-51401_rb/118098941_zpid/
            //string URL = "https://www.zillow.com/homes/@Address_rb";
            //    string URL = "https://www.msn.com";
            //    string URL = "https://carroll.iowaassessors.com/results.php?mode=basic&history=-1&ipin=&idba=&ideed=&icont=&ihnum=817&iaddr=capistrano+ave&ilegal=&iacre1=&iacre2=&iphoto=0";
            // string URL = "https://carroll.iowaassessors.com/results.php?mode=basic&hnum=817&iaddr=CAPISTRANO+AV&history=-1&ipin=&idba=&ideed=&";
            //   URL = URL.Replace("@Address", Address);

            // return new QandA.Models.HouseModel();// { Address = "blah" };
            /* var url = new Uri(URL);
            // var httpClient = new HttpClient();

             try
             {
                 var httpClient = new HttpClient();
                 //  var uri = "http://some.api.url;
                 //var response = await httpClient.GetAsync(url);
                 //var result = Task.Run(() =>  httpClient.GetAsync(url).Result);

                 HttpClient c = new HttpClient();
                // Task<string> t = c.GetStringAsync(url);
                 string s = t.Result;
                 Console.WriteLine(s);

                 //Console.WriteLine(result);
                 HtmlDocument htmlDoc = new HtmlDocument();
                 htmlDoc.LoadHtml(s);
                 /*    if (response.IsSuccessStatusCode)
                     {

                         var content = await response.Content.ReadAsStringAsync();
                         httpClient.Dispose();
                         return content;
                     }*/

            // return null;




            //  var response = await httpClient.GetStringAsync(URL).ConfigureAwait(false);

            // var result = await httpClient.GetStringAsync(url);
            //string checkResult = result.ToString();
            //   httpClient.Dispose();
            //    return response;
            //}
            // catch (Exception ex)
            // {
            //     string checkResult = "Error " + ex.ToString();
            //  httpClient.Dispose();
            //     return checkResult;
            // }
            
        }
    }
}
