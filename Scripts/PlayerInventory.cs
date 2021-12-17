using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject playerGround;

    public string[] slots;
    public Image[] slotImages = new Image[7];
    public Image[] slotImageHighlights = new Image[7];
    public Text[] slotTexts = new Text[6];
    public GameObject[] slotObjects = new GameObject[6];

    public bool pressed;
    public GameObject inventoryView;
    private Vector3 originalpoint;

    //this code is horrific and yet every tutorial I find is doing something similar with a canvas or just doesn't apply to what I'm doing.
    //if it doesn't affect the gameplay performace, I'll just keep this messy code. Sorry for anyone decoding the game. :(
    //Speaking of which, if you're reading this, feel free to email me! I'd love to know how to better efficiently do this!
    //Im assuming you know how to program games in unity considering... well, you're reading this.
    //- Dev
    public Slot1 slot1;
    public Slot2 slot2;
    public Slot3 slot3;
    public Slot4 slot4;
    public Slot5 slot5;
    public Slot6 slot6;
    public Slot7 slot7;

    public Sprite pistol;
    public Sprite flask;
    public Sprite flashlight;
    public Sprite document1;
    public Sprite document2;
    public Sprite document3;
    public Sprite hand;
    public Image usedItem = null;
    private bool equipped;

    public PlayerDrop dropButton;
    public Text interactText;
    public PlayerRaycast PlayerRaycast;
    public int slotSelected = -1;

    public PlayerUse animation;
    public PlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        slots = new string[6]; 
        originalpoint = inventoryView.transform.position;
        inventoryView.transform.position = new Vector3(Screen.width * 2f, originalpoint.y, originalpoint.z);
        usedItem.color = new Color32(255,255,255,255);
        usedItem.sprite = hand;
    }

    // Update is called once per frame
    void Update()
    {
        if (slot1.pressed == true && slotObjects[0] != null){
            slotSelected = 0;
            animation.pickupAnimation();
        }
        if (slot2.pressed == true && slotObjects[1] != null){
            slotSelected = 1;
            animation.pickupAnimation();
        }
        if (slot3.pressed == true && slotObjects[2] != null){
            slotSelected = 2;
            animation.pickupAnimation();
        }
        if (slot4.pressed == true && slotObjects[3] != null){
            slotSelected = 3;
            animation.pickupAnimation();
        }
        if (slot5.pressed == true && slotObjects[4] != null){
            slotSelected = 4;
            animation.pickupAnimation();
        }
        if (slot6.pressed == true && slotObjects[5] != null){
            slotSelected = 5;
            animation.pickupAnimation();
        }
        if(slot7.pressed){
            slotSelected = -1;
        }
        if(dropButton.pressed == true && slotSelected != -1 && playerMove.isGrounded == true){
            removeItemGUI(slotSelected);
            slotSelected = -1;
            usedItem.color = new Color32(255,255,255,0);
        }
        if(slotSelected == -1){
            foreach (Image a in slotImageHighlights){
                a.color = new Color32(255,255,255,0);
            }
            slotImageHighlights[6].color = new Color32(63,255,0,20);
            usedItem.sprite = hand;
            usedItem.color = new Color32(255,255,255,255);
            animation.resetItemInHand();
        }
        if(slotSelected > -1){
            foreach (Image a in slotImageHighlights){
                a.color = new Color32(255,255,255,0);
            }
            slotImageHighlights[slotSelected].color = new Color32(63,255,0,20);
            usedItem.sprite = slotImages[slotSelected].sprite;
            usedItem.color = new Color32(255,255,255,255);
        }
    }

    public void playAnimation(string type){
        //do some shit here
    }

    //determine if the inventory is full
    public bool full(){
        for(int i = 0; i < slots.Length; i++){
            if(slots[i] == null){
                return false;
            }
        }
        return true;
    }

    public void showInventoryTxt(){
        Debug.Log("Actual Items: " + slots);
    }

    public void AddItem(string item, GameObject pickedObject){
        if(full()){
            PlayerRaycast.interactWithTag("You can't pick that up. Your inventory is full.");
            return;
        }
        pickedObject.transform.position = new Vector3(pickedObject.transform.position.x, pickedObject.transform.position.y - 10, pickedObject.transform.position.z);
        for(int i = 0; i < slots.Length; i++){
            if(slots[i] == item){
                return;
            }
        }
        for(int i = 0; i < slots.Length; i++){
            if(slots[i] == null){
                slots[i] = item;
                slotObjects[i] = pickedObject;
                addItemGUI(i, item);
                break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Inventory Button Pressed");
        pressed = !pressed;
        if(pressed == false){
            inventoryView.transform.position = new Vector3(Screen.width * 2f, originalpoint.y, originalpoint.z);
        }
        else{
            inventoryView.transform.position = originalpoint;
        }
    }
 
    public void OnPointerUp(PointerEventData eventData){
        //Debug.Log("Inventory Button Released");
    }

    public void addItemGUI(int slotnumber, string type){
        
        Sprite itemlogo = null;
        string name = "";
        if(type == "pistol"){
            itemlogo = pistol;
            name = "Old Pistol";
        }
        else if(type == "potion"){
            itemlogo = flask;
            name = "Flask";
        }
        else if(type == "flashlight"){ //RIGHT HERE
            itemlogo = flashlight;
            name = "Flashlight";
        }
        else if(type == "document1"){ //RIGHT HERE
            itemlogo = document1;
            name = "RGI Instructions";
        }
        else if(type == "document2"){ //RIGHT HERE
            itemlogo = document2;
            name = "List of People";
        }
        else if(type == "document3"){ //RIGHT HERE
            itemlogo = document3;
            name = "Kyle's Document";
        }
        slotImages[slotnumber].sprite = itemlogo;
        slotTexts[slotnumber].text = name;

    }

    public void removeItemGUI(int slotnumber){
        GameObject droppedItem = slotObjects[slotnumber];
        slotImageHighlights[slotSelected].color = new Color32(255,255,255,0);
        slots[slotSelected] = null;
        slotImages[slotSelected].sprite = null;
        slotTexts[slotSelected].text = "Empty";
        slotObjects[slotSelected] = null;
        droppedItem.transform.position = new Vector3(playerGround.transform.position.x, playerGround.transform.position.y + 0.5f, playerGround.transform.position.z);
    }

    // public void setColor(){
    //     for()
    // }

}
