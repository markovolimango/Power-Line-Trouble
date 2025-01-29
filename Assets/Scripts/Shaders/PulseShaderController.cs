using UnityEngine;
using System.Collections;

namespace Shaders
{
    public class PulseShaderController : MonoBehaviour
    {
        private Material material;
        private float hitEffect = 0f;
        private bool isHit = false;

        [SerializeField] private Color hitColor = new Color(1f, 73f/255f, 73f/255f); // Set any color in the Inspector
        [SerializeField] private float pulseSpeed = 5f; // Adjust pulse speed in Inspector
        [SerializeField] private float pulseDuration = 0.3f; // Adjust pulse duration in Inspector
        [SerializeField] private float pulseInterval = 0.3f; // Adjust pulse interval in Inspector

        void Start()
        {
            material = GetComponent<SpriteRenderer>().material;
            material.SetColor("_HitColor", hitColor); // Apply initial color
        }

        void FixedUpdate()
        {
            if (isHit)
            {
                hitEffect = Mathf.Clamp01(hitEffect + Time.fixedDeltaTime * pulseSpeed);  // Increase effect
            }
            else
            {
                hitEffect = Mathf.Clamp01(hitEffect - Time.fixedDeltaTime * pulseSpeed);  // Fade back
            }

            material.SetFloat("_HitEffect", hitEffect);
        }

        public void Pulse(int n)
        {
            StartCoroutine(PulseCoroutine(n));
        }

        private IEnumerator PulseCoroutine(int n)
        {
            for (int i = 0; i < n; i++)
            {
                isHit = true;
                yield return new WaitForSeconds(pulseDuration); 
                isHit = false;
                yield return new WaitForSeconds(pulseInterval); 
            }
        }

        private void ResetHitEffect()
        {
            isHit = false;
        }

        // Optional: Change hit color at runtime
        public void SetHitColor(Color newColor)
        {
            hitColor = newColor;
            material.SetColor("_HitColor", hitColor);
        }
    }
}