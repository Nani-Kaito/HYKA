using UnityEngine.InputSystem;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public float acceleration = 15f;
    public float steering = 80f;
    public float maxSpeed = 20f;

    private Rigidbody rb;
    private float moveInput;   // Forward/back
    private float steerInput;  // Left/right

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // lowers center of mass so bike doesn’t tip too easily
    }

    void FixedUpdate()
    {
        // Forward/backward force
        Vector3 forwardMove = transform.forward * moveInput * acceleration;
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(forwardMove, ForceMode.Acceleration);
        }

        // Steering (rotates bike)
        if (rb.linearVelocity.magnitude > 1f) // only steer if moving
        {
            float turn = steerInput * steering * Time.fixedDeltaTime;
            Quaternion turnOffset = Quaternion.Euler(0, turn, 0);
            rb.MoveRotation(rb.rotation * turnOffset);
        }
    }

    // Input from new Input System
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().y;
        Debug.Log("w");
        // Up/Down = forward/back
        steerInput = context.ReadValue<Vector2>().x; // Left/Right = steer
        Debug.Log("d");
    }
    
}
