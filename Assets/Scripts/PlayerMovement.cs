using Grid;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GridManager _gridManager;
    private bool _isStopped;
    private Vector2 _inputDir;
    private int _posY, _posX;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputDir = Vector2.zero;
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
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public void OnBoom()
    {
        if (_posX + _inputDir.x < 0 || _posY - _inputDir.y < 0 || _posX + _inputDir.x >= _gridManager.n ||
            _posY - _inputDir.y >= _gridManager.m)
            _inputDir=Vector2.zero;
        _posX += (int)_inputDir.x;
        _posY -= (int)_inputDir.y;
        _rigidbody.position = _gridManager.NodePositions[_posY, _posX];
    }
}