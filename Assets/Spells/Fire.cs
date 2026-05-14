using UnityEngine;

public class Fire : Spell {
    [SerializeField] float avgSpeed;
    [SerializeField] float speedSD;

    protected override void CastImpl() {
        Vector3 position = PlayerController.instance.transform.position;
        Vector3 playerForward = PlayerController.instance.GetDirectionFacing();

        Vector3 direction = Quaternion.Euler(
            0f,
            Util.NormalDist(0f, 5.5f),
            0f
        ) * playerForward;

        Rigidbody fire = Instantiate(
            gameObject,
            position + direction,
            Quaternion.LookRotation(direction)
        ).GetComponent<Rigidbody>();

        // projection of players movement velocity onto their facing direction
        float movementWithFire = Mathf.Max(0f, Vector3.Dot(PlayerController.instance.movementVelocity, playerForward));
        fire.linearVelocity = direction * (Util.NormalDist(avgSpeed, speedSD) + movementWithFire);
    }

    void OnTriggerEnter(Collider other) {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;

        enemy.TakeDamage(vulnerableEnemies.Contains(enemy.type) ? damage : damage / 3f);
    }
}
