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
    public class GetUsersFromApi
    {      
       public static List<Users> RetrieveUsersFromApi()
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString($"{ApiRoot.Apiroot()}/users");

                List<Users> users = JsonConvert.DeserializeObject<List<Users>>(json);

                return users;
            }
        }
    }
}
