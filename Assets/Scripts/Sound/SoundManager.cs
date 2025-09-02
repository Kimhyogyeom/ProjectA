using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxUISource;
    public AudioSource sfxSource;

    [Header("Database")]
    public SoundDatabase soundDatabase;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // SFX UI
    public void PlaySfxUI(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxUISource.PlayOneShot(clip, volume);
    }

    // SFX
    public void PlaySfx(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

}
