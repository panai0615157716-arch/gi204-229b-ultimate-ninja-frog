using UnityEngine;
using TMPro; 

public class FruitCollector : MonoBehaviour
{
    [Header("Settings")]
    public int fruitCount = 0;           
    public TextMeshProUGUI fruitText;    

    [Header("Audio")]
    public GameObject collectSfxPrefab; 

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.gameObject.CompareTag("Fruit"))
        {
            fruitCount += 1;             
            UpdateFruitUI();             

            
            if (collectSfxPrefab != null)
            {
                Instantiate(collectSfxPrefab);
            }

            Destroy(other.gameObject);   
            Debug.Log("Collected Fruit! Total: " + fruitCount);
        }
    }

    void UpdateFruitUI()
    {
        if (fruitText != null)
        {
            
            fruitText.text = ": " + fruitCount;
        }
    }
}