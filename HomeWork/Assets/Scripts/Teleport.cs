using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private float _time;

    private float _timer;

    void Update()
    {
        if((_timer += Time.deltaTime) > _time)
        {
            transform.position = new Vector3(Random.Range(-5f,5f), 0.5f, Random.Range(-5f,5f));
            _timer = 0;
        }
    }
}
