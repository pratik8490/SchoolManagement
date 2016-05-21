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
    public class ExamScheduleModel
    {
        public int Id { get; set; }
        public int StandardId { get; set; }
        public int StandardExamMasterId { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan ExamTime { get; set; }
        public int TotalMarks { get; set; }
        public int PassingMark { get; set; }
        public int PaperType { get; set; }
        public int AcademicYearId { get; set; }
        public int OMRMarks { get; set; }
        public string StandardExamMasterName { get; set; }
        public string SubjectName { get; set; }
        public int ExamTypeId { get; set; }
        public string ExamTypeName { get; set; }

        public static Task<List<ExamScheduleModel>> GetExamTimeTable(int startDate, int endDate, int examTypeID, int stdExamMasterID)
        {
            var result = new TaskCompletionSource<List<ExamScheduleModel>>();
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

                        string requestUri = string.Format("api/ExamTimeTable/{0}/{1}/{2}/{3}", startDate, endDate, examTypeID, stdExamMasterID);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<ExamScheduleModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<ExamScheduleModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<ExamScheduleModel>());
                }
            });
            return result.Task;
        }
    }
}