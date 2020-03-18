using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using CsvHelper;

namespace novizinhotem_cli
{

    class Resposta
    {
        public string  Timestamp {get;set;}
        public  string Lugar{get;set;}
        public string Produto{get;set;}
        public string Quantidade{get;set;}
    }
    class Program
    {

        const string URL_GOOGLE_SHEET = "https://docs.google.com/spreadsheets/d/e/2PACX-1vTOlBswce8pvSFsabVjkU9aSFUs1YZ_9aUc1Nc1QCL-YmjlqNJZQuHp7skh53Qx-OuCtuekiViBF_rq/pub?gid=1736174841&single=true&output=csv";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Vizinho...");

        using (var reader = GetURLContents(URL_GOOGLE_SHEET))
            {
                var ativos = new List<Resposta>();
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.GetCultureInfo("en-US"));
                config.BufferSize = 1024 * 8;
                config.Delimiter = ",";

                {
                    using (var csv = new CsvReader(reader, config))
                    {
                        var respostas = csv.GetRecords<Resposta>();
                        foreach (var item in respostas)
                        {
                            Console.WriteLine($"    TimeStamp: {item.Timestamp}, {item.Lugar}, {item.Produto}, {item.Quantidade}");    
                        } 
                        
                                                
                    }

                }
                
            }


        }


         static StringReader GetURLContents(string url)
        {
            WebClientEx wc = new WebClientEx(new CookieContainer());
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
            wc.Headers.Add("DNT", "1");
            wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            wc.Headers.Add("Accept-Encoding", "deflate");
            wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            var uri = new Uri(url);
            var outputCSVdata = wc.DownloadString(url);
            return new StringReader(outputCSVdata);
        }       

    }
}
