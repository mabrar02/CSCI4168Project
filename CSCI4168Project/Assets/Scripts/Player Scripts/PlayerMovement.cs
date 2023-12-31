using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class used to enable the player's movement
 */
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool canJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    public bool grounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDir;

    private Rigidbody rb;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;

        
    }

    void Update()
    {
        // use a raycast  to check if player is grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);


        // get movement input
        GetInput();

        ControlSpeed();

        if (grounded) {
            rb.drag = groundDrag;
        }
        else {
            rb.drag = 0;
        }
        
    }

    private void FixedUpdate() {

        MovePlayer();


    }

    // if player is grounded, move with their directional input
    private void MovePlayer() {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded) {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded) {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    // get directional input from keyboard, and check if player jumping
    private void GetInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Space) && canJump && grounded) {
            canJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    // limit speed so it can't infinitely climb
    private void ControlSpeed() {
        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(velocity.magnitude > moveSpeed) {
            Vector3 limitedVelocity = velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    // use impulse force to simulate a jump
    private void Jump() {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        canJump = true;
    }
}
