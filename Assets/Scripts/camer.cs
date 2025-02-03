using Cinemachine;
using UnityEngine;

public class camer : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin _noise;
    private float _timer;
    private CinemachineVirtualCamera _vCam;

    private void Start()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _noise = _vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StopShakingIt();
    }

    private void FixedUpdate()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
            StopShakingIt();
    }

    public void ShakeIt(float intensity, float time)
    {
        _noise.m_AmplitudeGain = intensity;
        _timer = time;
    }

    public void StopShakingIt()
    {
        _noise.m_AmplitudeGain = 0f;
        _timer = 0;
    }
}