using api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Movies_Wpf_App
{
    public class GetWishlistFromApi
    {
        public static List<Wishlist> RetrieveSavedMoviesFromApi()
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString($"{ApiRoot.Apiroot()}/wishlist");

                List<Wishlist> wishlist = JsonConvert.DeserializeObject<List<Wishlist>>(json);

                return wishlist;
            }
        }
    }
}
