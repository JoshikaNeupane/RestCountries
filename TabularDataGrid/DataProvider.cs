using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Countries.Models;

namespace Countries
{

    public class DataProvider
    {
        /// <summary>
        /// map rest countries response to list of countries
        /// </summary>
        /// <param name="restCountriesResult">response fom rest countries API</param>
        /// <returns>List of Country</returns>
        public List<Country> MapRestCountriesJson(string restCountriesResult)
        {
            var countries = new List<Country>();
            try
            {
                JArray array = JArray.Parse(restCountriesResult);
                foreach (JObject obj in array)
                {

                    var country = new Country
                    {
                        Name = obj["name"]["common"]?.ToString(),
                        OfficialName = obj["name"]["official"]?.ToString(),
                        CountryCode = obj["cioc"]?.ToString(),
                        Flag = obj["flags"]["png"]?.ToString(),

                    };

                    JToken capital = obj["capital"];
                    if (capital?.Type == JTokenType.Array)
                    {
                        country.Capital = capital.First().ToString();
                    }

                    if (double.TryParse(obj["population"]?.ToString(), out double population))
                    {
                        country.Population = population;
                    }

                    if (bool.TryParse(obj["unMember"]?.ToString(), out bool isUnMember))
                    {
                        country.IsUnMember = isUnMember;
                    }

                    countries.Add(country);
                }

                return countries;
            }
            catch (Exception ex)
            {
                return countries;
                //throw;
            }
        }

        /// <summary>
        /// Get list of countries using public REST API (https://restcountries.com/v3.1/all)
        /// </summary>
        /// <returns></returns>
        public async Task<List<Country>> GetCountries()
        {
            List<Country> countries = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://restcountries.com/v3.1/all");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResult = await response.Content.ReadAsStringAsync();
                        countries = MapRestCountriesJson(jsonResult);
                    }
                }
                return countries;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
