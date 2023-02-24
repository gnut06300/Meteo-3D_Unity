using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeatherForecast : MonoBehaviour
{
    public TextMeshProUGUI[] forecasts;
    [SerializeField] GameObject forecastFond;
    [SerializeField] TextMeshProUGUI city;
    [SerializeField] TextMeshProUGUI cityTemp;
    [SerializeField] TextMeshProUGUI cityTempFells;
    [SerializeField] TextMeshProUGUI cityPressure;
    [SerializeField] TextMeshProUGUI cityHumidity;
    [SerializeField] TextMeshProUGUI cityState;
    [SerializeField] GameObject weather;
    [SerializeField] SearchCity searchCity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WeatherActuel(string cityName, float tempReponse, float tempFellsReponse, string weatherDesc, int pressureReponse, int humidityReponse)
    {
        weather.SetActive(true);
        city.text = cityName.Length == 0 ? "" : "Ville : " + cityName;
        cityTemp.text = "Température : " + tempReponse.ToString("0.00") + "°C";
        cityTempFells.text = "Température ressentie : " + tempFellsReponse.ToString("0.00") + "°C";
        cityState.text = char.ToUpper(weatherDesc[0]) + weatherDesc.Substring(1);
        cityPressure.text = "Pression : " + pressureReponse.ToString() + "hPa";
        cityHumidity.text = "Humidité : " + humidityReponse.ToString() + "%";
        searchCity.OKCity();
    }

    public void ForecastUi(string forecast, int i)
    {
        forecastFond.SetActive(true);
        forecasts[i].text = forecast;
    }
}
