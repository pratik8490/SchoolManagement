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
    public class StudentMarksDetailModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int StandardExamMasterId { get; set; }
        public int ExamScheduleId { get; set; }
        public DateTime ExamDate { get; set; }
        public int TheoryMarks { get; set; }
        public int PracticalMarks { get; set; }
        public int AcademicYearId { get; set; }
        public int IsPass { get; set; }
        public int OMRMarks { get; set; }
        public string StudentName { get; set; }
        public int StudentRollNumber { get; set; }
        public int PassingMark { get; set; }
        public int? PaperType { get; set; }
        public string StandardName { get; set; }
        public int IsPresent { get; set; }
        public string SubjectName { get; set; }
        public int TotalMarks { get; set; }
        public int TotalObtainedMarks { get; set; }

        public static Task<List<StudentMarksDetailModel>> ViewResult(int stdExamMasterID)
        {
            var result = new TaskCompletionSource<List<StudentMarksDetailModel>>();
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

                        string requestUri = string.Format("api/ViewResult/{0}", stdExamMasterID);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<StudentMarksDetailModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<StudentMarksDetailModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<StudentMarksDetailModel>());
                }
            });
            return result.Task;
        }

        public static Task<List<StudentMarksDetailModel>> StudentExamMarkList(int standardID, int examTypeID, int examID)
        {
            var result = new TaskCompletionSource<List<StudentMarksDetailModel>>();
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

                        string requestUri = string.Format("api/ViewStudentMarks/{0}/{1}/{2}", standardID, examTypeID, examID);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<StudentMarksDetailModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<StudentMarksDetailModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<StudentMarksDetailModel>());
                }
            });
            return result.Task;
        }
    }
}