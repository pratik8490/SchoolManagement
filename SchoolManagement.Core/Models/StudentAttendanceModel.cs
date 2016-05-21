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
    public class StudentAttendanceModel
    {
        public int Id { get; set; }
        public int StandardId { get; set; }
        public int ClassTypeId { get; set; }
        public DateTime Date { get; set; }
        public List<AttendanceDetailModel> Students { get; set; }

        public static Task<StudentAttendanceModel> GetClassType(int standardId, int classTypeId, int dateCounter)
        {
            var result = new TaskCompletionSource<StudentAttendanceModel>();
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

                        string requestUri = string.Format("api/AttendanceDetail/{0}/{1}/{2}", standardId, classTypeId, dateCounter);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<StudentAttendanceModel>(content));
                        }
                        else
                        {
                            result.SetResult(new StudentAttendanceModel());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new StudentAttendanceModel());
                }
            });
            return result.Task;
        }
    }
    public class AttendanceDetailModel
    {
        public int Id { get; set; }
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int IsPresent { get; set; }
    }

    public class ViewAttendanceModel
    {
        public int Id { get; set; }
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int IsPresent { get; set; }
        public int RollNumber { get; set; }
        public string StudentName { get; set; }
        public int Gender { get; set; }
        public string Date { get; set; }
        public object StandardName { get; set; }
        public object ClassTypeName { get; set; }
        public int StandardId { get; set; }
        public int ClassTypeId { get; set; }
        public int GRNumber { get; set; }
        public string BirthDate { get; set; }
        public object PhoneOffice { get; set; }
        public object PhoneResidence { get; set; }
        public object MobileNumber { get; set; }
        public int Total { get; set; }
        public bool IsPastStudent { get; set; }
        public object LeaveDate { get; set; }

        public static Task<List<ViewAttendanceModel>> GetStudentAttendance(int startDate, int endDate)
        {
            var result = new TaskCompletionSource<List<ViewAttendanceModel>>();
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

                        string requestUri = string.Format("api/ViewAttendance/{0}/{1}", startDate, endDate);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<ViewAttendanceModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<ViewAttendanceModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<ViewAttendanceModel>());
                }
            });
            return result.Task;
        }
    }
}