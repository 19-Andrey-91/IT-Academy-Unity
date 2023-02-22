using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FlickerLigth : MonoBehaviour
{
    [SerializeField] private GameObject lighting;
    [SerializeField] private float minFlickerSpeed;
    [SerializeField] private float maxFlickerSpeed;

    public bool IsOn { get; set; } = true;

    private void Update()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (IsOn)
        {
            lighting.SetActive(true);
            yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
            lighting.SetActive(false);
            yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        }
    }
}
