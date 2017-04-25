using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace VaccineApp.Persistency
{
    class Webservice
    {
        HttpClient client = new HttpClient();
        const String ServerUrl = "http://vaccappwebservice20170424033719.azurewebsites.net";

        
        public async Task<bool> CheckIfEmailIsTaken(String email)
        {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = client.GetAsync($"EmailCheck/{email}").Result;

                if(result.IsSuccessStatusCode)
                {
                    var EmailChecked = await result.Content.ReadAsStringAsync();
                    switch (EmailChecked)
                    {
                        case "true":
                            return true;
                        case "false":
                            return false;
                    }
                }
            return false;
        }

    }
}


