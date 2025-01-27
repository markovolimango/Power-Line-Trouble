using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class DJScript : MonoBehaviour
{
    public AudioClip boomClip, tskClip, musicClip;
    public float beatTime;
    public int tskFrequency;
    public UnityEvent boom, tsk;
    private AudioSource _audioSource;
    private bool _isBoom;
    private Animator _animator;
    private int _tskTimer;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _isBoom = true;
        StartCoroutine(TimePassed());
        _tskTimer=tskFrequency;
    }

    private void PlayMusic()
    {
        _audioSource.clip = musicClip;
        _audioSource.Play();
    }
    
    IEnumerator TimePassed()
    {
        PlayMusic();
        yield return new WaitForSeconds(beatTime);
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
                    print("TSK");
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