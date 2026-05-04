using UnityEngine;

public class Spike : MonoBehaviour
{
    [Header("ตั้งค่าดาเมจของหนาม")]
    public int damage = 10; 
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        Health Health = collision.gameObject.GetComponent<Health>();

        if (Health != null)
        {
            
            Health.TakeDamage(damage);
            
           
            Debug.Log("โอ๊ย! โดนหนามแทง เลือดลดไป: " + damage);
        }
    }
}