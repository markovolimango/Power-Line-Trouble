using Grid;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private AudioSource _audioSource;
    private GridManager _grid;
    private Vector2 _inputDir;
    private bool _isStopped;
    private int _posY, _posX;

    private void Start()
    {
        _inputDir = Vector2.zero;
        _grid = FindFirstObjectByType<GridManager>();
        _audioSource = GetComponent<AudioSource>();
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

    public void OnBoom()
    {
        if (_posX + _inputDir.x < 0 || _posY - _inputDir.y < 0 || _posX + _inputDir.x >= _grid.n ||
            _posY - _inputDir.y >= _grid.m)
            _inputDir *= -1;
        if (_inputDir == Vector2.up) _grid.VerticalBranches[_posY - 1, _posX].KillBirds(true, 0.01f);
        if (_inputDir == Vector2.down) _grid.VerticalBranches[_posY, _posX].KillBirds(false, 0.01f);
        if (_inputDir == Vector2.right) _grid.HorizontalBranches[_posY, _posX].KillBirds(true, 0.5f);
        if (_inputDir == Vector2.left) _grid.HorizontalBranches[_posY, _posX - 1].KillBirds(false, 0.5f);
        _posX += (int)_inputDir.x;
        _posY -= (int)_inputDir.y;
        transform.position = _grid.NodePositions[_posY, _posX];
        if (_grid.NodeIsWatched[_posY, _posX])
        {
            print("EJ! " + _posY + " " + _posX + " " + _grid.NodeIsWatched[_posY, _posX]);
            _grid.ScareBirds();
        }

        _audioSource.Play();
    }
}