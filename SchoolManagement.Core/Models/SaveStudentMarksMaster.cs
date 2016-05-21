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
    public class SaveStudentMarksMaster
    {
        public int SubjectId { get; set; }
        public int StandardExamMasterId { get; set; }
        public int ExamScheduleId { get; set; }
        public List<SaveStudentMarksDetail> Students { get; set; }

        public static Task<bool> SaveStudentMarks(SaveStudentMarksMaster saveStudentMarksMaster)
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

                        var response = await client.PostAsync(requestUri, saveStudentMarksMaster, new JsonMediaTypeFormatter());

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

    public class SaveStudentMarksDetail
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TheoryMarks { get; set; }
        public int PracticalMarks { get; set; }
        public int OMRMarks { get; set; }
    }
}