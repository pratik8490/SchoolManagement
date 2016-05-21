using Newtonsoft.Json;
using SchoolManagement.Core.Context;
using SchoolManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace SchoolManagement.Core.Models
{
    public class ClassTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Task<List<ClassTypeModel>> GetClassType(int standardId)
        {
            var result = new TaskCompletionSource<List<ClassTypeModel>>();
            var token = "";

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Constants.ServiceUrl);

                        if (SchoolManagementContext.TokenResponseModel != null)
                        {
                            token = SchoolManagementContext.TokenResponseModel.Token;
                        }
                        if (string.IsNullOrEmpty(token))
                        {
                            client.DefaultRequestHeaders.Add("Login-Token", string.Format("{0}-login", DateTime.Now.ToString("d")));
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("Authorization-Token", token);
                        }

                        string requestUri = string.Format("api/ClassType/{0}", standardId);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.TrySetResult(JsonConvert.DeserializeObject<List<ClassTypeModel>>(content));
                        }
                        else
                        {
                            result.TrySetResult(new List<ClassTypeModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.TrySetResult(new List<ClassTypeModel>());
                }
            });
            return result.Task;
        }
    }
}