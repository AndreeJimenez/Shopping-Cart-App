using Newtonsoft.Json;
using ShoppingCartApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services
{
    public class ApiService
    {
        private string ApiUrl = "http://impetec.ddns.net:14/apicart/";

        public async Task<ApiResponse> GetDataAsync<T>(string controller)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };
                var response = await client.GetAsync(controller);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var data = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = data
                };
            }
            catch (System.Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public async Task<ApiResponse> PostDataAsync(string controller, object data)
        {
            try
            {
                var serializedData = JsonConvert.SerializeObject(data);
                var content = new StringContent(serializedData, Encoding.UTF8, "application/json");

                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };

                var response = await client.PostAsync(controller, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result.ToString(),
                        Result = null
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse>(result);
            }
            catch (System.Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }
    }
}
