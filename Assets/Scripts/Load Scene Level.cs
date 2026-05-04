using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneLevel : MonoBehaviour
{
    [Header("ชื่อด่านต่อไปที่ต้องการโหลด")]
    public string sceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. เช็กว่าคนที่มาชนประตูคือผู้เล่นใช่หรือไม่
        if (other.gameObject.CompareTag("Player"))
        {
            // 2. ค้นหากรรมการ (LevelManager) ที่อยู่ในด่าน เพื่อขอดูคะแนน
            LevelManager manager = FindFirstObjectByType<LevelManager>();

            if (manager != null)
            {
                // 3. เช็กว่าผลไม้ที่เก็บได้ (currentFruits) มากกว่าหรือเท่ากับ เป้าหมาย (fruitsToWin) หรือยัง
                if (manager.currentFruits >= manager.fruitsToWin)
                {
                    Debug.Log("เก็บผลไม้ครบแล้ว! กำลังโหลดด่านต่อไป...");
                    StartCoroutine(LoadSceneGame(sceneName));
                }
                else
                {
                    int fruitsLeft = manager.fruitsToWin - manager.currentFruits;
                    Debug.Log("ยังเข้าไม่ได้! ต้องไปเก็บผลไม้เพิ่มอีก " + fruitsLeft + " ลูกนะ");
                }
            }
        }
    }

    public IEnumerator LoadSceneGame(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            Debug.Log("กำลังโหลด: " + (progress * 100).ToString("n0") + "%");

            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}