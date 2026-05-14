using UnityEngine;

public class Ice : Spell {
    [SerializeField] float speed;

    protected override void CastImpl() {
        Vector3 position = PlayerController.instance.transform.position;
        Vector3 playerForward =  PlayerController.instance.GetDirectionFacing();

        Rigidbody ice = Instantiate(
            gameObject,
            position + playerForward,
            Quaternion.LookRotation(playerForward)
        ).GetComponent<Rigidbody>();

        ice.linearVelocity = playerForward * speed;

        float movementWithProjectile = Mathf.Max(0f, Vector3.Dot(PlayerController.instance.movementVelocity, playerForward));
        ice.linearVelocity = playerForward * (speed + movementWithProjectile);
    }

    void OnTriggerEnter(Collider other) {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;

        enemy.TakeDamage(vulnerableEnemies.Contains(enemy.type) ? damage : damage / 3f);
        Destroy(gameObject);
    }
}
