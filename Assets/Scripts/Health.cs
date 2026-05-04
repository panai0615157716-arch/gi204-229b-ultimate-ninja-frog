using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("HP Settings")]
    public int maxHP = 100;
    public int currentHP;
    
    [Header("UI References")]
    public TextMeshProUGUI hpText;  
    public Image healthBarFill;     
    public GameObject gameOverPanel; 

    void Start()
    {
        currentHP = maxHP;
        UpdateHPUI();
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); 
        UpdateHPUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateHPUI()
    {
        if (hpText != null) hpText.text = "HP: " + currentHP;
        if (healthBarFill != null) healthBarFill.fillAmount = (float)currentHP / maxHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(20);
        }
    }

    void Die()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f; 
        Debug.Log("Player Died!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {

        Application.OpenURL("https://gi204-229b-gim-iti-bu.itch.io/kittys");


        Debug.Log(" itch.io close !");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}