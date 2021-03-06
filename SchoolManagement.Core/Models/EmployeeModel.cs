﻿using Newtonsoft.Json;
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
    public class EmployeeModel
    {
        #region Property
        public int Total { get; set; }
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }

        #endregion

        public static Task<EmployeeModel> GetEmployeeCount()
        {
            var result = new TaskCompletionSource<EmployeeModel>();
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

                        string requestUri = string.Format("api/EmployeeHeadCount");

                        var response = await client.GetAsync(requestUri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result.SetResult(JsonConvert.DeserializeObject<EmployeeModel>(content));
                        }
                        else
                        {
                            result.SetResult(new EmployeeModel());
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetResult(new EmployeeModel());
                }
            });
            return result.Task;
        }
    }
}