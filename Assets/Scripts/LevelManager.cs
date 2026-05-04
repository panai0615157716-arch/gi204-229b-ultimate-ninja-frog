using UnityEngine;
using UnityEngine.UI; // ต้องมีบรรทัดนี้เพื่อเชื่อมกับ UI
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อใช้คำสั่งเปลี่ยนด่าน

public class LevelManager : MonoBehaviour
{
    [Header("ตั้งค่าผลไม้")]
    public int currentFruits = 0;   // จำนวนที่เก็บได้ตอนนี้
    public int fruitsToWin = 5;     // จำนวนเป้าหมายที่ต้องเก็บให้ครบ

    [Header("หน้าต่าง UI")]
    public Text fruitTextUI;        // เอาตัวหนังสือ UI มาใส่ช่องนี้

    void Start()
    {
        UpdateUI(); // อัปเดตตัวเลขบนจอก่อนเริ่มเกม
    }

    // ฟังก์ชันนี้จะถูกเรียกใช้เมื่อ Player เดินชนผลไม้
    public void CollectFruit()
    {
        currentFruits++; // เพิ่มแต้ม
        UpdateUI();      // อัปเดตตัวเลขบนจอ

        // เช็กว่าเก็บครบหรือยัง
        if (currentFruits >= fruitsToWin)
        {
            LevelComplete();
        }
    }

    void UpdateUI()
    {
        if (fruitTextUI != null)
        {
            fruitTextUI.text = "ผลไม้: " + currentFruits + " / " + fruitsToWin;
        }
    }

    void LevelComplete()
    {
        Debug.Log("ยินดีด้วย! เก็บครบแล้ว ผ่านด่านได้!");
        
        // คำสั่งโหลดด่านต่อไป (เปลี่ยนชื่อในฟันหนูให้ตรงกับชื่อ Scene ด่านต่อไปของคุณ)
        // SceneManager.LoadScene("Level2"); 
    }
}