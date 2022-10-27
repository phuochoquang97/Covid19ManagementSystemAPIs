using System.Collections;
using System.Threading.Tasks.Dataflow;
using System.IO;
using System;
using System.Net;
using System.Collections.Generic;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Repositories.Homepage;
using Covid_Project.Domain.Services.Communication;
using Covid_Project.Domain.Services.Homepage;
using Newtonsoft.Json.Linq;

namespace Covid_Project.Services.Homepage
{
    public class HomepageService : IHomepageService
    {
        private readonly IHomepageRepository _homepageRepository;
        public HomepageService(IHomepageRepository homepageRepository)
        {
            _homepageRepository = homepageRepository;

        }

        public List<News> GetAllNews()
        {
            return _homepageRepository.GetAllNews();
        }

        public PageResponse<List<News>> GetListNews(PaginationFilter filter)
        {
            return _homepageRepository.GetListNews(filter);
        }

        public JObject GetMedicalInfoStatistic()
        {
            string url = "https://api.apify.com/v2/key-value-stores/p3nS2Q9TUn6kUOriJ/records/LATEST?fbclid=IwAR0tQNm7YV0YQdpnJSXSlE_p8tFBcb78l3UpJ8afxMjVwQcat_S7pIrBiaQ";
            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();
            string responseFromServer = "";
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
            }
            JObject jObject = JObject.Parse(responseFromServer);
            response.Close();

            var keys = jObject["key"];
            foreach (var key in keys)
            {
                key["name"] = convertCityName(key["name"].ToString());
                Console.WriteLine(convertCityName(key["name"].ToString()));
            }
            return jObject;
        }
        private string convertCityName(string cityName)
        {
            string[] words = cityName.Split("-");
            string[] wordsAfter = new string[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                string wordAfter = words[i][0].ToString().ToUpper() + words[i].Substring(1);
                wordsAfter[i] = wordAfter;
            }

            return string.Join(" ", wordsAfter);
        }
        public News GetNewsById(int id)
        {
            return _homepageRepository.GetNewsById(id);
        }
    }
}