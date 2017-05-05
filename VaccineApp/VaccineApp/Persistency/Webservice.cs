using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using VaccineApp.Model;
using System.Net.Http.Formatting;
using System.Diagnostics;
using Newtonsoft.Json;

namespace VaccineApp.Persistency
{
    class Webservice
    {
        HttpClient client = new HttpClient();
        const String ServerUrl = "http://vaccappwebservice20170424033719.azurewebsites.net/";

        #region User services
        public async Task<bool> CheckIfEmailIsTaken(String email)
        {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var result = client.GetAsync($"EmailCheck/{email}/").Result;

                if (result.IsSuccessStatusCode)
                {
                    var EmailChecked = await result.Content.ReadAsStringAsync();
                    var EmailCheckDeserialize = JsonConvert.DeserializeObject(EmailChecked);
                    var EmailCheckedBool = (bool)EmailCheckDeserialize;

                    switch (EmailCheckedBool)
                    {
                        case true:
                            return true;
                        case false:
                            return false;
                    }
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
                Debug.WriteLine(e);
            }

            return false;
        }

        public async Task<bool> AddUser(Users user)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();

            try
            {
                String JsonUser = JsonConvert.SerializeObject(user);
                var content = new StringContent(JsonUser, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/users", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return false;
        }

        public async Task<String> Login(String email, String password)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                var result = client.GetAsync($"Login/{email}/{password}").Result;

                if (result.IsSuccessStatusCode)
                {
                    var LoginCheck = await result.Content.ReadAsStringAsync();
                    var LoginCheckDeserialize = JsonConvert.DeserializeObject(LoginCheck);
                    var LoginCheckString = (string)LoginCheckDeserialize;

                    if(LoginCheckString != null)
                    {
                        return LoginCheckString;
                    }
                    else
                    {
                        return "false";
                    }
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
                Debug.WriteLine(e);
            }

            return null;
        }

        #endregion

        public async Task<bool> AddChild(UserChilds child, string api_key)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();           

            try
            {
                String JsonChild = JsonConvert.SerializeObject(child);
                var content = new StringContent(JsonChild, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"postchild/{api_key}", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return false;
        }

    }
}


