/*
 * CameraController.cs
 * Camera movement code with mouse or keyboard
 */

using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float PAN_MIN_X = 20;
    public float PAN_MAX_X = 20;

    public float PAN_MIN_Y = 13;
    public float PAN_MAX_Y = 10;

    public int PAN_MIN_Z = -15;
    public int PAN_MAX_Z = -5;

    public float panSpeed = 20f;
    public float panBorder = 10f;
    public int scrollSpeed = 2;

    Vector3 pos;


    // Update is called once per frame
    void Update() {

        pos = transform.position; // Gathers x,y,z of current position.

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || (Input.mousePosition.y >= Screen.height - panBorder)) {

            pos.y += panSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || (Input.mousePosition.y <= panBorder)) {

            pos.y -= panSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || (Input.mousePosition.x <= panBorder)) {

            pos.x -= panSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || (Input.mousePosition.x >= Screen.width - panBorder)) {

            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Plus)) {
            pos.z += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Minus)) {
            pos.z -= panSpeed * Time.deltaTime;
        }

        int scroll = (int)(Input.GetAxis("Mouse ScrollWheel") * 10); // ensure number is equal to or greater than 1 or less than -1
        pos.z += scroll * scrollSpeed; // must be int multiplication otherwise screen breaks, dun ask me. - misslame


        // clips minimum/maximum positions on axis
        pos.x = Mathf.Clamp(pos.x, -PAN_MIN_X, PAN_MAX_X);
        pos.y = Mathf.Clamp(pos.y, -PAN_MIN_Y, PAN_MAX_Y);
        pos.z = Mathf.Clamp(pos.z, PAN_MIN_Z, PAN_MAX_Z);


        transform.position = pos;

    }
}
