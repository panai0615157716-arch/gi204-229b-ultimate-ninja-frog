using UnityEngine;
 [RequireComponent(typeof(Rigidbody2D))]
 [RequireComponent(typeof(Animator))]
 [RequireComponent(typeof(SpriteRenderer))]

public class Playerr : MonoBehaviour
{
     
    [Header("Movement Settings")]
    public float speed = 5f;       // ค่าความเร็วใน Unity จะใช้น้อยกว่า Godot เพราะหน่วยเป็นเมตร (ไม่ใช่พิกเซล)
    public float jumpForce = 5f;  // ค่าแรงกระโดด (ใน Unity แกน Y ขึ้นบนคือค่าบวก)

    [Header("Audio")]
    public AudioSource jumpSound;
    public AudioSource deathSound;

    [Header("Ground Check")]
    public Transform groundCheck;  // ตำแหน่งใต้เท้าตัวละครสำหรับเช็กพื้น
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;  // เลเยอร์ของพื้น

    // ตัวแปรเก็บ Component
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool alive = true;
    private bool canMove = true;
    private bool isGrounded;

    void Start()
    {
        // ดึง Component ในตัวมันเองมาใช้งาน 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (!alive) return;

        // เช็กว่ายืนบนพื้นหรือไม่
        CheckGrounded();

        if (canMove)
        {
            HandleMovement();
            HandleJump();
        }

        HandleAnimation();
    }
    private void HandleMovement()
    {
    
        float moveInput = Input.GetAxisRaw("Horizontal");
        
        // กำหนดความเร็วแกน X โดยรักษาความเร็วแกน Y ไว้เหมือนเดิม
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // หันขวา
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;  // หันซ้าย
        }
    }

    private void HandleJump()
    {
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (jumpSound != null) jumpSound.Play();
        }
    }

    private void HandleAnimation()
    {
        // ควบคุมแอนิเมชันด้วยคำสั่ง Play (ชื่อ State ต้องตรงกับใน Animator Window)
        if (!isGrounded)
        {
            animator.Play("jumping");
        }
        // else if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        // {
        //     animator.Play("running");
        // }
        else
        {
            animator.Play("frong");
        }
    }

    private void CheckGrounded()
    {
        if (groundCheck != null)
        {
            // สร้างวงกลมจำลองใต้เท้าเพื่อตรวจจับว่าชนกับ Layer พื้นหรือไม่
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }

    public void Die()
    {
        if (!alive) return;

        alive = false;
        canMove = false;
        rb.linearVelocity = Vector2.zero; // หยุดนิ่งทันที

        if (deathSound != null) deathSound.Play();
        animator.Play("dying");
    }
}
