using System;
using System.Net.Http;
using System.Net.Http.Headers;
using StarWarsApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace StarWarsApi.Repository
{
    public static class APICollector
    {
        public static User ParseUser(string URL)
        {
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(URL); 
                httpclient.DefaultRequestHeaders.Add("User-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(URL).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }
            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var parsedUser = JsonConvert.DeserializeObject<User>(jsonResult);
                var tempURL = JsonConvert.DeserializeObject<TempHolder>(jsonResult);
                parsedUser.StarWarsID = int.Parse(Regex.Replace(URL,@"[^\d]", string.Empty));
                return parsedUser;
            }
            else
            {
                throw new Exception("Parse was empty; Is the URL in correct format?");
            }
        }

        private class TempHolder
        {
            public string ConnectionString { get; set; }

            public TempHolder(string url)
            {
                ConnectionString = url;
            }
        }
    }
}