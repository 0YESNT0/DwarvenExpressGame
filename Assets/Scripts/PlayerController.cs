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
    public MenuButtonScript MenuScript;    
    float rotationX = 0;
    [Header("Values")]        
    public float gravity = 20.0f;
    public float gravityscale = 1.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Header("Player Base Stats")]
    public float walkingSpeed;
    public int inventorySlots = 2;
    public int WeightCapBase = 50;
    public float jumpHeight;

    [Header("Player Stats Increase Value")]
    public float walkingspeedIncrease = 0.25f;
    public int inventorySlotsIncrease = 1;
    public int WeightCapIncrease = 25;
    public float JumpHeightIncrease = 0.25f;

    [Header("Player Stats Modifier")]
    public float walkingspeedMod = 0;
    public int inventorySlotsMod = 0;
    public int WeightCapMod = 0;
    public float JumpHeightMod = 0;

    public int currentWeight = 0;
    public float currentwalkSpeed = 0;

    public float overEncumberedSpeedReduction = 50;

    [HideInInspector]
    public bool canMove = true;
    public bool canJump = true;
 

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();                
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        currentWeight = 0;                     
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.y <= 0.6)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 forward = transform.TransformDirection(UnityEngine.Vector3.forward);
        UnityEngine.Vector3 right = transform.TransformDirection(UnityEngine.Vector3.right);
        if(currentWeight > (WeightCapBase + WeightCapMod)){
            currentwalkSpeed = (walkingSpeed + walkingspeedMod) * (overEncumberedSpeedReduction/100);
        }
        else{
            currentwalkSpeed = walkingSpeed + walkingspeedMod;
        }
           
        float cursSpeedX = canMove ? currentwalkSpeed * Input.GetAxis("Vertical") : 0;
        float cursSpeedY = canMove ? currentwalkSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * cursSpeedX) + (right * cursSpeedY);
        moveDirection.y = movementDirectionY;
        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
        

        if(canMove){
            if(Input.GetButton("Jump") && characterController.isGrounded && canJump){
                moveDirection.y = jumpHeight + JumpHeightMod;
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
            if(Input.GetKey(KeyCode.Escape)){
                MenuScript.PauseGame();
            }
        }

        


    }

    public void SetCurrentWeight(int totalweight){
        currentWeight = totalweight;
    }
    //checks for closest interactable object
}
