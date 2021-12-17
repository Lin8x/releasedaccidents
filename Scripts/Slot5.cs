using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot5 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed;

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Slot5 Pressed");
        pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData){
        Debug.Log("Slot5 unpressed");
        pressed = false;
    }
}
