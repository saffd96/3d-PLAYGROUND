using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition = Vector3.zero;

    [Header("Components")]
    [SerializeField] private CharacterController controller;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed;

    [Header("Gravity")]
    [SerializeField] private float gravityScale = 2f;
  
    [Header("Jump")]
    [SerializeField] private float jumpHeight = 3f;
    

    private float gravity;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var moveDirection = transform.forward * vertical + transform.right * horizontal;
        
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        var moveDelta = moveDirection * (moveSpeed * Time.deltaTime);

        if (controller.isGrounded)
        {
            gravity = -0.1f; //jump fix
            
            if (Input.GetButtonDown("Jump"))
            {
                gravity = jumpHeight;
            }
        }
        else
        {
            gravity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }
        moveDelta.y = gravity;

        controller.Move(moveDelta);
    }

    private void Rotate()
    {
        var horizontal = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, rotationSpeed * horizontal * Time.deltaTime);
    }

    public void MoveToStartPosition()
    {
        controller.enabled = false;
        transform.position = startPosition;
        controller.enabled = true;

    }
}
