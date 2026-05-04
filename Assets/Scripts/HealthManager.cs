using UnityEngine;
using UnityEngine.UI; // อย่าลืมเพิ่มตัวนี้เพื่อใช้งาน Image

public class HealthManager : MonoBehaviour
{
    // ลากรูปเลือด (Blood) ที่อยู่ใน Hp มาใส่ในช่องนี้
    public Image healthBarFill;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // คำนวณอัตราส่วนเลือด (0 - 1)
        float fillAmount = currentHealth / maxHealth;

        // ปรับขนาดแถบเลือดในแกน X
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = fillAmount;
        }
    }
}