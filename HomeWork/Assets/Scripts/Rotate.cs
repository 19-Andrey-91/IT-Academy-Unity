using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] private float _speedX = 45;
    [SerializeField] private float _speedY = 45;
    [SerializeField] private float _speedZ = 45;

    void Update()
    {
        transform.Rotate(new Vector3(_speedX, _speedY, _speedZ) * Time.deltaTime);
    }
}
