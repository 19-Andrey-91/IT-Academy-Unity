using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] private Vector2 _horizontalEdges = new(-5, 5);
    [SerializeField] private Vector2 _verticalEdges = new(-5, 5);
    [SerializeField] private float _speed = 5;

    private Vector3 _coordStartMove;
    private Vector3 _coordEndMove;

    private float RandomZ { get => Random.Range(_verticalEdges.x, _verticalEdges.y); }
    private bool MoveToLeft { get => _coordStartMove.x > _coordEndMove.x; }

    private void Start()
    {
        if (_horizontalEdges.y < _horizontalEdges.x)
        {
            (_horizontalEdges.y, _horizontalEdges.x) = (_horizontalEdges.x, _horizontalEdges.y);
        }
        _coordStartMove = new Vector3(_horizontalEdges.y, transform.position.y, RandomZ);
        _coordEndMove = new Vector3(_horizontalEdges.x, transform.position.y, RandomZ);

        transform.position = _coordStartMove;
    }

    void Update()
    {
        if (_coordStartMove.x == _coordEndMove.x) { return; }
        if (MoveToLeft && transform.position.x < _coordEndMove.x)
        {
            ChangeDirection(_horizontalEdges.y);
        }
        else if (!MoveToLeft && transform.position.x > _coordEndMove.x)
        {
            ChangeDirection(_horizontalEdges.x);
        }
        transform.position += (_coordEndMove - _coordStartMove).normalized * _speed * Time.deltaTime;
    }

    void ChangeDirection(float edge)
    {
        _coordStartMove = _coordEndMove;
        _coordEndMove = new Vector3(edge, transform.position.y, RandomZ);
    }
}
