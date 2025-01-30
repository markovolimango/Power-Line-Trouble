using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DJScript : MonoBehaviour
{
    public AudioClip boomClip, tskClip, musicClip, loopClip;
    public float beatTime;
    public int tskFrequency;
    public UnityEvent boom, tsk;
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isBoom;
    private int _tskTimer;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _isBoom = true;
        StartCoroutine(TimePassed());
        _tskTimer = tskFrequency;
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
           // PlayLoop();
        }
    }

    private void PlayMusic()
    {
        _audioSource.clip = musicClip;
        _audioSource.Play();
    }

    private void PlayLoop()
    {
        _audioSource.clip = loopClip;
        _audioSource.Play();
    }

    private IEnumerator TimePassed()
    {
        PlayMusic();
        yield return new WaitForSeconds(2f);
        while (true)
        {
            yield return new WaitForSeconds(beatTime);
            if (_isBoom)
            {
                //_audioSource.clip = boomClip;
                //_audioSource.Play();
                //print("BOOM");
                boom.Invoke();
            }
            else
            {
                if (_tskTimer == 1)
                {
                    //_audioSource.clip = tskClip;
                    //_audioSource.Play();
                    //print("TSK");
                    tsk.Invoke();
                    _tskTimer = tskFrequency;
                }
                else
                {
                    _tskTimer--;
                }
            }

            _isBoom = !_isBoom;
        }
    }
}