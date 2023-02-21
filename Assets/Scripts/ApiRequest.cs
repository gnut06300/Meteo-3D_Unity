using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class ApiRequest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI city;
    [SerializeField] TextMeshProUGUI cityTemp;
    [SerializeField] TextMeshProUGUI cityTempFells;
    [SerializeField] TextMeshProUGUI cityPressure;
    [SerializeField] TextMeshProUGUI cityHumidity;
    [SerializeField] TextMeshProUGUI cityState;
    [SerializeField] WeatherForecast weatherForecast;
    private string key;

    // Start is called before the first frame update
    void Start()
    {
        key = File.ReadAllText(@"Assets\apikey.txt");
        // A correct website page.
        
        /*string uri = "https://api.openweathermap.org/data/2.5/weather?appid=" + key + "&lat=43.6961&lon=7.27178&units=metric&lang=fr";
        StartCoroutine(GetRequest(uri));*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallApi(Vector2 longLat)
    {
        string uri = "https://api.openweathermap.org/data/2.5/weather?appid=" + key + "&lat=" + longLat.y + "&lon=" + longLat.x + "&units=metric&lang=fr";
        StartCoroutine(GetRequest(uri));
        string uri4Days = "https://api.openweathermap.org/data/2.5/forecast?appid=" + key + "&lat=" + longLat.y + "&lon=" + longLat.x + "&units=metric&lang=fr";
        StartCoroutine(GetRequest4Days(uri4Days));
    }

    public void CallApiCity(string cityName)
    {
        //string uri = "https://api.openweathermap.org/data/2.5/weather?appid=20cc30210e1cd0dfe6f4d7dd7e3de6e5&q=nice&units=metric&lang=fr";
        string uri = "https://api.openweathermap.org/data/2.5/weather?appid=" + key + "&q=" + cityName + "&units=metric&lang=fr";
        string uri4Days = $"https://api.openweathermap.org/data/2.5/forecast?appid={key}&q={cityName}&units=metric&lang=fr";
        StartCoroutine(GetRequest(uri));
        StartCoroutine(GetRequest4Days(uri4Days));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string json = webRequest.downloadHandler.text;
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    WeatherReponse weatherReponse = JsonConvert.DeserializeObject<WeatherReponse>(json);
                    //Debug.Log(weatherReponse.name + " :\nTempérature : " + weatherReponse.main.temp + "°C");
                    string cityName = weatherReponse.name;
                    float tempReponse = weatherReponse.main.temp;
                    float tempFellsReponse = weatherReponse.main.feels_like;
                    string weatherDesc = weatherReponse.weather[0].description;
                    int pressureReponse = weatherReponse.main.pressure;
                    int humidityReponse = weatherReponse.main.humidity;
                    city.text = "Ville : " + cityName;
                    cityTemp.text = "Température : " + tempReponse.ToString("0.00") + "°C";
                    cityTempFells.text = "Température ressentie : " + tempFellsReponse.ToString("0.00") + "°C";
                    cityState.text = char.ToUpper(weatherDesc[0]) + weatherDesc.Substring(1);
                    cityPressure.text = "Pression : " + pressureReponse.ToString() + "hPa";
                    cityHumidity.text = "Humidité : " + humidityReponse.ToString() + "%";
                    break;
            }
        }
    }

    IEnumerator GetRequest4Days(string uri4Days)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri4Days))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri4Days.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string json = webRequest.downloadHandler.text;
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    WeatherReponse4Days weatherReponse = JsonConvert.DeserializeObject<WeatherReponse4Days>(json);
                    DisplayForecastMeteo(weatherReponse);
                    break;
            }
        }
    }
    public void DisplayForecastMeteo(WeatherReponse4Days meteoData)
    {
        List<WeatherReponse> meteoByDay = meteoData.list.Where(meteo => meteo.dateMeteo.Hour == 12).ToList();
        int i = 0;
        foreach(WeatherReponse meteoData4Days in meteoByDay)
        {
            //Debug.Log(meteoData4Days.dateMeteo.ToString("dd/MM/yyyy hh:mm") + " Température :" + meteoData4Days.main.temp + "°C " + meteoData4Days.weather[0].description);
            string forescast = meteoData4Days.dateMeteo.ToString("dd/MM/yyyy hh:mm") + " Température : " + meteoData4Days.main.temp + "°C " + meteoData4Days.weather[0].description;
            weatherForecast.ForecastUi(forescast, i);
            i++;
            
        }
        
    }
    public class WeatherReponse4Days
    {
        public List<WeatherReponse> list { get; set; }
    }
    public class WeatherReponse
    {
        public string name { get; set; } 
        public WeatherMain main { get; set; }
        public IList<Weather> weather { get; set; }
        public DateTime dateMeteo { get; set; }

        private string dt_txt;
        public string Dt_txt
        {
            get { return dt_txt; }
            set
            {
                dt_txt = value;
                dateMeteo = DateTime.ParseExact(dt_txt, "yyyy-MM-dd HH:mm:ss", null);
            }
        }
    }

    public class WeatherMain
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Weather
    {
        public string description { get; set; }
    }
}
