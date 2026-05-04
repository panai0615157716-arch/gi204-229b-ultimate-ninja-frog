using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    
    
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>(); // ดึง Animator มาใช้งาน
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // เล่นแอนิเมชันตอนโดนยิง
        if (anim != null)
        {
            anim.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // เล่นแอนิเมชันตอนตาย
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

        // ปิดการชนและหยุดการเคลื่อนที่เพื่อไม่ให้ศัตรูเดินต่อตอนตาย
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false; 

        // ทำลายวัตถุทิ้งหลังจากแอนิเมชันจบ (เช่น จบใน 0.5 วินาที)
        Destroy(gameObject, 0.5f); 
    }
}