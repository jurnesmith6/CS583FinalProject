using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform character;
    
    public float moveSpeed = 10f;

    InputSystemActions input;
    Vector2 moveInput = Vector2.zero;
    Vector3 forwardDirection, rightDirection;
    Rigidbody rb;

    void Awake() {
        forwardDirection = mainCamera.transform.forward;
        forwardDirection.y = 0;
        forwardDirection.Normalize();
        rightDirection = Vector3.Cross(Vector3.up, forwardDirection);

        rb = GetComponent<Rigidbody>();

        input = new InputSystemActions();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        input.Enable();
    }

    void FixedUpdate() {
        Vector3 reoriented = forwardDirection * moveInput.y + rightDirection * moveInput.x;
        rb.MovePosition(character.position + moveSpeed * Time.deltaTime * reoriented);
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
}
