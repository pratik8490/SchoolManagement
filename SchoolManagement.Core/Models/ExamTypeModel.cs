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
    public class ExamTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int DepartmentId { get; set; }
        public int AcademicYearId { get; set; }
        public int SemesterId { get; set; }
        public int IsActivity { get; set; }

        public static Task<List<ExamTypeModel>> GetExamType()
        {
            var result = new TaskCompletionSource<List<ExamTypeModel>>();
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

                        string requestUri = string.Format("api/ExamType");

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<ExamTypeModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<ExamTypeModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<ExamTypeModel>());
                }
            });
            return result.Task;
        }
    }
}