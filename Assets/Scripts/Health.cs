using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("การตั้งค่าเลือด")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("ตั้งค่าหน้าต่าง UI")]
    public Slider healthSlider;
    public GameObject gameOverPanel;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        // ตั้งค่าหลอดเลือด UI
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // ซ่อนหน้าจอ Game Over ตอนเริ่มเกม
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die(); // เรียกฟังก์ชันตายโดยตรง ไม่ต้องรอ Coroutine
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("ผู้เล่นตายแล้ว - แสดงหน้าจอ Game Over ทันที");

        // แสดงหน้าจอ Game Over ทันที
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // หยุดเวลาในเกมทันที
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}