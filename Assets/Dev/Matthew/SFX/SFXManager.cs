// Assets/Dev/Matthew/SFX/SFXManager.cs
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

    [Header("Enemy Sounds")]
    [SerializeField] private AudioClip ogres;
    [SerializeField] private AudioClip zombies;
    [SerializeField] private float enemySoundInterval = 5f;

    [Header("Player")]
    [SerializeField] private AudioClip running;

    [Header("Combat")]
    [SerializeField] private AudioClip damage;

    private AudioSource sfxSource;
    private AudioSource loopSource;
    private AudioSource runSource;
    private float sfxVolume = 1f;
    private float enemySoundTimer = 0f;

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

        runSource = gameObject.AddComponent<AudioSource>();
        runSource.loop = true;
    }

    void Update()
    {
        // Randomly play ogre or zombie sounds at intervals
        enemySoundTimer -= Time.deltaTime;
        if (enemySoundTimer <= 0f)
        {
            PlayRandomEnemySound();
            enemySoundTimer = enemySoundInterval + Random.Range(-1f, 2f);
        }
    }

    // Volume control for menu dev
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        loopSource.volume = sfxVolume;
        runSource.volume = sfxVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    // --- Menu ---
    public void PlayButtonClick() { PlayOneShot(buttonClick); }

    // --- Crystal ---
    public void PlayCrystalDamage()
    {
        AudioClip clip = Random.value > 0.5f ? crystalDamage1 : crystalDamage2;
        PlayOneShot(clip);
    }

    // --- Fire ---
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

    // --- Ice ---
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

    // --- Stop any looping spell sound ---
    public void StopHoldSound()
    {
        loopSource.Stop();
    }

    // --- Running ---
    public void StartRunning()
    {
        if (!runSource.isPlaying)
        {
            runSource.clip = running;
            runSource.volume = sfxVolume;
            runSource.Play();
        }
    }

    public void StopRunning()
    {
        runSource.Stop();
    }

    // --- Enemy Damage ---
    public void PlayDamage() { PlayOneShot(damage); }

    // --- Random Enemy Ambient ---
    private void PlayRandomEnemySound()
    {
        AudioClip clip = Random.value > 0.5f ? ogres : zombies;
        PlayOneShot(clip);
    }

    private void PlayOneShot(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
    }
}