using api.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;

// Code below  4  Assignment 3 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

namespace Movies_Wpf_App
{
    public class PostUsers
    {
        public static string AddUsers(string fName, string lName, string password, string email, string uName)
        {
            Users user = new Users();
            user.Fname = fName;
            user.Lname = lName;
            user.Passwrd = password;
            user.Email = email;
            user.Uname = uName;

            string json = JsonConvert.SerializeObject(user);         

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{ApiRoot.Apiroot()}/users");
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