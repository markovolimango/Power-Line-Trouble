using UnityEngine;

namespace Shaders
{
    public class PulseShaderController : MonoBehaviour
    {
        private Material material;
        private float hitEffect = 0f;
        private bool isHit = false;

        [SerializeField] private Color hitColor = new Color(1f,73f/255f,73f/255f); // Set any color in the Inspector 255 73 73

        void Start()
        {
            material = GetComponent<SpriteRenderer>().material;
            material.SetColor("_HitColor", hitColor); // Apply initial color
        }

        void Update()
        {
            if (isHit)
            {
                hitEffect = Mathf.Clamp01(hitEffect + Time.deltaTime * 5f);  // Increase effect
            }
            else
            {
                hitEffect = Mathf.Clamp01(hitEffect - Time.deltaTime * 5f);  // Fade back
            }

            material.SetFloat("_HitEffect", hitEffect);
        }

        public void Pulse()
        {
            isHit = true;
            Invoke(nameof(ResetHitEffect), 0.3f); // Fade back after 0.3 seconds
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