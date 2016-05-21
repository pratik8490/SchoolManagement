using Newtonsoft.Json;
using SchoolManagement.Core.Context;
using SchoolManagement.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SchoolManagement.Core.Models
{
    public class FillUpAttendanceModel
    {
        #region Property
        public int Id { get; set; }
        public int StandardId { get; set; }
        public int ClassTypeId { get; set; }
        public string Date { get; set; }
        public List<Student> Students { get; set; }

        #endregion

        public static Task<FillUpAttendanceModel> GetFillUpAttendance(int Standard, int Class, int DateCounter)
        {
            var result = new TaskCompletionSource<FillUpAttendanceModel>();
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

                        string requestUri = string.Format("api/AttendanceDetail/{0}/{1}/{2}", Standard, Class, DateCounter);

                        var response = await client.GetAsync(requestUri);

                        FillUpAttendanceModel model = new FillUpAttendanceModel();

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            if (content != "null")
                            {
                                result.SetResult(JsonConvert.DeserializeObject<FillUpAttendanceModel>(content));
                            }
                            else
                            {
                                result.SetResult(model);
                            }
                        }
                        else
                        {
                            result.SetResult(model);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new FillUpAttendanceModel());
                }
            });
            return result.Task;
        }

        public static Task<bool> SaveStudentAttendance(FillUpAttendanceModel fillUpAttendanceModel)
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

                        string requestUri = string.Format("api/StudentAttendance");

                        var response = await client.PostAsync(requestUri, fillUpAttendanceModel, new JsonMediaTypeFormatter());

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
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
    public class Student
    {
        public int Id { get; set; }
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int IsPresent { get; set; }
        public string StudentName { get; set; }
    }
}
