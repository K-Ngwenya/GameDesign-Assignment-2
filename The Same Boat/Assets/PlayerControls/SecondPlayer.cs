using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayer : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] PlayerController controller;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity = -9.81f;
    private float velocity;
    private Collider _groundDist;
    private Vector2 moveInput;

    private float jumpVal;
    private float distGround;
    private bool jumping;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;


    private void Awake() {
        controller = new PlayerController();
        rb = GetComponent<Rigidbody2D>();

        controller.Player2.Jump.performed += _ => Jump();

        if(rb == null)
        {
            Debug.Log("This isn't rigid at all");
        }
    }

    private void Start() {
        jumping = false;
    }

    private void Jump() {
        jumping = true;
    }

    private void Update() {

        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        velocity += gravity * Time.deltaTime;
        if(isTouchingGround && jumping)
        {
            velocity = jumpForce;
            Debug.Log("UP2");
            /*rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumping = false;*/
        }
        else if(!isTouchingGround && jumping)
        {
            jumping = false;
        }
        transform.Translate(new Vector2(rb.velocity.x, velocity) * Time.deltaTime);

        if(isTouchingGround && velocity < 0)
        {
            velocity = 0;
        }
    }

    private void FixedUpdate() {
        moveInput = controller.Player2.Movement.ReadValue<Vector2>();
        moveInput.y = 0f;
        rb.velocity = moveInput * speed;

        

    }

    private void OnEnable() {
        controller.Player2.Enable();
    }

    private void OnDisable() {
        controller.Player2.Disable();
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, distGround + 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
    
}
