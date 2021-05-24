using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private TextMeshProUGUI uiText;
    
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float rayRange = 3f;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    
    private Vector3 velocity;
    private bool isGrounded;
    private bool keyDown;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            keyDown = true;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * (speed * Time.deltaTime));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        ShootRay();
    }

    private void ShootRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, rayRange))
        {
           // check whether it's a button or not 
            
            if (hit.transform.gameObject.CompareTag("Button"))
            {
                ElevatorButtons(hit);
            }
            else if(!hit.transform.gameObject.CompareTag("Button") && uiText.text != "")
            {
                uiText.text = "";
            }
        }
        else if(uiText.text != "")
        {
            uiText.text = "";
        }
    }

    private void ElevatorButtons(RaycastHit hit)
    {
        
        Debug.Log(hit.transform.name);
        if (hit.transform.gameObject.CompareTag("Button"))
        {
            // if it's a button to call an elevator
            //if (Input.GetKey(KeyCode.E))
            //{
                if(keyDown)
                {
                    keyDown = false;
                    hit.transform.gameObject.GetComponent<ButtonController>().CallElevator();
                }
            //}
            uiText.text = hit.transform.
                    gameObject.GetComponent<ButtonController>().displayedText;
        }
    }

    
}

