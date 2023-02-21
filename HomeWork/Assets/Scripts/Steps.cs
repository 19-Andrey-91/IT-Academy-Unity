using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    [SerializeField] private Character character;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        character.OnWalks += PlayStep;
    }

    private void OnDisable()
    {
        character.OnWalks -= PlayStep;
    }

    private void PlayStep(bool isWalk)
    {
        if(isWalk)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
