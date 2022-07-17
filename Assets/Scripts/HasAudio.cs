using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class HasAudio : MonoBehaviour
{

    private AudioSource Audio;


    virtual protected void Start()
    {
        Audio = GetComponent<AudioSource>();
        EventSystem.OnVolumeChange += OnVolumeChanged;
        Audio.volume = GameLogic.Volume;
    }

    private void OnVolumeChanged(object sender, float e)
    {
        Audio.volume = e;
    }

    virtual protected void OnDestroy()
    {
        EventSystem.OnVolumeChange -= OnVolumeChanged;
    }

    public void Play()
    {
        if (Audio.clip)
        {
            Audio.Play();
            EventSystem.LaunchEvent(Audio, "audio.play", Audio.clip.name);
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        Audio.PlayOneShot(clip);
        EventSystem.LaunchEvent(Audio, "audio.play.one", clip.name);
    }

    public bool IsPlaying => Audio.isPlaying;
}
