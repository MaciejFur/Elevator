using System;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public string displayedText = "Press \"E\" to call the elevator";

    [SerializeField] private int floor;
    [SerializeField] private ElevatorController elevator;
    [SerializeField] private AudioSource source;
    [SerializeField] private Animator anim;
    [SerializeField] private Material[] _materials;
    private void Update()
    {
        if (elevator.floorNum == floor)
            gameObject.GetComponent<Renderer>().material = _materials[1];
        if (elevator.floorNum != floor)
            gameObject.GetComponent<Renderer>().material = _materials[0];
    }

    public void CallElevator()
    {
        if (elevator.floorNum != floor)
        {
            anim.SetBool("Opened", false);
            anim.SetTrigger("DoorTrigger");
            source.Play();
            elevator.floorCalled = floor;
        }
    }

    /*public void SelectFloor()
    {

        if (elevator.floorNum != floor)
        {
            anim.SetBool("Opened", false);
            anim.SetTrigger("DoorTrigger");
            source.Play();
            elevator.floorCalled = floor;   
        }
    }*/
}
