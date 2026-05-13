using UnityEngine;

public class HitShardVFX : MonoBehaviour
{
    private ParticleSystem[] allParticles;

    void Awake()
    {
        allParticles = GetComponentsInChildren<ParticleSystem>();
    }

    public void Trigger()
    {
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