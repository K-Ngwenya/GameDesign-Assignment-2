using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] PlayerController controller;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce = 5f;
    private Collider _groundDist;
    private Vector2 moveInput;
    private float jumpVal;
    private float distGround;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;


    private void Awake() {
        controller = new PlayerController();
        rb = GetComponent<Rigidbody2D>();

        if(rb == null)
        {
            Debug.Log("This isn't rigid at all");
        }
    }

    private void Start() {
        isTouchingGround = false;
    }

    private void Update() {

        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(isTouchingGround && moveInput.y != 0)
        {
            Debug.Log("UP");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate() {
        moveInput = controller.Player1.Movement.ReadValue<Vector2>();
        if(!isTouchingGround)
        {
            moveInput.y = 0f;
        }
        rb.velocity = moveInput * speed;

        

    }

    private void OnEnable() {
        controller.Player1.Enable();
    }

    private void OnDisable() {
        controller.Player2.Disable();
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distGround + 0.1f);
    }




}
