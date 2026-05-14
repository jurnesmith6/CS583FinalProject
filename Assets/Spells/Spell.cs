using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {
    [SerializeField] private float cooldown;
    [SerializeField] protected List<EnemyType> vulnerableEnemies;
    [SerializeField] protected float damage;
    [SerializeField] protected float lifeTime;

    public static Dictionary<Type, float> cooldowns = new();

    static Spell() {
        InitializeCooldowns();
    }

    protected void FixedUpdate() {
        lifeTime -= Time.fixedDeltaTime;

        if (lifeTime <= 0f)
            Destroy(gameObject);
    }

    public void Cast() {
        if (cooldowns[GetType()] > 0f) return;
        CastImpl();
        cooldowns[GetType()] = cooldown;
    }

    protected abstract void CastImpl();

    public bool CanCast() { return cooldowns[GetType()] <= 0; }

    public static void InitializeCooldowns() {
        Type[] spellTypes = typeof(Spell).Assembly.GetTypes();

        foreach (Type spellType in spellTypes) {
            if (spellType.IsSubclassOf(typeof(Spell)))
                cooldowns[spellType] = 0f;
        }
    }

    // Called from the players FixedUpdate to update cooldowns for all spells
    public static void CooldownUpdate() {
        Type[] keys = new Type[cooldowns.Keys.Count];
        cooldowns.Keys.CopyTo(keys, 0);

        foreach (Type key in keys)
            cooldowns[key] = Mathf.Max(0f, cooldowns[key] - Time.fixedDeltaTime);
    }
}
