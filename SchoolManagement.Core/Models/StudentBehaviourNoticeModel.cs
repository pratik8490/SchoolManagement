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
    public class StudentBehaviourNoticeModel
    {
        public int Id { get; set; }
        public int StandardId { get; set; }
        public int ClassTypeId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public string Comment { get; set; }

        public static Task<bool> SaveStudentBehaviour(StudentBehaviourNoticeModel studentBehaviourNoticeModel)
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

                        string requestUri = string.Format("api/StudentBehaviourNotice");

                        var response = await client.PostAsync(requestUri, studentBehaviourNoticeModel, new JsonMediaTypeFormatter());

                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
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


    public class StudentBehaviourNoticeDetailModel
    {
        public int Id { get; set; }
        public int StudentBehaviourNoticeId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }



}