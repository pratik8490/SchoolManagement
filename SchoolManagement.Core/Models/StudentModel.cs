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
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Task<List<StudentModel>> GetStudent(int standardID, int classTypeID)
        {
            var result = new TaskCompletionSource<List<StudentModel>>();
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

                        string requestUri = string.Format("api/Students/{0}/{1}", standardID, classTypeID);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<StudentModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<StudentModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<StudentModel>());
                }
            });
            return result.Task;
        }
    }

    public class StudentHeadCountModel
    {
        public int StandardId { get; set; }
        public int ClassTypeId { get; set; }
        public string ClassTypeName { get; set; }
        public int Total { get; set; }
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }
        public List<StudentSummary> StudentSummary { get; set; }

        public static Task<List<StudentHeadCountModel>> GetStudentHeadCount(int standardID)
        {
            var result = new TaskCompletionSource<List<StudentHeadCountModel>>();
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

                        string requestUri = string.Format("api/StudentHeadCount/{0}", standardID);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<StudentHeadCountModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<StudentHeadCountModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<StudentHeadCountModel>());
                }
            });
            return result.Task;
        }
    }

    public class StudentSummary
    {
        public int Category { get; set; }
        public string CategoryName { get; set; }
        public int Total { get; set; }
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }
    }
}