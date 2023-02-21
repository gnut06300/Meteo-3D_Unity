using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicLocation : MonoBehaviour
{
    private Vector3 clickPos;
    private bool clicked;
    private Vector2 longLat;
    [SerializeField] ApiRequest apiRequest;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        clicked = Input.GetMouseButtonDown(0);
        clickPos = Input.mousePosition;


        RaycastHit rt;
        Ray ray = Camera.main.ScreenPointToRay(clickPos);
        if (Physics.Raycast(ray, out rt) && clicked)
        {
            Debug.Log("Coordonnées x, y, z: " + rt.point + " x :" + rt.point.x + " y :" + rt.point.y + " z :" + rt.point.z);
            // nullIsland x :-0,9884224 y :-0,373946 z :-49,14286
            Vector3 offset = rt.collider.transform.InverseTransformPoint(rt.point);
            //Vector3 offset = rt.point;
            longLat = ToSpherical(offset);

            // appeler api request
            apiRequest.CallApi(longLat);
        }
    }

    public Vector2 ToSpherical(Vector3 position)
    {
        // Convert to a unit vector so our y coordinate is in the range -1...1.
        position.Normalize();

        // The vertical coordinate (y) varies as the sine of latitude, not the cosine.
        float lat = Mathf.Asin(position.y) * Mathf.Rad2Deg;

        // Use the 2-argument arctangent, which will correctly handle all four quadrants.
        float lon = 90 - Mathf.Atan2(position.x, position.z) * Mathf.Rad2Deg;
        if (lon > 180f)
        {
            lon -= 360f;
        }

        // Here I'm assuming (0, 0, 1) = 0 degrees longitude, and (1, 0, 0) = +90.
        // You can exchange/negate the components to get a different longitude convention.

        Debug.Log(lat + ", " + lon);

        // I usually put longitude first because I associate vector.x with "horizontal."
        return new Vector2(lon, lat);
    }
}
