using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Shake Settings Variables")]
    public float AmplitudeGain = 0;
    public float FrecuencyGain = 0;

    private float shakeTimer = 0;
    private float shakeTimerTotal;
    private float startingIntensity;

    CinemachineVirtualCamera levelCamera;

    public NoiseSettings noiseSettings;

    //GetComponent<CinemachineImpulseSource>().m_ImpulseDefinition.m_RawSignal = noiseSettings;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        CameraShaking();

        if (Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<CinemachineImpulseSource>().m_ImpulseDefinition.m_AmplitudeGain = 12f;
            GetComponent<CinemachineImpulseSource>().m_ImpulseDefinition.m_FrequencyGain = 1.5f;
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }

    public void CameraShake(float intensity, float frecuency, float duration)
    {
        CinemachineBasicMultiChannelPerlin CBMCP = levelCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CBMCP.m_FrequencyGain = frecuency;
        CBMCP.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = duration;
        shakeTimer = duration;
    }

    public void CameraShaking()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin CBMCP = levelCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            CBMCP.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }
}
