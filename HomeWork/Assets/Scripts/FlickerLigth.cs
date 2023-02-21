using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FlickerLigth : MonoBehaviour
{
    [SerializeField] private GameObject lighting;
    [SerializeField, Range(0, 100)] private int flickerIntensity;

    public bool IsOn { get; set; } = true;

    private void FixedUpdate()
    {
        if(IsOn)
        {
            if(Random.Range(0, 100) < flickerIntensity)
            {
                lighting.SetActive(true);
            }
            else
            {
                lighting.SetActive(false);
            }
        }
    }
}
