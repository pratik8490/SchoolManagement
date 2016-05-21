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
    public class ParentComplainModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int UserType { get; set; }
        public int UserId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }


        public static Task<List<ParentComplainModel>> GetComplainList()
        {
            var result = new TaskCompletionSource<List<ParentComplainModel>>();
            var token = "";
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

                        var requestUri = "api/StudentBehaviourNotice";
                        HttpResponseMessage response = await client.GetAsync(requestUri).ConfigureAwait(false);
                        List<ParentComplainModel> lstBatchModel = new List<ParentComplainModel>();

                        if (response.IsSuccessStatusCode)
                        {
                            var content = response.Content.ReadAsStringAsync().Result;

                            if (!string.IsNullOrEmpty(content))
                            {
                                lstBatchModel = JsonConvert.DeserializeObject<List<ParentComplainModel>>(content);
                            }

                            result.SetResult(lstBatchModel);
                        }
                        else
                        {
                            result.SetResult(new List<ParentComplainModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<ParentComplainModel>());
                }
            });

            return result.Task;
        }
    }
}
