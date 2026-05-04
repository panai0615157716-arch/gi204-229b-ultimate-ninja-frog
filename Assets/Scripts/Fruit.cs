using UnityEngine;

public class FruitCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าคนที่มาชน มีป้ายชื่อ (Tag) ว่า "Player" หรือไม่
        if (collision.gameObject.CompareTag("Player"))
        {
            // ค้นหากรรมการ (LevelManager) ในฉาก
            LevelManager manager = FindFirstObjectByType<LevelManager>();
            
            if (manager != null)
            {
                manager.CollectFruit(); // สั่งให้กรรมการบวกแต้ม
            }

            // ทำลายผลไม้ทิ้ง (เสมือนว่าเก็บเข้ากระเป๋าไปแล้ว)
            Destroy(gameObject);
        }
    }
}