using api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movies_Wpf_App
{
    public class GetMoviesFromApi
    {
        public static List<Movies> RetrieveMoviesFromApi()
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString($"{ApiRoot.Apiroot()}/movies");

                List<Movies> movies = JsonConvert.DeserializeObject<List<Movies>>(json);

                return movies;
            }
        }
    }
}
