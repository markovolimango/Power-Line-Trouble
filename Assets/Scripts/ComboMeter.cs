using UnityEngine;

public class ComboMeter : MonoBehaviour
{
    public int loseTime;
    private AudioSource _audioSource;
    private int _combo;
    private int _loseTimer;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnBoom()
    {
        if (_loseTimer > 0)
        {
            _loseTimer--;
            return;
        }

        _player.transform.localScale = Vector3.one * 0.5f;
        if (_combo != 0)
        {
            _combo = 0;
            _audioSource.Play();
        }
    }

    public void OnBirdHit()
    {
        _combo++;
        _loseTimer = loseTime;
        if (_combo % 10 == 0)
            _player.transform.localScale *= 1.2f;
    }
}