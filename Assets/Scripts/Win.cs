using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [Header("Win Settings")]
    public int fruitCount = 0;
    public int requiredFruits = 5;

    [Header("UI Connections")]
    public TextMeshProUGUI fruitText;
    public GameObject winPanel;

    void Start()
    {
        UpdateFruitUI();
        if (winPanel != null) winPanel.SetActive(false);
        Time.timeScale = 1f; // คืนค่าเวลาให้ปกติทุกครั้งที่เริ่มเกม
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ระบบเก็บผลไม้
        if (other.gameObject.CompareTag("Fruit"))
        {
            fruitCount++;
            UpdateFruitUI();
            Destroy(other.gameObject);
        }

        // ระบบเช็กเส้นชัย
        if (other.gameObject.CompareTag("WinZone") && fruitCount >= requiredFruits)
        {
            WinGame();
        }
    }

    void UpdateFruitUI()
    {
        if (fruitText != null)
        {
            fruitText.text = "Fruits: " + fruitCount + " / " + requiredFruits;
        }
    }

    void WinGame()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true); // ใช้จุด (.) แทนขีดล่าง (_) เพื่อแก้ Error
        }
        Time.timeScale = 0f; // หยุดเกมเมื่อชนะ
        Debug.Log("Win Game!");
    }

    // --- ฟังก์ชันสำหรับปุ่มกด (Button Events) ---

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // กลับไป Scene ลำดับที่ 0
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