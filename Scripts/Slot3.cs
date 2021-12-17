using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot3 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed;

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Slot3 Pressed");
        pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData){
        Debug.Log("Slot3 unpressed");
        pressed = false;
    }
}
