using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SearchCity : MonoBehaviour
{
    [SerializeField] TMP_InputField searchCity;
    [SerializeField] Button searchButton;
    [SerializeField] ApiRequest apiRequest;
    [SerializeField] TextMeshProUGUI notCity;

    // Start is called before the first frame update
    void Start()
    {
        searchButton.onClick.AddListener(GetCity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValidEnter(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GetCity();
        }
    }
    
    public void GetCity()
    {
        string city = searchCity.text;
        Debug.Log(city);
        apiRequest.CallApiCity(city);
        searchCity.text = "";
    }

    public void NotCity(string cityName)
    {
        notCity.gameObject.SetActive(true);
        notCity.text = "La ville " + cityName + " n'est pas référencé";
    }

    public void OKCity()
    {
        notCity.gameObject.SetActive(false);
    }
}
