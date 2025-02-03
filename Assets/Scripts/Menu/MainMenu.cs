using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip[] birdSounds;
    public AudioClip fart;
    private AudioSource _audio;

    private void Start()
    {
        gameObject.SetActive(true);
        _audio = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnHover()
    {
        _audio.clip = birdSounds[Random.Range(0, birdSounds.Length)];
        _audio.pitch = Random.Range(0.9f, 1.1f);
        _audio.Play();
    }

    public void PlayBigFart()
    {
        _audio.clip = fart;
        _audio.Play();
    }
}