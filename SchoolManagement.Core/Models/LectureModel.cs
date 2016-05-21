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
    public class LectureModel
    {
        public int Id { get; set; }
        public string LectureName { get; set; }

        public static Task<List<LectureModel>> GetLecture()
        {
            var result = new TaskCompletionSource<List<LectureModel>>();
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

                        string requestUri = string.Format("api/Lecture");

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<LectureModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<LectureModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<LectureModel>());
                }
            });
            return result.Task;
        }
    }
}
