using System;
using UnityEngine;

public class CameraControler : MonoBehaviour{

    public float panSpeed = 20f;
    public float panBorder = 10f;

    Vector3 pos;


    // Update is called once per frame
    void Update(){

        pos = transform.position; // Gathers x,y,z of current position.
            
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || (Input.mousePosition.y >= Screen.height - panBorder) ){

            pos.y += panSpeed * Time.deltaTime;

        }
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || (Input.mousePosition.y <= panBorder)){

            pos.y -= panSpeed * Time.deltaTime;

        }
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || (Input.mousePosition.x <=  panBorder)){

            pos.x -= panSpeed * Time.deltaTime;

        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || (Input.mousePosition.x >= Screen.width - panBorder)){

            pos.x += panSpeed * Time.deltaTime;
        }

        transform.position = pos;

    }
}
