using UnityEngine;

public class IceBallVFX : MonoBehaviour
{
    private ParticleSystem[] allParticles;

    [SerializeField] private float vfxScale = 3f; // adjust in Inspector if needed

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
    }

    public void Detach()
    {
        transform.SetParent(null);

        float longestLife = 0f;
        foreach (var ps in allParticles)
        {
            ps.Stop();
            float life = ps.main.startLifetime.constantMax;
            if (life > longestLife) longestLife = life;
        }

        Destroy(gameObject, longestLife);
    }
}