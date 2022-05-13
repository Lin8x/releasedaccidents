using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLook : MonoBehaviour
{
    public Joystick lookjoystick;
    public Transform playerBody;
    [SerializeField] public float lookspeed;
    
    //float xrotate = 0f;
    //float mouseX = 0;
    //float mouseY = 0;

    // private Vector3 inputRotation;
    // private Vector3 mousePlacement;
    // private Vector3 screenCentre;

    public Camera playerCamera;

    Vector3 firstPoint;
    public float sensitivity = 2.5f;

    public void RotateUpDown(float axis)
    {
       transform.RotateAround(transform.position, transform.right, -axis * Time.deltaTime);
    }

    //rotate the camera rigt and left (y rotation)
    public void RotateRightLeft(float axis)
    {
        playerBody.transform.RotateAround(transform.position, Vector3.up, -axis * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
    
        // This is for look joystick

        // mouseX = lookjoystick.Horizontal * lookspeed * Time.deltaTime;
        // mouseY = lookjoystick.Vertical * lookspeed * Time.deltaTime;

        // xrotate -= mouseY;
        // xrotate = Mathf.Clamp(xrotate, -90f, 90f);

        // transform.localRotation = Quaternion.Euler(xrotate, 0f, 0f);
        // playerBody.Rotate(Vector3.up * mouseX);

        if (Input.touchCount > 0)
        {
            Touch lookTouch = new Touch();
            lookTouch.fingerId = 100;

            //Debug.Log("Screen Height: " + Screen.height);

            // Filter touches based on x positon 
            for(int i = 0; i < Input.touchCount; i++){
                if(Input.GetTouch(i).position.x > Screen.width/2){ 
                    lookTouch = Input.GetTouch(i);
                }
            }
            
            if (lookTouch.phase == TouchPhase.Began && lookTouch.fingerId != 100)
            {
                
                //touch logic
                firstPoint = lookTouch.position;
                Debug.Log("original x" + lookTouch.position.x);
                Debug.Log("original y" + lookTouch.position.y);

                //testing purposes
                Debug.Log("First Touch Point X:" + firstPoint.x + ", Y:" + firstPoint.y); 
            }
            
            if (lookTouch.phase == TouchPhase.Moved && lookTouch.fingerId != 100)
            {
                
                Vector3 secondPoint = lookTouch.position;

                float x = FilterGyroValues(secondPoint.x - firstPoint.x);
                RotateRightLeft(-x * sensitivity);

                float y = FilterGyroValues(secondPoint.y - firstPoint.y);
                RotateUpDown(-y * -sensitivity);

                firstPoint = secondPoint;
            }
        }
    }

    float FilterGyroValues(float axis)
    {
        float thresshold = 0.5f;
        if (axis < -thresshold || axis > thresshold)
        {
            return axis;
        }
        else
        {
            return 0;
        }
    }

}
