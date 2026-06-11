using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void onMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    void Update()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.deltaTime);
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        float minX = -8.5f;
        float maxX = 8.5f;
        float minY = -4.5f;
        float maxY = 4.5f;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
