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
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int UserType { get; set; }
        public int UserId { get; set; }
        public int CreatedDate { get; set; }
        public int ScheduledDate { get; set; }

        public static Task<List<NotificationModel>> GetNotification()
        {
            var result = new TaskCompletionSource<List<NotificationModel>>();
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

                        string requestUri = string.Format("api/ViewNotification");

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<NotificationModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<NotificationModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<NotificationModel>());
                }
            });
            return result.Task;
        }
    }
}
