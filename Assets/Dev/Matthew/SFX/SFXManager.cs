using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Menu")]
    [SerializeField] private AudioClip buttonClick;

    [Header("Crystal")]
    [SerializeField] private AudioClip crystalDamage1;
    [SerializeField] private AudioClip crystalDamage2;

    [Header("Fire Spell")]
    [SerializeField] private AudioClip fireSingle;
    [SerializeField] private AudioClip fireHold;

    [Header("Ice Spell")]
    [SerializeField] private AudioClip iceSingle;
    [SerializeField] private AudioClip iceHold;

    private AudioSource sfxSource;       // one-shots
    private AudioSource loopSource;      // looping hold sounds
    private float sfxVolume = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        sfxSource = gameObject.AddComponent<AudioSource>();

        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;
    }

    // Volume control for menu
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        loopSource.volume = sfxVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    // Menu
    public void PlayButtonClick() { PlayOneShot(buttonClick); }

    // Crystal
    public void PlayCrystalDamage()
    {
        AudioClip clip = Random.value > 0.5f ? crystalDamage1 : crystalDamage2;
        PlayOneShot(clip);
    }

    // Fire
    public void PlayFireSingle() { PlayOneShot(fireSingle); }

    public void StartFireHold()
    {
        if (loopSource.clip != fireHold || !loopSource.isPlaying)
        {
            loopSource.clip = fireHold;
            loopSource.volume = sfxVolume;
            loopSource.Play();
        }
    }

    // Ice
    public void PlayIceSingle() { PlayOneShot(iceSingle); }

    public void StartIceHold()
    {
        if (loopSource.clip != iceHold || !loopSource.isPlaying)
        {
            loopSource.clip = iceHold;
            loopSource.volume = sfxVolume;
            loopSource.Play();
        }
    }

    // Stop looping the sound
    public void StopHoldSound()
    {
        loopSource.Stop();
    }

    private void PlayOneShot(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
    }
}