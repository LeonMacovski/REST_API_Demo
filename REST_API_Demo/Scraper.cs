using HtmlAgilityPack;
using REST_API_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Demo
{
    public class Scraper
    {
        public static List<Movie> Scrape(string url)
        {
            List<Movie> movies = new List<Movie>();

            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("/html/body/main/div/article/ol/li[position()]");

            Console.WriteLine(nodes.Count);
            foreach (var node in nodes)
            {
                var Title = node.SelectSingleNode("header/h1").InnerText;
                Title = Title.Replace("&#039;", "'");
                var Director = node.SelectSingleNode("div/p[1]").InnerText;
                Director = Director.Replace("Director: ", string.Empty);
                Director = Director.Replace("Director:", string.Empty);
                Director = Director.Replace("Directors: ", string.Empty);
                Director = Director.Replace("Directors:", string.Empty);
                Director = Director.Replace("&nbsp;", " ");
                Director = Director.Trim();
                var Description = node.SelectSingleNode("div/p[5]").InnerText;
                Description = Description.Replace("&#039;", "'");
                Description = Description.Replace("&#39;", "'");
                Description = Description.Replace("&nbsp;", " ");

                Random rnd = new Random();

                Movie movie = new Movie
                {
                    Title = Title,
                    Director = Director,
                    Description = Description,
                    Id = Guid.NewGuid(),
                    Quantity = rnd.Next(3, 8),
                    Price = rnd.Next(20, 50)
                };

                movies.Add(movie);
            }

            return movies;
        }
    }
}
