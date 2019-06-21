using api.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Movies_Wpf_App
{
    public class PutUsers
    {
        public static string UpdateUsers(int id, string fName, string lName, string password, string email, string uName)
        {
            Users user = new Users();
            user.Id = id;
            user.Fname = fName;
            user.Lname = lName;
            user.Passwrd = password;
            user.Email = email;
            user.Uname = uName;

            string json = JsonConvert.SerializeObject(user);

            //MessageBox.Show($"{json}");

            //System.Diagnostics.Debug.WriteLine($"**************** --- {json}");

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{ApiRoot.Apiroot()}/users/{id}");
            req.Method = "PUT";
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