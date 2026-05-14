using UnityEngine;

public class BloodSplatterVFX : MonoBehaviour
{
    private ParticleSystem[] allParticles;

    [SerializeField] private float vfxScale = 2f;

    void Awake()
    {
        allParticles = GetComponentsInChildren<ParticleSystem>();
    }

    public void Trigger()
    {
        transform.localScale = Vector3.one * vfxScale;

        foreach (var ps in allParticles)
        {
            ps.Play();
        }

        float longestLife = 0f;
        foreach (var ps in allParticles)
        {
            float total = ps.main.duration + ps.main.startLifetime.constantMax;
            if (total > longestLife) longestLife = total;
        }
        Destroy(gameObject, longestLife);
    }
}