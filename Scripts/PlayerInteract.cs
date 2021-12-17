using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteract : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData){
        pressed = true;
        Debug.Log("Interact Pressed");
    }
 
    public void OnPointerUp(PointerEventData eventData){
        pressed = false;
    }

}
