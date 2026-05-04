using UnityEngine;
using System; 

public class EnemyTrap : MonoBehaviour 
{
    
    private SpriteRenderer _spriteRenderer;

   
    private const float Speed = 2.0f; 
    private float _direction = -1.0f;

    [Header("เวลาสลับด้าน (แทนโหนด Timer)")]
    public float timerDuration = 2.0f; // ตั้งว่ากี่วินาทีสลับฝั่งทีนึง
    private float _timePassed = 0f;

   
    void Start() 
    {
      
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

   
    void Update() 
    {
       
        transform.Translate(new Vector2(_direction * Speed * Time.deltaTime, 0));

        
        _timePassed += Time.deltaTime;
        if (_timePassed >= timerDuration)
        {
            OnTimerTimeout();
            _timePassed = 0f; 
        }
    }

   
    private void OnTimerTimeout()
    {
        _direction *= -1;
        if (_spriteRenderer != null)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX; 
        }
    }

    
   private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject body = collision.gameObject;

        // เช็กว่าคนที่ชนคือ Player ใช่หรือไม่
        if (body.CompareTag("Player") || body.name == "Player")
        {
          
            Health playerScript = body.GetComponent<Health>();
            if (playerScript != null) 
            {
                // สั่งลดเลือด 50 หน่วยแทนการทำลายทิ้งทันที
                playerScript.TakeDamage(50);
                
                // ปริ้นท์บอกใน Console เพื่อความชัวร์ว่าคำสั่งทำงานแล้ว
                Debug.Log("โดนศัตรูชน! เลือดลดไป 50");
            }
        }
    }
}