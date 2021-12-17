using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot6 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed;

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Slot6 Pressed");
        pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData){
        Debug.Log("Slot6 unpressed");
        pressed = false;
    }
}
