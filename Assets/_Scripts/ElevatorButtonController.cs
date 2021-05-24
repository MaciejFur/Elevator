using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorButtonController : MonoBehaviour
{
    public string displayedText = "Press \"E\" to call the elevator";
    public bool isDisplayed = false;

    [SerializeField] private float floorHeight;
    [SerializeField] private ElevatorController elevator;
    [SerializeField] private AudioSource source;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDisplayed)
        {
            source.Play();
            elevator.ElevatorMovement(0);
        }
    }
}
