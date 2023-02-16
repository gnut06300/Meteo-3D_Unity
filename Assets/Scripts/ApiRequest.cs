using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;

public class ApiRequest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?appid=20cc30210e1cd0dfe6f4d7dd7e3de6e5&lat=43.6961&lon=7.27178&units=metric&lang=fr"));
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
                    break;
            }
        }
    }

    public class WeatherReponse
    {
        public string name { get; set; } 
        public WeatherMain main { get; set; }
    }

    public class WeatherMain
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public int pressure { get; set; }
        public string humidity { get; set; }
    }
}
