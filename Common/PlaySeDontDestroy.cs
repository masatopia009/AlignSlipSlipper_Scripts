using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySeDontDestroy : SingletonDontDestroy<PlaySeDontDestroy>
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        if(audioClip != null)
            audioSource.PlayOneShot(audioClip);
    }
}
