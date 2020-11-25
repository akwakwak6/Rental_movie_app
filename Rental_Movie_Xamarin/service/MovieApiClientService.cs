using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rental_Movie_Xamarin.model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rental_Movie_Xamarin.service {

    static class MovieApiClientService {

        public static bool IsConnected { get; private set; } = false;
        public static User User { get; private set; } = null;

        private static string BASE_API_URL = "https://10.0.2.2:44315/api/";

        private static HttpClient _httpClient = getClient();

        public static async Task<bool> Register(User user) {
            var contentToSend = JsonConvert.SerializeObject(user);
            HttpResponseMessage response = await _httpClient.PostAsync(new Uri(BASE_API_URL + "auth/register"), new StringContent(contentToSend, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) {
                return true;
            }
            return false;
        }

        public static async Task<T> GetObjetc<T>(string path) where T : class {
            HttpResponseMessage response = await _httpClient.GetAsync(new Uri(BASE_API_URL + path));
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            return null;
        }

        public static async Task<List<MovieListModel>> GetFilmListFilted( FiltterModel fm ) {

            var contentToSend = JsonConvert.SerializeObject(fm);

            HttpResponseMessage response = await _httpClient.PostAsync(new Uri(BASE_API_URL + "movies/info"), new StringContent(contentToSend, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MovieListModel>>(content);
            }
            return null;
        }

        public static async Task<bool> SendRental() {

            List<int> l = new List<int>();
            foreach(MovieListModel ml in CartService.GetMoviesList()) {
                l.Add(ml.Id);
            }
            var contentToSend = JsonConvert.SerializeObject(l);
            HttpResponseMessage response = await _httpClient.PostAsync(new Uri(BASE_API_URL + "rental"), new StringContent(contentToSend, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) {
                return true;
            }
            return false;
        }

        private static HttpClient getClient() {
            return new HttpClient(
                new HttpClientHandler() {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => {
                        //bypass certificate
                        return true;
                    },
                }
                , false);
        }

        public static async Task<MovieDetailModel> GetMovieDetailById(int id) {
            HttpResponseMessage response = await _httpClient.GetAsync(new Uri(BASE_API_URL + "Movies/info/" + id));
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MovieDetailModel>(content);
            }
            return null;
        }

        public static async Task<List<OrderModel>> GetMyOrder() {
            HttpResponseMessage response = await _httpClient.GetAsync(new Uri(BASE_API_URL + "rental"));
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<OrderModel>>(content);
            }
            return null;
        }


        public static async Task<List<MovieListModel>> GetMovieListStartedBy(char initial) {

            HttpResponseMessage response = await _httpClient.GetAsync(new Uri(BASE_API_URL+"Movies/" + initial));
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MovieListModel>>(content);
            }
            return null;
        }

        public static async Task<bool> LogIn( string mail,string pwd) {

            User user = new User() { Email = mail, Password = pwd };
            var contentToSend = JsonConvert.SerializeObject(user);
            
            HttpResponseMessage response = await _httpClient.PostAsync(new Uri(BASE_API_URL + "auth/login"), new StringContent(contentToSend, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<User>(content);
                IsConnected = true;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", User.Token);
            }
            return IsConnected;

        }


    }
}
