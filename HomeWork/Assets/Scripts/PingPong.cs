using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] private float _bottomEdge = -5;
    [SerializeField] private float _upperEdge = 5;
    [SerializeField] private float _leftEdge = -5;
    [SerializeField] private float _rightEdge = 5;
    [SerializeField] private float _speed = 5;

    private Vector3 _coordStartMove;
    private Vector3 _coordEndMove;

    private float _z { get => Random.Range(_bottomEdge, _upperEdge); }
    private bool _moveToLeft { get => _coordStartMove.x > _coordEndMove.x; }

    private void Start()
    {
        if (_rightEdge < _leftEdge)
        {
            (_rightEdge, _leftEdge) = (_leftEdge, _rightEdge);
        }
        _coordStartMove = new Vector3(_rightEdge, transform.position.y, _z);
        _coordEndMove = new Vector3(_leftEdge, transform.position.y, _z);

        transform.position = _coordStartMove;
    }

    void Update()
    {
        if (_coordStartMove.x == _coordEndMove.x) { return; }
        if (_moveToLeft && transform.position.x < _coordEndMove.x)
        {
            _coordStartMove = _coordEndMove;
            _coordEndMove = new Vector3(_rightEdge, transform.position.y, _z);
        }
        else if (!_moveToLeft && transform.position.x > _coordEndMove.x)
        {
            _coordStartMove = _coordEndMove;
            _coordEndMove = new Vector3(_leftEdge, transform.position.y, _z);
        }
        transform.position += (_coordEndMove - _coordStartMove).normalized * _speed * Time.deltaTime;
    }
}
