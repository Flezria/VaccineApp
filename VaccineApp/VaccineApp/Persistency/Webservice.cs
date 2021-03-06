﻿using System;
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
using System.Collections.ObjectModel;

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
                    bool EmailCheckedBool = (bool)EmailCheckDeserialize;

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
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
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

        #region Child services
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
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
                Debug.WriteLine(e);
            }

            return false;
        }

        public async Task<bool> DeleteChild(int childId, String api_key)
        {
            
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();

            try
            {
                var result = await client.DeleteAsync($"DeleteChild/{childId}/{api_key}");

                if (result.IsSuccessStatusCode)
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
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
                Debug.WriteLine(e);
            }
            return false;
        }

        public async Task<ObservableCollection<UserChilds>> GetChild(String api_key)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var result = client.GetAsync($"GetChilds/{api_key}").Result;

                if(result.Content != null)
                {
                    var ChildListAsString = await result.Content.ReadAsStringAsync();
                    var ChildListDeserialize = JsonConvert.DeserializeObject<ObservableCollection<UserChilds>>(ChildListAsString);
                    ObservableCollection<UserChilds> ChildList = ChildListDeserialize;

                    return ChildList;
                }
                

            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
                Debug.WriteLine(e);
            }

            return null;
        }

        public async Task<bool> CheckForChild(String api_key)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var result = client.GetAsync($"ChildBool/{api_key}").Result;

                if (result.Content != null)
                {
                    var ChildExist = await result.Content.ReadAsStringAsync();
                    var ChildExistDeserialize = JsonConvert.DeserializeObject(ChildExist);

                    return (bool)ChildExistDeserialize;
                }


            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
                Debug.WriteLine(e);
            }

            return false;
        }
        #endregion

        #region Vaccine Services

        public async Task<List<Vaccinations>> GetVacProgram(string api_key, int program_id, int child_id)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var result = client.GetAsync($"GetVacProgram/{api_key}/{program_id}/{child_id}").Result;

                if (result.IsSuccessStatusCode)
                {
                    var VacListAsString = await result.Content.ReadAsStringAsync();
                    var VacListDeserialize = JsonConvert.DeserializeObject<List<Vaccinations>>(VacListAsString);
                    List<Vaccinations> VacList = VacListDeserialize;

                    return VacList;
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
                Debug.WriteLine(e);
            }

            return null;
        }

        public async Task<List<Vaccinations>> GetVacHistorik(string api_key, int program_id, int child_id)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var result = client.GetAsync($"GetVacHistorik/{api_key}/{program_id}/{child_id}").Result;

                if (result.IsSuccessStatusCode)
                {
                    var VacHistorikAsString = await result.Content.ReadAsStringAsync();
                    var VacHistorikDeserialize = JsonConvert.DeserializeObject<List<Vaccinations>>(VacHistorikAsString);
                    List<Vaccinations> VacHistorik = VacHistorikDeserialize;

                    return VacHistorik;
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
                Debug.WriteLine(e);
            }

            return null;
        }

        public async Task<VaccineInfo> GetVacInfo(string api_key, int VacInfo_ID)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var result = client.GetAsync($"GetVacInfo/{api_key}/{VacInfo_ID}").Result;

                if (result.IsSuccessStatusCode)
                {
                    var VacInfoAsString = await result.Content.ReadAsStringAsync();
                    var VacInfoDeserialize = JsonConvert.DeserializeObject<VaccineInfo>(VacInfoAsString);
                    VaccineInfo VacInfo = VacInfoDeserialize;

                    return VacInfo;
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

        #region Vaccination_check Services

        public async Task<bool> UpdateVacAsDone(String api_key, int vac_id, int child_id)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var result = client.GetAsync($"VacDone/{api_key}/{vac_id}/{child_id}").Result;

                if (result.IsSuccessStatusCode)
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
                await App.Current.MainPage.DisplayAlert("Internet error", "Kan ikke forbinde til internettet", "OK");
                Debug.WriteLine(e);
            }

            return false;

        }

        #endregion
    }
}


