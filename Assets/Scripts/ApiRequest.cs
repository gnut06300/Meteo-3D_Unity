using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class ApiRequest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI city;
    [SerializeField] TextMeshProUGUI cityTemp;
    [SerializeField] TextMeshProUGUI cityTempFells;
    [SerializeField] TextMeshProUGUI cityPressure;
    [SerializeField] TextMeshProUGUI cityHumidity;
    [SerializeField] TextMeshProUGUI cityState;
    private string key;
    // Start is called before the first frame update
    void Start()
    {
        key = File.ReadAllText(@"Assets\apikey.txt");
        // A correct website page.
        string uri = "https://api.openweathermap.org/data/2.5/weather?appid=" + key + "&lat=43.6961&lon=7.27178&units=metric&lang=fr";
        StartCoroutine(GetRequest(uri));
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    Debug.Log(weatherReponse.name + " :\nTempérature : " + weatherReponse.main.temp + "°C");
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

    public class WeatherReponse
    {
        public string name { get; set; } 
        public WeatherMain main { get; set; }
        public IList<Weather> weather { get; set; }
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
