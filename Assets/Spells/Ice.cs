using UnityEngine;

public class Ice : Spell {
    protected override void CastImpl() {
        Vector3 position = PlayerController.instance.transform.position;
        Vector3 forward = PlayerController.instance.transform.forward;

        Rigidbody ice = Instantiate(
            gameObject,
            position + forward,
            Quaternion.LookRotation(forward)
        ).GetComponent<Rigidbody>();

        ice.linearVelocity = forward * 10f;
    }

    void OnTriggerEnter(Collider other) {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;

        enemy.TakeDamage(vulnerableEnemies.Contains(enemy.type) ? damage : damage / 3f);
    }
}
