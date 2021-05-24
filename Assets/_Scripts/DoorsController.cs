using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorsController : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] private AudioClip[] doorSounds;
    [SerializeField] private Animator anim;
    [SerializeField] private float volume;

    private void Start()
    {
        anim.SetTrigger("DoorTrigger");
    }

    private void PlaySound(int index)
    {
        audioSource.pitch = Random.Range(.9f, 1.1f);
        audioSource.PlayOneShot(doorSounds[index], volume);        
    }

    private void FullyOpened(int opened)
    {
        anim.SetBool("FullyOpened", (opened != 0));
    }
    private void FullyClosed()
    {
        anim.SetBool("IsMoving", true);
        StartCoroutine(PlaySoundCoroutine());
    }
    IEnumerator PlaySoundCoroutine()
    {
        yield return new WaitForSeconds(.25f);
        audioSource.PlayOneShot(doorSounds[2], volume);
    }
    
}
