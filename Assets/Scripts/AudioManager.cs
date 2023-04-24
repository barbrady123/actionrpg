using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource[] SoundEffects;

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

    void Start()
    {

    }

    void Update()
    {

    }

    public void PlaySFX(SFX sfx)
    {
        this.SoundEffects[(int)sfx].Stop();
        this.SoundEffects[(int)sfx].Play();
    }
}
