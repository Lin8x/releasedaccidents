using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot4 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed;

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Slot4 Pressed");
        pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData){
        Debug.Log("Slot4 unpressed");
        pressed = false;
    }
}
