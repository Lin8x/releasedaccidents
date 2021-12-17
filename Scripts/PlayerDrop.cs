using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData){
        //Debug.Log("Crouch Button Pressed");
        pressed = true;
    }
 
    public void OnPointerUp(PointerEventData eventData){
        //Debug.Log("Crouch Button Released");
        pressed = false;
    }
}
