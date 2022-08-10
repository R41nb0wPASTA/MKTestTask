using UnityEngine;

public class AudioController_Ingosick : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip stepOneAudio;
    [SerializeField] private AudioClip stepFourAudio;

    private void StepOneAudio()
    {
        audioSource.clip = stepOneAudio;
        audioSource.Play();
    }
    
    private void StepFourAudio()
    {
        audioSource.clip = stepFourAudio;
        audioSource.Play();
    }
}
