using UnityEngine;

public class Ice : Spell {
    [SerializeField] float speed;

    protected override void CastImpl() {
        Vector3 position = PlayerController.instance.transform.position;
        Vector3 direction =  PlayerController.instance.GetDirectionFacing();

        Rigidbody ice = Instantiate(
            gameObject,
            position + direction,
            Quaternion.LookRotation(direction)
        ).GetComponent<Rigidbody>();

        ice.linearVelocity = direction * speed;
    }

    void OnTriggerEnter(Collider other) {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;

        enemy.TakeDamage(vulnerableEnemies.Contains(enemy.type) ? damage : damage / 3f);
        Destroy(gameObject);
    }
}
