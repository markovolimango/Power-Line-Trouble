using UnityEngine;

namespace Shaders
{
    public class LeftToRightColorChangeShaderController : MonoBehaviour
    {
        private Material material;
        private float transitionProgress = 0f;
        private bool isTransitioning = false;

        [SerializeField] private float transitionSpeed = 1f; // Adjust speed in Inspector
        [SerializeField] private float smoothness = 0.1f; // Adjust smoothness in Inspector
        [SerializeField] private Color startColor = Color.yellow; // Start color (yellow by default)

        private bool transitioningToYellow = true; // Keeps track of whether we are transitioning to yellow or not

        void Start()
        {
            material = GetComponent<SpriteRenderer>().material;
            material.SetColor("_StartColor", startColor); // Set start color
            material.SetFloat("_Smoothness", smoothness); // Set smooth transition
            material.SetFloat("_TransitionMode", 1f); // Set to original color mode
            material.SetFloat("_TransitionProgress", 1f); // Set transition progress to 1 (original color)
        }

        void Update()
        {
            if (isTransitioning)
            {
                transitionProgress = Mathf.Clamp01(transitionProgress + Time.deltaTime * transitionSpeed);
                material.SetFloat("_TransitionProgress", transitionProgress);

                // If we've reached the yellow color, switch to transitioning back to original color
                if (transitionProgress >= 1f && transitioningToYellow)
                {
                    transitioningToYellow = false;
                    material.SetFloat("_TransitionMode", 1f); // Set mode to transition back to the original color
                    transitionProgress = 0f; // Reset transition progress for the reverse transition
                }
                // If we've reached the original color, stop the transition
                if (!transitioningToYellow && transitionProgress >= 1f)
                {
                    ResetTransition();
                }
            }
        }

        public void StartTransition()
        {
            isTransitioning = true;
            transitionProgress = 0f; // Reset progress
            material.SetFloat("_TransitionMode", 0f); // Start transitioning to yellow
            transitioningToYellow = true; // Ensure we're transitioning to yellow initially
        }

        public void ResetTransition()
        {
            isTransitioning = false;
            transitionProgress = 0f; // Reset progress
            material.SetFloat("_TransitionProgress", transitionProgress);
            material.SetFloat("_TransitionMode", 1f); // Set to original color mode
            material.SetFloat("_TransitionProgress", 1f); // Set transition progress to 1 (original color)
        }

        // Set the transition speed dynamically
        public void SetTransitionSpeed(float speed)
        {
            transitionSpeed = speed;
        }
    }
}