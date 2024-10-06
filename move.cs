using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    private float xRotation = 0f;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main; // Get the main camera
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

        // Capsule movement
        float moveHorizontal = Input.GetAxis("Horizontal"); // A and D keys
        float moveVertical = Input.GetAxis("Vertical"); // W and S keys

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
