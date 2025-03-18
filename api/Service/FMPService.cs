using api.Models;
using api.Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using api.Dtos.Stock;
using api.Mappers;
        
        namespace api.Service
        {
            public class FMPService : IFMPService
            {
                private HttpClient _httpClient;
                private IConfiguration _config;
        
                public FMPService(HttpClient httpClient, IConfiguration config)
                {
                    _httpClient = httpClient;
                    _config = config;
                }
        
                public async Task<Stock> FindStockBySymbolasync(string symbol)
                {
                    try
                    {
                        var result =
                            await _httpClient.GetAsync(
                                $"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}");
                        if (result.IsSuccessStatusCode)
                        {
                            var content = await result.Content.ReadAsStringAsync();
                            var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                            var stock = tasks[0];
                            if (stock != null)
                            {
                                return stock.ToStockFromFMP();
                            }
        
                            return null;
                        }
                        return null;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return null;
                    }
                }
            }
        }