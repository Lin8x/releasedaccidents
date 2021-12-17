using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUse : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerInventory inventory;
    public PlayerRaycast playerRaycast;
    public Image animatedItem;
    public Image magItem;
    public float itemWeight;
    public bool pressed;

    public Sprite pistolSprite;
    public Sprite emptyFlaskSprite;
    public Sprite flashlightSprite;
    public Sprite notAvailable;

    private Vector3 originalPos;
    private Vector3 originalScale;
    private Vector3 lowerPos;
    private Vector3 regularAngle;

    public Camera playerCamera;

    public Light playerLight;
    public Color32 noFlashLight = new Color32(68, 136, 253, 255);
    public float noLightIntensity = 65;
    public float flashLightIntensity = 100;

    public Image paperDocument;
    private Color32 documentOff = new Color32(0, 0, 0, 0);
    private Color32 documentOn = new Color32(255, 255, 255, 255);
    private Color32 heldColor = new Color32(0, 180, 255, 65);
    private Vector3 paperScale = new Vector3(3.0877119f, 4.877119f, 4.877119f);
    public bool documentVisible = false;

    private Vector3 shootPos;
    private Vector3 reloadPos;
    private Vector3 reloadPosTwo;
    private Vector3 reloadAngle;
    private Vector3 magPos;
    private Vector3 magReloadPos;
    private float range;
    public GameObject impact;
    public Image muzzleflash;
    public Slider ammoBar;
    public float ammo;
    public float mag;
    public Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        //everything for basic hand
        pressed = false;
        originalPos = animatedItem.transform.position;
        originalScale = new Vector3(4.877119f, 4.877119f, 4.877119f);
        lowerPos = new Vector3(animatedItem.transform.position.x,  -(Screen.height/3.5f), animatedItem.transform.position.z);
        regularAngle = new Vector3(0, 0, 0);

        //everything for gun
        shootPos = new Vector3(animatedItem.transform.position.x + 200, animatedItem.transform.position.y - 100, animatedItem.transform.position.z);
        reloadPos = new Vector3(animatedItem.transform.position.x + 65, Screen.height/1.7f, animatedItem.transform.position.z);
        reloadPosTwo = new Vector3(reloadPos.x - 10, reloadPos.y - 400, reloadPos.z);
        reloadAngle = new Vector3(0, 0, -29.1f);

        //everything for mag
        magPos = new Vector3(23.3f, -12.7f, 0f);
        magReloadPos = new Vector3(Screen.width/1.3f, -300f, 0f);
        magItem.enabled = false;

        //muzzleflash.color = invsible;
        muzzleflash.enabled = false;
        ammo = 5;
        mag = 1;
        ammoBar.maxValue = 5;
        ammoBar.value = 5;
        ammoText.text = "x " + mag;
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory.slotSelected <= -1){
            animatedItem.transform.position = lowerPos;
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        pressed = true;
        useAnimation(inventory.slotSelected);
    }
 
    public void OnPointerUp(PointerEventData eventData){
        pressed = false;
    }

    public void pickupAnimation(){
        resetItemInHand();
        //4488FD
        animatedItem.transform.position = lowerPos;
        if(inventory.slotObjects[inventory.slotSelected].tag == "Gun"){
            animatedItem.sprite = pistolSprite;
            itemWeight = 0.9f;
            ammoText.text = "x " + mag;
            ammoBar.value = ammo;
        }
        else if(inventory.slotObjects[inventory.slotSelected].tag == "Potion"){
            animatedItem.sprite = emptyFlaskSprite;
            itemWeight = 0.3f;
        }
        else if(inventory.slotObjects[inventory.slotSelected].tag == "Flashlight"){
            animatedItem.sprite = flashlightSprite;
            playerLight.intensity = flashLightIntensity;
            playerLight.color = Color.white;
            itemWeight = 0.6f;
        }
        else if(inventory.slotObjects[inventory.slotSelected].tag == "Document1" || inventory.slotObjects[inventory.slotSelected].tag == "Document2" | inventory.slotObjects[inventory.slotSelected].tag == "Document3"){
            animatedItem.sprite = inventory.slotImages[inventory.slotSelected].sprite;
            animatedItem.color = heldColor;
            animatedItem.transform.localScale = paperScale;
            itemWeight = 0.2f;
        }
        else{
            animatedItem.sprite = notAvailable;
            playerLight.intensity = noLightIntensity;
            playerLight.color = noFlashLight;
            itemWeight = 0.5f;
        }
        animatedItem.transform.DOMove(originalPos, itemWeight, false);
    }

    public void resetItemInHand(){
        paperDocument.color = documentOff;
        documentVisible = false;
        animatedItem.color = Color.white;
        animatedItem.transform.localScale = originalScale;
        playerLight.intensity = noLightIntensity;
        playerLight.color = noFlashLight;

    }

    public void useAnimation(int num){
        print("num - " + num + " ammo - " + ammo + " ammoValue - " + ammoBar.value + " magz - " + mag);
        if(num <= -1){
           return;
        }
       
        //Debugging checking for spam. For some reason the y positions are not the same???
        //print("X1: " + animatedItem.transform.position.x + " Y1: " + animatedItem.transform.position.y + " Z1: " + animatedItem.transform.position.z);
        //print("X2: " + originalPos.x + " Y2: " + originalPos.y + " Z2: " + originalPos.z);
        //ammo = 5;
    
        if(animatedItem.transform.position.x == originalPos.x){
            if(inventory.slotObjects[inventory.slotSelected].tag == "Gun"){
                if(mag == 0 && ammo == 0){
                    StartCoroutine(playerRaycast.interactWithTag("You are out of ammo."));
                }
                //if there is available ammo
                else if(ammo > 0){
                    //Changes range to gun range
                    range = 100f;

                    //ammo
                    ammo -= 1;
                    ammoBar.value = ammo;

                    //See the shooting
                    //muzzleflash.color = visible;
                    StartCoroutine(flashMuzzle());
                    Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * range, Color.yellow);
                    
                    //Perform Shoot Animation
                    StartCoroutine(performAnimation(shootPos, 0.1f, 0.6f, 0.7f, false, true));

                    //Determine shot
                    RaycastHit hit;
                    if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range)){
                        // if(hit.collider.gameObject.tag.ToString()){
                        //     ENEMY REACTION
                        // }
                    }
                }
                else {
                    StartCoroutine(reloadAnimation());
                    ammo = 5;
                    mag -= 1;
                }
                ammoText.text = "x " + mag;
            }
            if(inventory.slotObjects[inventory.slotSelected].tag == "Flashlight"){
                //AHHHHHHH
            }
            if(inventory.slotObjects[inventory.slotSelected].tag == "Document1" || inventory.slotObjects[inventory.slotSelected].tag == "Document2" || inventory.slotObjects[inventory.slotSelected].tag == "Document3"){
                paperDocument.sprite = inventory.slotImages[inventory.slotSelected].sprite;
                if(documentVisible == false){
                    paperDocument.color = documentOn;
                    documentVisible = true;
                }
                else{
                    paperDocument.color = documentOff;
                    documentVisible = false;
                }
            }
        }
    }

    public IEnumerator performAnimation(Vector3 pos, float animationTime, float returnTime, float scale, bool snapping, bool light){
        animatedItem.transform.DOMove(pos, animationTime, snapping);
        animatedItem.transform.DOScale(new Vector3(originalScale.x + scale, originalScale.y + scale, originalScale.z + scale), animationTime);
        if(light == true){
            playerLight.intensity = flashLightIntensity * 2;
            playerLight.color = Color.white;
        }
        yield return new WaitForSeconds(animationTime);
        animatedItem.transform.DOScale(originalScale, animationTime);
        animatedItem.transform.DOMove(originalPos, returnTime, snapping);
        if(light == true){
            playerLight.intensity = noLightIntensity;
            playerLight.color = noFlashLight;
        }

    }

    public IEnumerator moveDoor(GameObject door, float amount, float time, bool retreat){
        door.transform.DOLocalMoveZ(amount, time, false);
        if(retreat == true){
            yield return new WaitForSeconds(time * 2.3f);
            door.transform.DOLocalMoveZ(0, time, false);
        }

    }

    public IEnumerator reloadAnimation(){

        //--- picking up gun ---
        ammoBar.value = 0;
        animatedItem.transform.DOMove(reloadPos, 0.7f, false);
        animatedItem.transform.DORotate(reloadAngle, 0.7f, RotateMode.Fast);
        StartCoroutine(playerRaycast.interactWithTag("You reload your gun."));

        //--- shaking gun ---
        yield return new WaitForSeconds(0.7f);

        //the act of moving pistol shake to lower part
        animatedItem.transform.DOMove(reloadPosTwo, 0.2f, false).OnComplete(() => {
            //enabling the magazine and moving it to the "magazine" part of pistol position
            magItem.enabled = true;

            //the act of moving shake to top part
            animatedItem.transform.DOMove(reloadPos, 0.2f, false);

            //reload mag animation
            //magItem.transform.DOLocalMove(magReloadPos, 0.4f, false);
            magItem.transform.DOMove(magReloadPos, 0.35f, false).OnComplete(() => {
                magItem.transform.DOLocalMove(magPos, 1f, false);
            });
        });

        //--- putting gun back ---
        yield return new WaitForSeconds(1.2f);

        //resets magazine pos and angle
        magItem.enabled = false;
        //magItem.transform.position = magOriginalPos;
        animatedItem.transform.DOMove(originalPos, 0.8f, false);
        animatedItem.transform.DORotate(regularAngle, 0.8f, RotateMode.Fast);
        ammoBar.value = 5;

    }

    IEnumerator flashMuzzle(){
        muzzleflash.enabled = true;
        yield return new WaitForSeconds(0.05f);
        muzzleflash.enabled = false;
    }

}
