using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{

    private bool isJumping = false;
    private bool isClimbing = false;
    private bool hasKey = false;
    private float jumpCooldownTimer;
    private CharacterController controller;
    private InputAction moveAction;
    private InputAction jumpAction;

    [SerializeField]
    private float characterSpeed;

    [SerializeField]
    private float climbSpeed;

    [SerializeField]
    private float dampening;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float jumpCooldown;
    private Vector3 characterMovement;
    private Vector3 jumpVelocity;
    private Vector3 characterGravity;
    private Vector3 platformVelocity = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        jumpCooldownTimer = 0.0f;
    }

    void HandleJumping()
    {
        if (this.controller.isGrounded && this.isJumping && this.jumpCooldownTimer <= 0.0f)
        {
            this.jumpVelocity = Vector3.zero;
            this.isJumping = false;
        }
        if (this.controller.isGrounded && !this.isJumping && this.jumpAction.WasPressedThisFrame())
        {
            this.characterGravity = Vector3.zero;
            this.jumpVelocity = Vector3.zero;
            this.jumpVelocity.y = this.jumpSpeed;
            this.jumpCooldownTimer = this.jumpCooldown;
            this.isJumping = true;
        }
        if (this.jumpVelocity.y > 0.0f)
        {
            this.jumpVelocity.y -= Time.fixedDeltaTime;
        }
        else
        {
            this.jumpVelocity = Vector3.zero;
        }
        this.jumpCooldownTimer -= Time.fixedDeltaTime;
    }

    void GetPlatformVelocity()
    {
        bool hit = Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit hitInfo, 1.0f);
        if (hit && hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Platforms") && !isJumping)
        {
            platformVelocity = hitInfo.transform.gameObject.GetComponent<MovingPlatform>().GetVelocity();
        }
        else
        {
            platformVelocity = Vector3.zero;
        }
    }

    public void CollectKey()
    {
        hasKey = true;
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void ResetCharacter()
    {
        hasKey = false;
        isJumping = false;
        isClimbing = false;
        jumpCooldownTimer = 0.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            isClimbing = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            isClimbing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetPlatformVelocity();
        var inputMovement = this.moveAction.ReadValue<Vector2>();

        if (isClimbing)
        {
            this.characterGravity = Vector3.zero;
            this.jumpVelocity = Vector3.zero;
            this.characterMovement = Vector3.zero;
            this.controller.Move(Vector3.up * inputMovement.y * this.climbSpeed * Time.fixedDeltaTime);
            return;
        }

        this.HandleJumping();
        var inputRightDirection = this.cameraTransform.right;
        var inputForwardDirection = this.cameraTransform.forward;
        inputRightDirection.y = 0.0f;
        inputForwardDirection.y = 0.0f;
        inputRightDirection.Normalize();
        inputForwardDirection.Normalize();
        //Since we do not use the physics system, we have to simulate gravity ourselves
        if (this.controller.isGrounded)
        {
            this.characterGravity.y = 0.0f;
        }
        this.characterGravity.y += this.gravity * Time.fixedDeltaTime;
        this.characterMovement += this.characterGravity * Time.fixedDeltaTime;
        this.characterMovement += this.jumpVelocity * Time.fixedDeltaTime;
        this.characterMovement += inputRightDirection * inputMovement.x * this.characterSpeed * Time.fixedDeltaTime;
        this.characterMovement += inputForwardDirection * inputMovement.y * this.characterSpeed * Time.fixedDeltaTime;
        this.characterMovement *= (1 - this.dampening);
        Vector3 characterForward = this.characterMovement;
        characterForward.y = 0.0f;
        if (characterForward.sqrMagnitude > 0.0f && characterForward != Vector3.zero)
        {
            this.transform.forward = characterForward.normalized;
        }
        var combinedMovement = this.characterMovement + this.platformVelocity * Time.fixedDeltaTime;
        this.controller.Move(combinedMovement);
    }
}
