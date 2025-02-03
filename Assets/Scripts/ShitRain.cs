using TMPro;
using UnityEngine;

public class ShitRain : MonoBehaviour
{
    public GameObject[] shits;
    public float shitTime, fartTime, megaShitTime;
    public GameObject ps;
    public TMP_Text scoreText;
    private AudioSource _audio;
    private camer _cam;
    private float _shitTimer, _fartTimer, _megaShitTimer;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _cam = FindFirstObjectByType<camer>();
        FindFirstObjectByType<ScoreTransfer>().UpdateText(scoreText);
    }

    private void FixedUpdate()
    {
        if (_shitTimer > 0)
        {
            _shitTimer -= Time.deltaTime;
        }
        else
        {
            var offset = Vector3.right * Random.Range(-2.0f, 2.0f);
            Instantiate(shits[Random.Range(0, shits.Length)], transform.position + offset, transform.rotation);
            _shitTimer = shitTime;
        }

        if (_fartTimer > 0)
        {
            _fartTimer -= Time.deltaTime;
        }
        else
        {
            _audio.pitch = Random.Range(0.6f, 1.4f);
            _audio.Play();
            _fartTimer = fartTime;
        }

        if (_megaShitTimer > 0)
        {
            _megaShitTimer -= Time.deltaTime;
        }
        else
        {
            var mega = Instantiate(ps, transform.position, transform.rotation);
            _cam.ShakeIt(2 * (2.0f - _audio.pitch), 0.7f * (2 - _audio.pitch));
            mega.GetComponent<ParticleSystem>().Play();
            _megaShitTimer = megaShitTime;
        }
    }
}