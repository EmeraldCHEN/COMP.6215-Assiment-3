using api.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Movies_Wpf_App
{
    public class PostWishlist
    {
        public static string SaveMovies(int peopleId, int movieId)
        {
            Wishlist saveMovie = new Wishlist();
            saveMovie.Pid = peopleId;
            saveMovie.Mid = movieId;

            string json = JsonConvert.SerializeObject(saveMovie);

            // MessageBox.Show($"{json}");

            // System.Diagnostics.Debug.WriteLine($"**************** --- {json}");

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{ApiRoot.Apiroot()}/wishlist"); // POST -> NO need {id} !!!!!!!!!!!!!!!!!!!!!!!
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
