using Newtonsoft.Json;
using SchoolManagement.Core.Context;
using SchoolManagement.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace SchoolManagement.Core.Models
{
    public class TeacherLeaveModel
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string ReasonOfLeave { get; set; }
        public int Date { get; set; }
        public string Comment { get; set; }
        public int NoOfDays { get; set; }

        public static Task<List<TeacherLeaveModel>> GetTeacherLeave(int teacherID, int startDateCounter, int endDateCounter)
        {
            var result = new TaskCompletionSource<List<TeacherLeaveModel>>();
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

                        string requestUri = string.Format("api/ViewTeacherLeave/{0}/{1}/{2}", teacherID, startDateCounter, endDateCounter);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.TrySetResult(JsonConvert.DeserializeObject<List<TeacherLeaveModel>>(content));
                        }
                        else
                        {
                            result.TrySetResult(new List<TeacherLeaveModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.TrySetResult(new List<TeacherLeaveModel>());
                }
            });
            return result.Task;
        }

        public static Task<bool> ApplyLeave(TeacherLeaveModel teacherLeaveModel)
        {
            var result = new TaskCompletionSource<bool>();
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

                        string requestUri = string.Format("api/TeacherLeave");

                        var response = await client.PostAsync(requestUri, teacherLeaveModel, new JsonMediaTypeFormatter());

                        if (response.IsSuccessStatusCode)
                        {
                            result.SetResult(true);
                        }
                        else
                        {
                            result.SetResult(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(false);
                }
            });
            return result.Task;
        }
    }
}