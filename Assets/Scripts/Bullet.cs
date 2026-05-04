using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // ตั้งค่าดาเมจของอาวุธชิ้นนี้

    // ฟังก์ชันนี้ทำงานอัตโนมัติเมื่อกระสุนไปชนกับวัตถุอื่น
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าสิ่งที่กระสุนชน มีสคริปต์ Health แปะอยู่ไหม
        Health targetHealth = collision.gameObject.GetComponent<Health>();

        if (targetHealth != null)
        {
            // ถ้ามีสคริปต์ Health แปลว่ามีชีวิต ก็สั่งให้ลดเลือดตามดาเมจที่เราตั้งไว้
            targetHealth.TakeDamage(damage);
        }

        // ยิงไปโดนอะไรก็ตาม (โดนกำแพง โดนศัตรู) กระสุนต้องทำลายตัวเองทิ้ง
        Destroy(gameObject); 
    }
}