using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SonicScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public int maxJumps = 2;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isGrounded = false;
    private int jumpCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        // Movimento horizontal
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Verificar se o personagem está no chão
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);

        // Pulo
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpCount = 1;
        }
        else if (!isGrounded && isJumping && jumpCount < maxJumps && Input.GetButtonDown("Jump"))
        {
            jumpCount++;
        }
    }

    private void FixedUpdate()
    {
        // Aplicar pulo
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }

        // Aplicar controle da gravidade
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * 2f * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.deltaTime;
        }
    }
}
