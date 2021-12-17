using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    public PlayerMove player;

    public Slider staminaBar;
    
    public float maxStamina = 10;

    //private WaitForSeconds regenTick = new WaitForSeconds(0.01f);

    public bool isUsed = false;

    void Start()
    { 
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    void Update()
    {
        if(isUsed){
            player.speed = 12;
            staminaBar.value-=Time.deltaTime;//-1 every sec
            
            if(staminaBar.value <= 0 || player.controller.height == 4.0f)
            {
                if(player.controller.height == 4.0f){
                    player.speed = 2;
                }
                else{
                    player.speed = 6;
                }
                isUsed = false;
            }
        }
        else
        {
            //regain stamina if not using it
            if(staminaBar.value < staminaBar.maxValue)
            {
                if(player.controller.height == 4.0f){
                    player.speed = 2;
                }
                else{
                    player.speed = 6;
                }
                staminaBar.value += Time.deltaTime; 
            }
            isUsed = false;
        }
    }
    
    //btw i got an error
    //it's not being referenced in player code for some reason
    //can u see it

    //we dont gotta do it this way imo

    // public void UseStamina(int amount)
    // {// i was thinking to make it easier. just add the boolean for when it's decreasing 
    // //and turn off boolean when it's increasing
    //     UseStam=true;
    //     if(currentStamina - amount >= 0){

    //         currentStamina -= amount;
    //         staminaBar.value = currentStamina;

    //         StartCoroutine(RegenStamina());

    //     }
    //     else{
    //         Debug.Log("not enough stamina");
    //     }
    // }

    // public IEnumerator UseAllStamina()
    // {
    //     while(staminaBar.value > 0){
    //         yield return new WaitForSeconds(0.4f);
    //         UseStamina(5);
    //     }
    //     yield return new WaitForSeconds(0.1f);
    // }

    // private IEnumerator RegenStamina()
    // {
    //     yield return new WaitForSeconds(2);

    //     while(currentStamina < maxStamina){
    //         currentStamina += maxStamina / 100;
    //         staminaBar.value = currentStamina;
    //         yield return regenTick;
    //     }
    // }

}
