using Grid;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private GridManager _gridManager;
    private bool _isStopped;
    private Vector2 _moveDir, _inputDir;
    private int _posY, _posX;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.right;
        _inputDir = Vector2.right;
        _gridManager = FindFirstObjectByType<GridManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            _inputDir = Vector2.up;
        else if (Input.GetKey(KeyCode.S))
            _inputDir = Vector2.down;
        else if (Input.GetKey(KeyCode.A))
            _inputDir = Vector2.left;
        else if (Input.GetKey(KeyCode.D))
            _inputDir = Vector2.right;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveDir * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isStopped = true;
        _moveDir = Vector2.zero;
        _rigidbody.MovePosition(other.transform.position);
    }

    public void OnBoom()
    {
        if (!_isStopped)
            return;

        var startPos = _gridManager.NodePositions[_posY, _posX];
        if (_posX + _inputDir.x < 0 || _posY - _inputDir.y < 0 || _posX + _inputDir.x >= _gridManager.n ||
            _posY - _inputDir.y >= _gridManager.m)
            _inputDir *= -1;
        _posX += (int)_inputDir.x;
        _posY -= (int)_inputDir.y;
        _moveDir = (_gridManager.NodePositions[_posY, _posX] - startPos).normalized;
    }
}