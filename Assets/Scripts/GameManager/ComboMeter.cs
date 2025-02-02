using System;
using TMPro;
using UnityEngine;

public class ComboMeter : MonoBehaviour
{
    public int loseTime;
    private AudioSource _audioSource;
    [NonSerialized] public int Combo;
    private int _loseTimer;
    private GameObject _player;
    public TMP_Text comboText;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        comboText.text = "";
        _loseTimer = 0;

    }

    public void OnBoom()
    {
        if (_loseTimer > 0)
        {
            _loseTimer--;
            return;
        }
        comboText.text = "";
        Combo = 0;
    }

    public void OnBirdHit()
    {
        Combo++;
        _loseTimer = loseTime;
        comboText.text = Combo.ToString();
    }
}