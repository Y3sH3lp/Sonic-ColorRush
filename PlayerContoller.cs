using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 50f;
    public float maxForwardSpeed = 1000f; 
    public float speedIncreaseRate = 0.2f; 
    public float sidewaysSpeed = 5f;
    public float jumpForce = 10f;
    public float airJumpForce = 8f;
    public float dashForce = 50f;
    public float dashCooldown = 2f;
    public float fallAcceleration = 50f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("References")]
    public Transform playerModel; 
    public Transform cameraTransform; 
    public Vector3 cameraOffset = new Vector3(0, 5, -10);
    public float cameraFollowSpeed = 5f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isDashing;
    private float dashCooldownTimer;
    private float currentForwardSpeed = 0f;
    private float verticalVelocity = 0f;
    private bool isGrounded;

    [SerializeField] private int maxJumps = 2;
    private int currentJumps;

  
    public float gravity = -9.81f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentJumps = maxJumps;

    
        currentForwardSpeed = forwardSpeed;
    }

    private void Update()
    {
        CheckGroundStatus();
        HandleMovement();
        HandleJumping();
        HandleDashing();
        RotatePlayerModel();
        UpdateCameraPosition();
        
        IncreaseForwardSpeed();
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            currentJumps = maxJumps; 
        }
    }

    private void HandleMovement()
    {
        currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, forwardSpeed, Time.deltaTime * 5f);
        
        moveDirection.z = currentForwardSpeed;
        
        float horizontalInput = Input.GetAxis("Horizontal");
        moveDirection.x = horizontalInput * sidewaysSpeed;
        
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; 
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; 
        }
        
        moveDirection.y = verticalVelocity;
        
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump") && currentJumps > 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        verticalVelocity = 0;
        
        float appliedJumpForce = isGrounded ? jumpForce : airJumpForce;
        verticalVelocity = appliedJumpForce;
        
        currentJumps--;
    }
    

    private void HandleDashing()
    {
        if (isDashing)
        {
            return;
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        Vector3 dashDirection = new Vector3(moveDirection.x, 0, currentForwardSpeed).normalized;
        float dashTime = 5f; 
        float elapsedTime = 0;
        
        while (elapsedTime < dashTime / 2)
        {
            characterController.Move(dashDirection * dashForce * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        while (elapsedTime < dashTime)
        {
            characterController.Move(dashDirection * (dashForce * (1 - (elapsedTime / dashTime))) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    private void RotatePlayerModel()
    {
        Vector3 movementDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        if (movementDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 targetPosition = transform.position + cameraOffset;
        cameraTransform.position =
            Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime * cameraFollowSpeed);
        cameraTransform.LookAt(transform.position); 
    }

    private void IncreaseForwardSpeed()
    {
        currentForwardSpeed = Mathf.Min(currentForwardSpeed + speedIncreaseRate * Time.deltaTime, maxForwardSpeed);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red; 
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

}
