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
    public UnityEvent boom, tsk;
    private AudioSource _audioSource;
    private float _beatTimer;
    private bool _isBoom;
    private Animator _animator;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _beatTimer = beatTime;
        _isBoom = true;
        StartCoroutine(TimePassed());
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
                //_audioSource.clip = tskClip;
                //_audioSource.Play();
                //print("TSK");
                tsk.Invoke();
            }

            _isBoom = !_isBoom;
        }
    }
}