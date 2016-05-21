using Newtonsoft.Json;
using SchoolManagement.Core.Context;
using SchoolManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Core.Models
{
    public class LoginModel
    {
        #region Propertry
        /// <summary>
        /// Username 
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string password { get; set; }

        #endregion
        public static Task<UserToken> LogIn(string username, string password)
        {
            LoginModel login = new LoginModel { username = username, password = password };
            var result = new TaskCompletionSource<UserToken>();
            var token = "";

            var json = JsonConvert.SerializeObject(login);
            var loginContent = new StringContent(json, Encoding.UTF8, "application/json");

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Constants.ServiceUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        if (SchoolManagementContext.TokenResponseModel != null)
                        {
                            token = SchoolManagementContext.TokenResponseModel.Token;
                        }

                        if (string.IsNullOrEmpty(token))
                        {
                            client.DefaultRequestHeaders.Add("Login-Token", string.Format("{0}-login", "1"));
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("Authorization-Token", token);
                        }

                        var requestUri = "api/Authenticate/AuthorizationToken";
                        HttpResponseMessage response = await client.PostAsync(requestUri, loginContent).ConfigureAwait(false);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = response.Content.ReadAsStringAsync().Result;
                            UserToken objTokenResponseModel = new UserToken();

                            if (!string.IsNullOrEmpty(content))
                            {
                                objTokenResponseModel = JsonConvert.DeserializeObject<UserToken>(content);
                            }

                            result.SetResult(objTokenResponseModel);
                        }
                        else
                        {
                            result.SetResult(new UserToken());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new UserToken());
                }
            });
            return result.Task;
        }
    }
}
