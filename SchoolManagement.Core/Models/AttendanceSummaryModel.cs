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
    public class AttendanceSummaryModel
    {
        public int TotalWorkingDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }

        public static Task<AttendanceSummaryModel> GetAttendanceSummary(int dateCounter)
        {
            var result = new TaskCompletionSource<AttendanceSummaryModel>();
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

                        string requestUri = string.Format("api/ViewAttendanceSummary/{0}", dateCounter);

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<AttendanceSummaryModel>(content));
                        }
                        else
                        {
                            result.SetResult(new AttendanceSummaryModel());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new AttendanceSummaryModel());
                }
            });
            return result.Task;
        }
    }

    public class YearMonthModel
    {
        public int Key { get; set; }
        public string Value { get; set; }

        public static Task<List<YearMonthModel>> GetYerRange()
        {
            var result = new TaskCompletionSource<List<YearMonthModel>>();
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

                        string requestUri = string.Format("api/YearRange");

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<List<YearMonthModel>>(content));
                        }
                        else
                        {
                            result.SetResult(new List<YearMonthModel>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new List<YearMonthModel>());
                }
            });
            return result.Task;
        }
    }
}