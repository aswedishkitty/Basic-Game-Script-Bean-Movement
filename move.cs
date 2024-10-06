using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    public Camera playerCamera; // Assign your camera in the Inspector
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Use the main camera if none is assigned
        }
        
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor at the start
    }

    void Update()
    {
        // Cursor lock/unlock with Alt key
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the up/down rotation

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX); // Rotate the capsule left/right

        // Capsule movement based on camera direction
        float moveHorizontal = Input.GetAxis("Horizontal"); // A and D keys
        float moveVertical = Input.GetAxis("Vertical"); // W and S keys

        // Get camera's forward and right directions
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        // Flatten the directions on the Y-axis to prevent upward/downward movement
        forward.y = 0;
        right.y = 0;

        forward.Normalize(); // Normalize the vectors to have a magnitude of 1
        right.Normalize();

        // Calculate movement direction based on camera orientation
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized; // Combine forward and right movements
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World); // Move the capsule
    }
}
