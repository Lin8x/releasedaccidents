using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Joystick movejoystick;
    public PlayerJump jB;
    public PlayerCrouch jC;
    public PlayerSprint jS;
    public StaminaBar sB;
    public CharacterController controller;
    public bool isMoving;

    [SerializeField] public float speed = 6;
    public float jumpHeight = 2f;

    Vector3 velocity;
    public bool isGrounded;

    public float groundDistance = 0.4f;
    public float gravity = -19f;

    public Transform groundCheck;
    public LayerMask groundMask;

    public float jumpCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        groundCheck.position = new Vector3(transform.position.x, transform.position.y - controller.height/2, transform.position.z);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            
            velocity.y = -2f;

        }
        
        if(jC.pressed == true && isGrounded){
            Debug.Log("crouched");
            if(controller.height == 7.0f){
                gravity = -10f;
                speed = 2;
                velocity.y = Mathf.Sqrt(0.1f * -2 * gravity);
                controller.height = 4.0f;
                gravity = -19f;
            }
            else{
                gravity = -10f;
                speed = 6;
                velocity.y = Mathf.Sqrt(1.5f * -2 * gravity);
                controller.height = 7.0f;
                gravity = -19f;
            }
        }

        if(jS.pressed == true && isGrounded && controller.height == 7.0f){
            Debug.Log("stamina activated");
            sB.isUsed = !sB.isUsed;
        }

        if(jB.pressed == true && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //Vector3 move = new Vector3(movejoystick.Horizontal / slowspeed, 0f, movejoystick.Vertical / slowspeed);
        float x = movejoystick.Horizontal;
        float z = movejoystick.Vertical;
        Vector3 move = transform.right * x + transform.forward * z;

        if(movejoystick.Horizontal > 0 || movejoystick.Vertical > 0){
            isMoving = true;
        }
        else{
            isMoving = false;
        }

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

}
