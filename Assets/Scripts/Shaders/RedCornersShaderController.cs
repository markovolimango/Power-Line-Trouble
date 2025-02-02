using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RedCornersShaderController: MonoBehaviour
{
    [SerializeField] private Material pulseEffectMaterial;
    public float pulseSpeed = 2.0f;
    public float redIntensity = 0.8f;

    private static readonly int PulseSpeedProperty = Shader.PropertyToID("_PulseSpeed");
    private static readonly int RedIntensityProperty = Shader.PropertyToID("_RedIntensity");

    private bool isEffectActive;

    private void Start()
    {
        PlayPulseEffect(10f);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (pulseEffectMaterial != null && isEffectActive)
        {
            pulseEffectMaterial.SetFloat(PulseSpeedProperty, pulseSpeed);
            pulseEffectMaterial.SetFloat(RedIntensityProperty, redIntensity);
            Graphics.Blit(source, destination, pulseEffectMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    public void PlayPulseEffect(float duration)
    {
        print("dnasjfjsdhnl");
        isEffectActive = true;
        Invoke(nameof(StopPulseEffect), duration);
    }

    private void StopPulseEffect()
    {
        isEffectActive = false;
    }
} 