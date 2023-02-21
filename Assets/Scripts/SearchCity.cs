using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchCity : MonoBehaviour
{
    [SerializeField] TMP_InputField searchCity;
    [SerializeField] Button searchButton;
    [SerializeField] ApiRequest apiRequest;

    // Start is called before the first frame update
    void Start()
    {
        searchButton.onClick.AddListener(GetCity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GetCity()
    {
        string city = searchCity.text;
        Debug.Log(city);
        apiRequest.CallApiCity(city);
    }
}
