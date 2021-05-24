using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private Vector3[] levels;
    [SerializeField] private Animator anim;
    [SerializeField] private float elevatorSpeed;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource doorAudioSource;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private TextMeshProUGUI uiText;
    
    //[SerializeField] private Transform child;
    
    private Vector3 curPos;
    private Vector3 lastPos;

    public int floorNum;
    public int floorCalled;
    
    /*private void Start()
    {
        child.SetParent(transform);
    }*/
    
    void Update()
    {

        if (anim.GetBool("IsMoving"))
        {
            ElevatorMovement(floorCalled);
            UpdateCurrentFloor();
        }
        

        // if elevator is not moving, proceed with door opening sequence
        curPos = transform.position;
        if(curPos == lastPos && anim.GetBool("IsMoving"))
        {
            //doorAudioSource.Stop();
            anim.SetBool("IsMoving", false);
            if (floorNum == floorCalled)
            {
                anim.SetBool("Opened", true);
                anim.SetTrigger("DoorTrigger");
            }
            source.PlayOneShot(clips[0]);
        }
        lastPos = curPos;

    }

    public void ElevatorMovement(int destination)
    {
        // move to selected floor
        if (!anim.GetBool("IsMoving"))
            anim.SetBool("IsMoving", true);
        
        if (destination < floorNum)
        {
            if (transform.position.y > levels[destination].y)
            {
                transform.Translate(-Vector3.up * (elevatorSpeed * Time.deltaTime));
                //child.Translate(-Vector3.up * (elevatorSpeed * Time.deltaTime));
            }   
        }
        else if (destination > floorNum)
        {
            if (transform.position.y < levels[destination].y)
            {
                transform.Translate(Vector3.up * (elevatorSpeed * Time.deltaTime));
                //child.Translate(Vector3.up * (elevatorSpeed * Time.deltaTime));
            }
                
        }
    }

    private void UpdateCurrentFloor()
    {
        // returns floor number based on position.y of the elevator
        int approxPos = Mathf.RoundToInt(transform.position.y);
        
        if (approxPos <= 0)
            floorNum = 0;
        else if (approxPos == 6)
            floorNum = 1;
        else if (approxPos == 12)
            floorNum = 2;
        else if (approxPos == 18)
            floorNum = 3;
        else if (approxPos == 24)
            floorNum = 4;

        uiText.text = floorNum.ToString();
    }
}
