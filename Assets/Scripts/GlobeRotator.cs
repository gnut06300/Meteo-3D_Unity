using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobeRotator : MonoBehaviour

{
    public float speed = 50f;

    private Vector2 rotation;

    // Update is called once per frame
    void Update()

    {
        /*transform.Rotate(Vector3.up * speed * Time.deltaTime);*/
        transform.Rotate(rotation.y * speed * Time.deltaTime, rotation.x * speed * Time.deltaTime, 0, Space.World);
    }

    public void RotationEarth(InputAction.CallbackContext ctx)
    {
        rotation = ctx.ReadValue<Vector2>();
    }

}