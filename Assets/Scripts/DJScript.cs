using UnityEngine;
using UnityEngine.Events;

public class DJScript : MonoBehaviour
{
    public AudioClip boomClip, tskClip;
    public float beatTime;
    public UnityEvent boom, tsk;
    private AudioSource _audioSource;
    private float _beatTimer;
    private bool _isBoom;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _beatTimer = beatTime;
    }

    private void FixedUpdate()
    {
        if (_beatTimer > 0)
        {
            _beatTimer -= Time.deltaTime;
        }
        else
        {
            if (_isBoom)
            {
                _audioSource.clip = boomClip;
                _audioSource.Play();
                boom.Invoke();
            }
            else
            {
                _audioSource.clip = tskClip;
                _audioSource.Play();
                tsk.Invoke();
            }

            _isBoom = !_isBoom;
            _beatTimer = beatTime;
        }
    }
}