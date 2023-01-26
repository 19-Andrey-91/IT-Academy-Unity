using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float _timeBetweenScale = 3;
    [SerializeField] private float _speedScale = 1;

    [Header("X-scale")]
    [SerializeField] private float _maxScaleX = 5;
    [SerializeField] private float _minScaleX = 1;

    [Header("Y-scale")]
    [SerializeField] private float _maxScaleY = 5;
    [SerializeField] private float _minScaleY = 1;

    [Header("Z-scale")]
    [SerializeField] private float _maxScaleZ = 5;
    [SerializeField] private float _minScaleZ = 1;

    private Vector3 _scale;
    private Vector3 _newScale;

    private float _time;

    private void Start()
    {
        if (_maxScaleX < _minScaleX)
            (_maxScaleX, _minScaleX) = (_minScaleX, _maxScaleX);
        if (_maxScaleY < _minScaleY)
            (_maxScaleY, _minScaleY) = (_minScaleY, _maxScaleY);
        if (_maxScaleZ < _minScaleZ)
            (_maxScaleZ, _minScaleZ) = (_minScaleZ, _maxScaleZ);

        _scale = new Vector3(_minScaleX, _minScaleY, _minScaleZ);
        _newScale = new Vector3(_maxScaleX, _maxScaleY, _maxScaleZ);
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
