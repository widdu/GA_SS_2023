using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    // Private variables
    private PlayerController playerController;
    private Vector2 moveInput;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(moveInput);
        if(moveInput == Vector2.zero && playerController.WaitForRelease)
        {
            playerController.WaitForRelease = false;
        }
        else if(moveInput != Vector2.zero && !playerController.WaitForRelease)
        {
            playerController.MovementSwitch(moveInput);
        }
    }

    public void Move(InputAction.CallbackContext callback)
    {
        moveInput = callback.ReadValue<Vector2>();
    }
}
