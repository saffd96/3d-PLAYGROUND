using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController controller;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    // [Header("Rotation")]
    // [SerializeField] private float rotationSpeed;

    [Header("Gravity")]
    [SerializeField] private float gravityScale = 2f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 3f;

    private Transform cameraTransform;

    private float gravity;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (GameManager.Instance.IsPaused) return; 
        
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        var right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        var moveDirection = forward * vertical + right * horizontal;

        if (Mathf.Abs(horizontal)+(Mathf.Abs(vertical)) > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }

        var moveDelta = moveDirection * (moveSpeed * Time.deltaTime);

        if (controller.isGrounded)
        {
            gravity = -0.1f;

            if (Input.GetButtonDown("Jump"))
            {
                AudioManager.Instance.PLaySfx(SfxType.Jump);
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
    //
    // private void Rotate()
    // {
    //     var horizontal = Input.GetAxis("Mouse X");
    //     transform.Rotate(Vector3.up, rotationSpeed * horizontal * Time.deltaTime);
    // }

    public void MoveToPosition(Transform position)
    {
        controller.enabled = false;
        transform.position = position.position;
        controller.enabled = true;
    }

}
