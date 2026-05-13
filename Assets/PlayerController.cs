using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform character;
    [SerializeField] Spell[] spells;

    public static PlayerController instance;
    public float moveSpeed;
    public float maxHp;
    public float hp { private set; get; }
    public float knockbackForce;
    public Rigidbody rb { private set; get; }
    public Vector3 movementVelocity { private set; get; }

    InputSystemActions input;
    Vector2 moveInput = Vector2.zero;
    Vector3 forwardDirection, rightDirection;
    bool attacking = false;
    Spell spell;
    float hitStunDuration = 0.75f;
    float invincibilityDuration = 0f;

    void Awake() {
        instance = this;
        spell = spells[0];
        hp = maxHp;

        forwardDirection = mainCamera.transform.forward;
        forwardDirection.y = 0;
        forwardDirection.Normalize();
        rightDirection = Vector3.Cross(Vector3.up, forwardDirection);

        rb = GetComponent<Rigidbody>();

        input = new InputSystemActions();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        input.Player.Attack.performed += ctx => attacking = true;
        input.Player.Attack.canceled += ctx => attacking = false;
        input.Player.SpellSwitch.performed += ctx => spell = spells[int.Parse(ctx.control.name) - 1];

        input.Enable();
    }

    void FixedUpdate() {
        Spell.CooldownUpdate();
        invincibilityDuration = Mathf.Max(0f, invincibilityDuration - Time.fixedDeltaTime);

        if (hitStunDuration > 0f) {
            hitStunDuration -= Time.fixedDeltaTime;
            return;
        }

        if (attacking)
            spell.Cast();

        movementVelocity = moveSpeed * (forwardDirection * moveInput.y + rightDirection * moveInput.x);
        rb.MovePosition(rb.position + Time.fixedDeltaTime * movementVelocity);
    }

    void Update() {
        LookAtPoint(MouseWorldPosition());
    }

    public void LookAtPoint(Vector3 target) {
        Vector3 direction = target - character.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        character.rotation = Quaternion.RotateTowards(
            character.rotation,
            targetRotation,
            360f * Time.deltaTime
        );
    }

    Vector3 MouseWorldPosition() {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f, groundLayer))
            return hit.point;

        return mainCamera.transform.position;
    }

    public Vector3 GetDirectionFacing() {
        return character.forward;
    }

    public void OnCollisionStay(Collision collision) {
        if (invincibilityDuration > 0f || collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        --hp;
        invincibilityDuration = 1.5f;
        hitStunDuration = 0.5f;
        
        rb.linearVelocity = Vector3.zero;
        Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }
}
