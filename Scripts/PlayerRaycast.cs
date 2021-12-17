//using System.Threading.Tasks.Dataflow;
//using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{

    public Camera playercamera;
    public PlayerUse animationTool;
    public Text interactText;
    public PlayerInteract jI;
    public PlayerUse pU;
    public Button interactButton;
    public PlayerInventory inventory;
    private float distanceToSee = 7;
    RaycastHit whatIHit;
    private Vector3 originalpoint;

    // Start is called before the first frame update
    void Start()
    {
        interactText.text = "";
        originalpoint = interactButton.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //See what the player is looking at
        Debug.DrawRay(playercamera.transform.position, playercamera.transform.forward * distanceToSee, Color.magenta);

        //if(jI.pressed){

        if(Physics.Raycast(playercamera.transform.position, playercamera.transform.forward, out whatIHit, distanceToSee)){
            Debug.Log("I touched " + whatIHit.collider.gameObject.name);
            Debug.Log("Object Tag " + whatIHit.collider.gameObject.tag);
            if(whatIHit.collider.gameObject.tag.ToString() == "CrustyWall"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed){StartCoroutine(interactWithTag("Wow this wall is crusty... I probably shouldn't touch it."));}
            }
            else if(whatIHit.collider.gameObject.tag.ToString() == "RustyChair"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed){StartCoroutine(interactWithTag("This chair seems like it's molding."));}
            }
            else if(whatIHit.collider.gameObject.tag.ToString() == "Gun"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed)
                {
                    inventory.AddItem("pistol", whatIHit.collider.gameObject);
                    StartCoroutine(interactWithTag("You picked up the pistol."));
                }
            }
            else if(whatIHit.collider.gameObject.tag.ToString() == "Potion"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed)
                {
                    inventory.AddItem("potion", whatIHit.collider.gameObject);
                    StartCoroutine(interactWithTag("You picked up the potion."));
                }
            }
            else if(whatIHit.collider.gameObject.tag.ToString() == "Flashlight"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed)
                {
                    inventory.AddItem("flashlight", whatIHit.collider.gameObject);
                    StartCoroutine(interactWithTag("You picked up a flashlight."));
                }
            }
            else if(whatIHit.collider.gameObject.tag.ToString() == "Document1"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed)
                {
                    inventory.AddItem("document1", whatIHit.collider.gameObject);
                    StartCoroutine(interactWithTag("You picked up a document."));
                }
            }
            else if(whatIHit.collider.gameObject.tag.ToString() == "Document2"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed)
                {
                    inventory.AddItem("document2", whatIHit.collider.gameObject);
                    StartCoroutine(interactWithTag("You picked up a document."));
                }
            }
            else if(whatIHit.collider.gameObject.tag.ToString() == "Document3"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed)
                {
                    inventory.AddItem("document3", whatIHit.collider.gameObject);
                    StartCoroutine(interactWithTag("You picked up a document."));
                }
            }
            else if(whatIHit.collider.gameObject.tag.ToString() == "UnlockedDoor"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed && whatIHit.collider.gameObject.transform.localPosition.z == 0)
                {
                    StartCoroutine(animationTool.moveDoor(whatIHit.collider.gameObject, 1.3f, 2f, true));
                }
            }
            else if(whatIHit.collider.gameObject.tag.ToString() == "Ammo"){
                interactButton.transform.position = new Vector3(originalpoint.x, interactButton.transform.position.y, interactButton.transform.position.z);
                if(jI.pressed)
                {
                    pU.mag = pU.mag + 1;
                    pU.ammoText.text = "x " + pU.mag;
                    Destroy(whatIHit.collider.gameObject);
                    StartCoroutine(interactWithTag("You picked up a pistol magazine."));
                }
            }
            else {interactButton.transform.position = new Vector3(Screen.width * 1.5f, interactButton.transform.position.y, interactButton.transform.position.z);}

            //Debug.Log(inventory.full());
            //inventory.showInventoryTxt();

        }
        else {interactButton.transform.position = new Vector3(Screen.width * 1.5f, interactButton.transform.position.y, interactButton.transform.position.z);}
    }

    public IEnumerator interactWithTag(string message){
         interactText.text = message;
         yield return new WaitForSeconds(2);
         interactText.text = "";
    }
}
