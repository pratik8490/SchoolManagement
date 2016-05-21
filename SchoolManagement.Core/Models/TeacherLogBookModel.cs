using Newtonsoft.Json;
using SchoolManagement.Core.Context;
using SchoolManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace SchoolManagement.Core.Models
{
    public class TeacherLogBookModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int AcademicYearId { get; set; }
        public int Date { get; set; }
        public int TeacherId { get; set; }
        //public int StandardId { get; set; }
        //public int ClassTypeId { get; set; }
        public string LessonPlan { get; set; }
        public string TeachingAids { get; set; }
        public string HomeWork { get; set; }
        public string Comment { get; set; }
        public int LectureId { get; set; }
        //public string FileUploadLink { get; set; }
        //public string FileUpload { get; set; }
        public int IsApproved { get; set; }
        public string Remark { get; set; }

        public static Task<List<TeacherLogBookModel>> GetHomeWorkModel(int DateCounter, int lectureID)
        {
            var result = new TaskCompletionSource<List<TeacherLogBookModel>>();
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

                        string requestUri = string.Format("api/ViewHomeWork/{0}/{1}", DateCounter, lectureID);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            if (content != "null")
                            {
                                result.SetResult(JsonConvert.DeserializeObject<List<TeacherLogBookModel>>(content));
                            }
                            else
                            {
                                result.SetResult(new List<TeacherLogBookModel>());
                            }
                        }
                        else
                        {
                            result.SetResult(new List<TeacherLogBookModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<TeacherLogBookModel>());
                }
            });
            return result.Task;
        }

        public static Task<bool> SaveLogBook(TeacherLogBookModel teacherLogBookModel)
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

                        string requestUri = string.Format("api/TeacherLogBook");

                        var response = await client.PostAsync(requestUri, teacherLogBookModel, new JsonMediaTypeFormatter());

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

    public class HomeWorkModel
    {
        public int Date { get; set; }
        public string Comment { get; set; }
        public int LectureID { get; set; }

        public static Task<bool> SaveHomeWork(HomeWorkModel homeWorkModel)
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

                        string requestUri = string.Format("api/HomeWork");

                        var response = await client.PostAsync(requestUri, homeWorkModel, new JsonMediaTypeFormatter());

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
}
