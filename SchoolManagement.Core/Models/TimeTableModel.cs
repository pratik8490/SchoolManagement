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
    public class TimeTableModel
    {
        public string SubjectName { get; set; }
        public int DayOfWeek { get; set; }
        public string StandardName { get; set; }
        public string ClassTypeName { get; set; }
        public string LectureName { get; set; }
        public string LogBook { get; set; }
        public string TeacherName { get; set; }

        public static Task<List<TimeTableModel>> ShowTimeTable(int standardID, int classTypeID)
        {
            var result = new TaskCompletionSource<List<TimeTableModel>>();
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

                        string requestUri = string.Format("api/ShowTimetable/{0}/{1}", standardID, classTypeID);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.TrySetResult(JsonConvert.DeserializeObject<List<TimeTableModel>>(content));
                        }
                        else
                        {
                            result.TrySetResult(new List<TimeTableModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.TrySetResult(new List<TimeTableModel>());
                }
            });
            return result.Task;
        }

        public static Task<List<TimeTableModel>> ShowTimeTable(int teacherID)
        {
            var result = new TaskCompletionSource<List<TimeTableModel>>();
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

                        string requestUri = string.Format("api/TimeTable/{0}", teacherID);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.TrySetResult(JsonConvert.DeserializeObject<List<TimeTableModel>>(content));
                        }
                        else
                        {
                            result.TrySetResult(new List<TimeTableModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.TrySetResult(new List<TimeTableModel>());
                }
            });
            return result.Task;
        }
    }
}