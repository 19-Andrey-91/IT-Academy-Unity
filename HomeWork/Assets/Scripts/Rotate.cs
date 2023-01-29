using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _speed = new(45, 45, 45);

    void Update()
    {
        transform.Rotate(_speed * Time.deltaTime);
    }
}
