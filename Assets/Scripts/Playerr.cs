using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Playerr : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("HP Settings")]
    public int maxHP = 100;
    public int currentHP;
    public TextMeshProUGUI hpText;
    public Image healthBarFill;

    [Header("Fruit & Win Settings")]
    public int fruitCount = 0;
    public int requiredFruits = 5;
    public TextMeshProUGUI fruitText;
    public GameObject winPanel;
    public GameObject winSfxPrefab;
    public GameObject gameOverPanel;


    [Header("Audio")]
    public AudioSource jumpSound;
    public AudioSource deathSound;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    // ตัวแปรเก็บ Component
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool alive = true;
    private bool canMove = true;
    private bool isGrounded;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ตั้งค่าเริ่มต้นระบบเลือดและ UI
        currentHP = maxHP;
        Time.timeScale = 1f;

        if (winPanel != null) winPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        UpdateAllUI();
    }

    void Update()
    {
        if (!alive) return;

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
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            if (jumpSound != null) jumpSound.Play();
        }
    }

    private void HandleAnimation()
    {
        if (!isGrounded)
        {
            animator.Play("jumping");
        }
        else if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            animator.Play("running"); // เปิดใช้งานอนิเมชันวิ่งถ้ามี
        }
        else
        {
            animator.Play("frong");
        }
    }

    private void CheckGrounded()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }

    // --- ระบบการชน (Collision/Trigger) ---

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. เก็บผลไม้ (อย่าลืมสร้าง Tag "Fruit")
        if (other.CompareTag("Fruit"))
        {
            fruitCount++;
            UpdateAllUI();
            Destroy(other.gameObject);
        }

        // 2. เข้าเส้นชัย (อย่าลืมสร้าง Tag "WinZone")
        if (other.CompareTag("WinZone") && fruitCount >= requiredFruits)
        {
            WinGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 3. ชนหนาม (อย่าลืมสร้าง Tag "Spike")
        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(20);
        }
    }

    // --- ระบบจัดการค่าพลังและสถานะเกม ---

    public void TakeDamage(int damage)
    {
        if (!alive) return;
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateAllUI();

        if (currentHP <= 0) Die();
    }

    void UpdateAllUI()
    {
        if (hpText != null) hpText.text = "HP: " + currentHP;
        if (healthBarFill != null) healthBarFill.fillAmount = (float)currentHP / maxHP;
        if (fruitText != null) fruitText.text = "Fruits: " + fruitCount + " / " + requiredFruits;
    }

    public void Die()
    {
        if (!alive) return;
        alive = false;
        canMove = false;
        rb.linearVelocity = Vector2.zero;

        if (deathSound != null) deathSound.Play();
        animator.Play("dying");

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void WinGame()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;

        // --- แทรกตรงนี้ ---
        if (winSfxPrefab != null)
        {
            Instantiate(winSfxPrefab);
        }
        // ----------------

        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // --- ปุ่มสำหรับ UI ---

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {

        Application.OpenURL("https://gi204-229b-gim-iti-bu.itch.io/kittys");


        Debug.Log(" itch.io close !");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}