using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource[] SoundEffects;

    public AudioSource[] BackgroundMusic;

    void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(SFX sfx)
    {
        this.SoundEffects[(int)sfx].Stop();
        this.SoundEffects[(int)sfx].Play();
    }

    public void PlayMusic(Music music)
    {        
        music = (music == Music.None) ? CustomCameraController.Instance.SceneMusic : music;

        if (!this.BackgroundMusic[(int)music].isPlaying)
        {
            foreach (var m in this.BackgroundMusic)
            {
                m.Stop();
            }

            this.BackgroundMusic[(int)music].Play();
        }
    }
}
