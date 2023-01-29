using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float _timeBetweenScale = 3;
    [SerializeField] private float _speedScale = 1;

    [SerializeField] private Vector3 _maxScale = new(5, 5, 5);
    [SerializeField] private Vector3 _minScale = new(1, 1, 1);

    private Vector3 _scale;
    private Vector3 _newScale;

    private float _time;

    private void Start()
    {
        if (_maxScale.x < _minScale.x)
            (_maxScale.x, _minScale.x) = (_minScale.x, _maxScale.x);
        if (_maxScale.y < _minScale.y)
            (_maxScale.y, _minScale.y) = (_minScale.y, _maxScale.y);
        if (_maxScale.z < _minScale.z)
            (_maxScale.z, _minScale.z) = (_minScale.z, _maxScale.z);

        _scale = _minScale;
        _newScale = _maxScale;
        _time = _timeBetweenScale;
    }
    void Update()
    {
        _time -= Time.deltaTime;
        transform.localScale = Vector3.Lerp(transform.localScale, _newScale, Time.deltaTime * _speedScale);
        if (_time <= 0)
        {
            (_scale, _newScale) = (_newScale, _scale);
            _time = _timeBetweenScale;
        }
    }
}
