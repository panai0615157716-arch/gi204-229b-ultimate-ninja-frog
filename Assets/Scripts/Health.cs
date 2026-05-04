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
    public Image healthBarFill;     // ลากวัตถุ "Blood" มาใส่ที่นี่
    public GameObject gameOverPanel;
    public float lerpSpeed = 5f;    // ความเร็วในการลดของหลอดเลือด

    void Start()
    {
        currentHP = maxHP;
        Time.timeScale = 1f;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateHPUI();
    }

    void Update()
    {
        // ทำให้หลอดเลือดค่อยๆ ลดแบบนิ่มนวล
        if (healthBarFill != null)
        {
            float targetFill = (float)currentHP / maxHP;
            healthBarFill.fillAmount = Mathf.Lerp(healthBarFill.fillAmount, targetFill, Time.deltaTime * lerpSpeed);
        }
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
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP;
        }
        // หมายเหตุ: การลดแบบทันทีจะอยู่ในฟังก์ชันนี้ แต่เราย้ายไปทำแบบ Smooth ใน Update แล้ว
    }

    // ใช้ OnTriggerEnter2D หรือ OnCollisionEnter2D เพราะเกมคุณเป็น 2D
    private void OnCollisionEnter2D(Collision2D collision)
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
    public void QuitGame()
    {

        Application.OpenURL("https://gi204-229b-gim-iti-bu.itch.io/ultimate-ninja-frog");


        Debug.Log(" itch.io close !");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}