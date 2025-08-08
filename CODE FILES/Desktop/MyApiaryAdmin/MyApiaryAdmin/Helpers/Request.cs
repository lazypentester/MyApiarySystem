using MyApiaryAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApiaryAdmin.Helpers
{
    public class Request
    {
        private static string hostAuth = "http://grechaninova-001-site1.itempurl.com/api/";

        //private static string hostAuth = "http://localhost:51369/api/";

        //private static string hostRes2 = "http://localhost:63089/api/";

        private static string hostRes = "http://verabryzgalova-001-site1.etempurl.com/api/";

        public static bool AuthLogin(AuthLogin user)
        {
            bool auth = false;

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"{hostAuth}auth/login");
                var userJson = JsonConvert.SerializeObject(user);
                var payload = new StringContent(userJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result;

                try
                {
                    if (result.IsSuccessStatusCode)
                    {
                        try
                        {
                            string responce = result.Content.ReadAsStringAsync().Result;
                            UserAuth userAuth = JsonConvert.DeserializeObject<UserAuth>(responce);
                            Storage.Storage.token_long = userAuth.access_token_long;
                            Storage.Storage.token_short = userAuth.access_token_short;
                            Storage.Storage.user_id = userAuth.user_id;
                            auth = true;
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                catch(Exception e)
                {

                }
            }
            return auth;
        }

        public static List<SensorContext> GetAllSensors()
        {
            List<SensorContext> sensorList = null;

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"{hostRes}Sensors/getall");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Storage.Storage.token_long);

                try
                {
                    var result = client.GetAsync(endpoint).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        try
                        {
                            string responce = result.Content.ReadAsStringAsync().Result;
                            sensorList = JsonConvert.DeserializeObject<List<SensorContext>>(responce);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                catch(Exception e)
                {

                }

            }

            return sensorList;
        }
    
        public static void updateSensorsAuto(List<SensorContext> sensors)
        {
            if (sensors == null) return;

            Random r = new Random();
            int[] random_power = { 0, 0 };
            
            foreach(var sensor in sensors)
            {
                // умная генерация показателей для датчиков, опираясь на мин и макс фиксированные значения датчика(каждого отдельно)
                if (sensor.Is_working)
                {
                    double generated_value = (double)r.Next(Convert.ToInt32(sensor.Min_value) - random_power[0], Convert.ToInt32(sensor.Max_value) + random_power[1]) + r.NextDouble();
                    sensor.Value = (float)Math.Round(generated_value, 1);
                }
                else sensor.Value = 0;
            }

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"{hostRes}Sensors/editValue");
                var sensorsJson = JsonConvert.SerializeObject(sensors);
                var payload = new StringContent(sensorsJson, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Storage.Storage.token_long);

                try
                {
                    var result = client.PutAsync(endpoint, payload).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        try
                        {
                            string responce = result.Content.ReadAsStringAsync().Result;
                            Console.WriteLine(result.StatusCode);
                            //UserAuth userAuth = JsonConvert.DeserializeObject<UserAuth>(responce);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                catch(Exception e)
                {

                }
            }
        }

        public static List<SensorType> GetSensorTypes()
        {
            List<SensorType> sensorTypes = null;

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"{hostRes}SensorTypes/getall");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Storage.Storage.token_long);

                try
                {
                    var result = client.GetAsync(endpoint).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        try
                        {
                            string responce = result.Content.ReadAsStringAsync().Result;
                            sensorTypes = JsonConvert.DeserializeObject<List<SensorType>>(responce);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                catch (Exception e)
                {

                }

            }

            return sensorTypes;
        }

        public static List<Beehive> GetBeehives()
        {
            List<Beehive> beehives = null;

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"{hostRes}Beehives/getall");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Storage.Storage.token_long);

                try
                {
                    var result = client.GetAsync(endpoint).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        try
                        {
                            string responce = result.Content.ReadAsStringAsync().Result;
                            beehives = JsonConvert.DeserializeObject<List<Beehive>>(responce);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                catch (Exception e)
                {

                }

            }

            return beehives;
        }

        public static List<BaseStation> GetBaseStations()
        {
            List<BaseStation> baseStations = null;

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"{hostRes}Basestations/getall");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Storage.Storage.token_long);

                try
                {
                    var result = client.GetAsync(endpoint).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        try
                        {
                            string responce = result.Content.ReadAsStringAsync().Result;
                            baseStations = JsonConvert.DeserializeObject<List<BaseStation>>(responce);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                catch (Exception e)
                {

                }

            }

            return baseStations;
        }

        public static HttpStatusCode CreateNewSensor(SensorContext sensor)
        {
            HttpStatusCode code = 0;

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"{hostRes}Sensors/addbyAdmin");
                var sensorJson = JsonConvert.SerializeObject(sensor);
                var payload = new StringContent(sensorJson, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Storage.Storage.token_long);
                var result = client.PostAsync(endpoint, payload).Result;

                try
                {
                    code = result.StatusCode;
                }
                catch (Exception e)
                {

                }
            }
            return code;
        }
    }
}
