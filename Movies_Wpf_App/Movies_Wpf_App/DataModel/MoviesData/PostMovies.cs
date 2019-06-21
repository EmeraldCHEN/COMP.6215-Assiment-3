using api.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Movies_Wpf_App
{
    public class PostMovies
    {
        public static string AddMovies(string title, string summary, string picture, string genre, double rating)
        {
            Movies movie = new Movies();
            movie.Title = title;
            movie.Summary = summary;
            movie.Picture = picture;
            movie.Genre = genre;
            movie.Rating = rating;

            string json = JsonConvert.SerializeObject(movie);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{ApiRoot.Apiroot()}/movies");
            req.Method = "POST";
            req.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            };

            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
