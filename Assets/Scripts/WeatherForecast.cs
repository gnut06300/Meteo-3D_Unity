using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeatherForecast : MonoBehaviour
{
    public TextMeshProUGUI[] forecasts;
    [SerializeField] GameObject forecastFond;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ForecastUi(string forecast, int i)
    {
        forecastFond.SetActive(true);
        forecasts[i].text = forecast;
    }
}
