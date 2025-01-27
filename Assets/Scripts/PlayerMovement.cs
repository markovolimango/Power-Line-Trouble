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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
            _inputDir = Vector2.up;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            _inputDir = Vector2.down;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            _inputDir = Vector2.left;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
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
            _inputDir*=-1;
        if (_inputDir==Vector2.up) _gridManager.VerticalBranches[_posY-1,_posX].KillBirds();
        if (_inputDir==Vector2.down) _gridManager.VerticalBranches[_posY,_posX].KillBirds(); 
        if (_inputDir==Vector2.right) _gridManager.HorizontalBranches[_posY,_posX].KillBirds(); 
        if (_inputDir==Vector2.left) _gridManager.HorizontalBranches[_posY,_posX-1].KillBirds(); 
        _posX += (int)_inputDir.x;
        _posY -= (int)_inputDir.y;
        _rigidbody.position = _gridManager.NodePositions[_posY, _posX];
    }
}