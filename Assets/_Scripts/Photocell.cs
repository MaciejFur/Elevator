using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photocell : MonoBehaviour
{
    [SerializeField] private GameObject doors;
    [SerializeField] private bool playerNear;
    [SerializeField] private Animator anim;

    private void Start()
    {
        //StartCoroutine(CloseTheDoor());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNear = true;
            if (!anim.GetBool("Opened") && !anim.GetBool("FullyOpened"))
            {
                anim.SetBool("Opened", true);
                anim.SetTrigger("DoorTrigger");   
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    IEnumerator CloseTheDoor()
    {
        while (true)
        {
            if (!playerNear && anim.GetBool("Opened"))
            {
                bool isOpened = anim.GetBool("Opened");
                anim.SetBool("Opened", false);
                anim.SetBool("FullyOpened", false);
                if (isOpened != false)
                {
                    anim.SetTrigger("DoorTrigger");
                }
            }

            yield return new WaitForSeconds(6f);
        }
    }
}
