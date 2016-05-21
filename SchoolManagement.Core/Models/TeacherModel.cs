using Newtonsoft.Json;
using SchoolManagement.Core.Context;
using SchoolManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Core.Models
{
    public class TeacherModel
    {
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        #endregion
        public static Task<List<TeacherModel>> GetTeacher()
        {
            var result = new TaskCompletionSource<List<TeacherModel>>();
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

                        string requestUri = string.Format("api/Teacher");

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.TrySetResult(JsonConvert.DeserializeObject<List<TeacherModel>>(content));
                        }
                        else
                        {
                            result.TrySetResult(new List<TeacherModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.TrySetResult(new List<TeacherModel>());
                }
            });
            return result.Task;
        }

    }
}
