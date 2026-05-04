using UnityEngine;
using UnityEngine.UI; // ต้องมีบรรทัดนี้เพื่อใช้งาน UI (เช่น Slider)
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อใช้คำสั่งโหลดฉาก (Restart)

public class Health : MonoBehaviour
{
    [Header("การตั้งค่าเลือด")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("ตั้งค่าหน้าต่าง UI")]
    public Slider healthSlider;        // ช่องสำหรับใส่หลอดเลือด UI
    public GameObject gameOverPanel;   // ช่องสำหรับใส่หน้าต่าง Game Over

    void Start()
    {
        // เริ่มเกมมาให้เลือดเต็ม
        currentHealth = maxHealth;

        // ตั้งค่าหลอดเลือด UI
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // ซ่อนหน้าต่าง Game Over และให้เวลาในเกมเดินตามปกติ (เผื่อกด Restart มา)
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // อัปเดตหลอดเลือดให้ลดลงตาม
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // ถ้าเลือดหมดให้เรียกใช้ฟังก์ชันตาย
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("ผู้เล่นตายแล้ว! เกมหยุด");

        // เด้งหน้าต่าง Game Over ขึ้นมา
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // หยุดเวลาในเกม (ศัตรู แอนิเมชัน และการเคลื่อนไหวทุกอย่างจะหยุดนิ่ง)
        Time.timeScale = 0f;
    }

    // ==========================================
    // ส่วนคำสั่งสำหรับให้ปุ่ม UI กดเรียกใช้งาน
    // ==========================================

    public void RestartGame()
    {
        // สิ่งสำคัญ: ต้องคืนค่าเวลาให้เดินปกติ (1f) ก่อนโหลดฉาก ไม่งั้นเกมจะค้างตั้งแต่เริ่ม
        Time.timeScale = 1f;

        // โหลดฉากปัจจุบันขึ้นมาใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("ออกจากการเล่นเกม");
        // ปิดเกม (จะเห็นผลตอนกด Build เกมเป็นไฟล์ .exe ออกมาแล้วเท่านั้น ในหน้าต่าง Editor จะไม่ปิดให้)
        Application.Quit();
    }
}