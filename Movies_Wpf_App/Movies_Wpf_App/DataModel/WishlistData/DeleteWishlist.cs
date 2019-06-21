using api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Movies_Wpf_App
{
    public class DeleteWishlist
    {
        public static string DeleteSavedMovie(int id, int peopleId, int movieId)
        {
                Wishlist deletedMovie = new Wishlist();
                deletedMovie.Id = id;
                deletedMovie.Pid = peopleId;
                deletedMovie.Mid = movieId;

                string json = JsonConvert.SerializeObject(deletedMovie);

                // MessageBox.Show($"{json}");

                // System.Diagnostics.Debug.WriteLine($"**************** --- {json}");

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{ApiRoot.Apiroot()}/wishlist/{id}"); 
                req.Method = "DELETE";
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
