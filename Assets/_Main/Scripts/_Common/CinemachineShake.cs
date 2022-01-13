using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] private GameObject cmCam;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin channelPerlin;
    private float shakerTimer = 0;
    private float shakeTimerTotal = 0;
    private float startingIntensity = 0;
    

    public static CinemachineShake Instance;

    private void Awake()
    {
        cinemachineVirtualCamera = cmCam.GetComponent<CinemachineVirtualCamera>();
        channelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }     
    }

    public void ShakeCamera(float frequency ,float intensity, float time)
    {
        channelPerlin.m_FrequencyGain = frequency;
        channelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakerTimer = time;
    }

    public void Stop()
    {
        ShakeCamera(0, 0, 0);       
    }

    private void Update()
    {
        if (shakerTimer > 0)
        {
            shakerTimer -= Time.deltaTime;

            if (shakerTimer <= 0f)
            {
                channelPerlin.m_AmplitudeGain = 0f;
                shakerTimer = 0;
            }
        }
    }
}
