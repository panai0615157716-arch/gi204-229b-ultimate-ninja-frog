using UnityEngine;
using UnityEngine.UI; // สำหรับ Text ธรรมดา
using TMPro; // สำหรับ TextMeshPro (แนะนำให้ใช้ตัวนี้)

public class PlayerWinSystem : MonoBehaviour
{
    [Header("ตั้งค่าการชนะ")]
    public int fruitCount = 0;
    public int requiredFruits = 3;

    [Header("การเชื่อมต่อ UI")]
    public TextMeshProUGUI fruitText; // ลาก FruitText มาใส่ช่องนี้
    public GameObject winPanel;

    void Start()
    {
        UpdateFruitUI(); // อัปเดตข้อความครั้งแรกตอนเริ่มเกม
        if (winPanel != null) winPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            fruitCount++;
            UpdateFruitUI(); // ทุกครั้งที่เก็บได้ ให้สั่งอัปเดตตัวเลขบนจอ
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("WinZone") && fruitCount >= requiredFruits)
        {
            WinGame();
        }
    }

    // ฟังก์ชันสำหรับเปลี่ยนข้อความบน UI
    void UpdateFruitUI()
    {
        if (fruitText != null)
        {
            fruitText.text = "Fruits: " + fruitCount + " / " + requiredFruits;
        }
    }

    void WinGame()
    {
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}