using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip audioClip2;
    [SerializeField] private AudioClip audioClip3;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
    }

    public void SetAudio(int num)
    {
        if (num == 1)
        {
            audioSource.clip = audioClip;
        } if (num == 2)
        {
            audioSource.clip = audioClip2;
        }
        if (num == 3)
        {
            audioSource.clip = audioClip3;
        }
    }

    public void Play()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public void Stop()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public bool IsPlaying()
    {
        return audioSource != null && audioSource.isPlaying;
    }
}