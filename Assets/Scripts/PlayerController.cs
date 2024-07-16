using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [Header("Object References")]
    public Camera playerCamera;
    public RaycastController InteractSystem;
    CharacterController characterController;
    UnityEngine.Vector3 moveDirection = UnityEngine.Vector3.zero;
    float rotationX = 0;
    [Header("Values")]
    public float runningSpeed = 11.5f;
    public float walkingSpeed = 4.0f;
    public float jumpSpeed = 1.0f;
    public float gravity = 20.0f;
    public float gravityscale = 1.0f;
   
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Header("Player Stats")]
    public float walkingspeedMod = 0;
    public float inventorySlots = 2;
    public float WrightCapMod = 50;
    public float JumpHeightMod = 0;

    [HideInInspector]
    public bool canMove = true;
    

    //private variables
    //reference to location of where the player holds the key
 

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();                
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
    }
    
    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 forward = transform.TransformDirection(UnityEngine.Vector3.forward);
        UnityEngine.Vector3 right = transform.TransformDirection(UnityEngine.Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float cursSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float cursSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * cursSpeedX) + (right * cursSpeedY);
        moveDirection.y = movementDirectionY;
        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        
        

        if(canMove){
            if(Input.GetButton("Jump") && characterController.isGrounded){
                moveDirection.y = jumpSpeed;
            }
            else{
                moveDirection.y = movementDirectionY;
            }

            if(!characterController.isGrounded){
                moveDirection.y -= gravity * Time.deltaTime * gravityscale;
            }
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = UnityEngine.Quaternion.Euler(rotationX, 0 ,0);
            transform.rotation *= UnityEngine.Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0 );
        }


    }
    //checks for closest interactable object
    

    
}
