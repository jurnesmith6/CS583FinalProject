using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] float avgMoveSpeed;
    // Standard deviation for move speed. 68% of move speeds will be within 1 moveSpeedSD from averageMoveSpeed, 95% within 2, and 99.7% within 3.
    [SerializeField] float moveSpeedSD;
    [SerializeField] float attackRange;

    public EnemyType type;
    public float hp;
    Rigidbody rb;
    float moveSpeed;
    bool dead = false;

    Vector3 crystalPosition = Vector3.zero;
    Vector3 crystalDirection;

    public void TakeDamage(float amount) {
        if (dead) return;

        hp -= amount;

        if (hp <= 0f) {
            dead = true;
            Destroy(gameObject);
            Spawner.instance.EnemyKilled();
        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        moveSpeed = Util.NormalDist(avgMoveSpeed, moveSpeedSD);

        // diection to face the crystal
        Vector3 direction = crystalPosition - transform.position;
        direction.y = 0f;
        crystalDirection = direction;
    }

    private void Update() {
        rb.rotation = Quaternion.LookRotation(crystalDirection);
    }

    void FixedUpdate() {
        if ((crystalPosition - rb.position).magnitude > attackRange)
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * crystalDirection.normalized);
    }
}
