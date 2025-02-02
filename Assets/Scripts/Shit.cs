using UnityEngine;

namespace DefaultNamespace
{
    public class Shit : MonoBehaviour
    {
        public int damage;
        private AudioSource _audio;

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            _audio.pitch = Random.Range(0.9f, 1.1f);
            _audio.Play();
        }
    }
}