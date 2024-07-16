using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Task1.Models;

namespace Task1.Services
{
    public class UserService
    {
        private readonly HttpClient _client;

        public UserService()
        {
            _client = new HttpClient();
        }

        //API method create
        public async Task<UserModel> GetRandomUser()
        {
            var response = await _client.GetAsync("https://randomuser.me/api");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserApiResponse>(json);
            return MapToUserModel(result);
        }

        //Pass API data 
        private UserModel MapToUserModel(UserApiResponse response)
        {
            var user = response?.Results?.FirstOrDefault();
            if (user != null)
            {
                return new UserModel
                {
                    FirstName = user.Name.First,
                    LastName = user.Name.Last,
                    Gender = user.Gender,
                    DateOfBirth = user.Dob.Date,
                    Email = user.Email,
                    Country = user.Location.Country,
                    ProfilePicture = user.Picture.Thumbnail
                };
            }
            return null;
        }
    }

}