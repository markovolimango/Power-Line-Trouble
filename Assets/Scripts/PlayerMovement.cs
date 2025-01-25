using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private bool _isStopped;
    private Vector2 _moveDir, _inputDir;
    private NodeManager _nodeManager;
    private Rigidbody2D _rigidbody;
    private int i, j;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.right;
        _inputDir = Vector2.right;
        _nodeManager = FindObjectOfType<NodeManager>();
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
        _moveDir = CalculateMoveDir(_inputDir);
    }

    private Vector2 CalculateMoveDir(Vector2 dir)
    {
        var start = _nodeManager.NodePositions[i, j];
        if (j + dir.x < 0 || i - dir.y < 0 || j + dir.x >= _nodeManager.n || i - dir.y >= _nodeManager.m)
            return Vector2.zero;
        j += (int)dir.x;
        i -= (int)dir.y;
        print(i + " " + j);
        return (_nodeManager.NodePositions[i, j] - start).normalized;
    }
}