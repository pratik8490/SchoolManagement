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
    public class StandardExamMasterModel
    {
        public int Id { get; set; }
        public int StandardId { get; set; }
        public int ExamTypeId { get; set; }
        public int ConsolidateResultId { get; set; }
        public int ActivityMark { get; set; }
        public int AcademicYearId { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }

        public static Task<List<StandardExamMasterModel>> GetStdExamMaster()
        {
            var result = new TaskCompletionSource<List<StandardExamMasterModel>>();
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

                        string requestUri = string.Format("api/StandardExamMaster");

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<StandardExamMasterModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<StandardExamMasterModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<StandardExamMasterModel>());
                }
            });
            return result.Task;
        }
    }
}