using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuController : MonoBehaviour
{
    
    public void PlayGame()
    {
      
        SceneManager.LoadScene(1); 
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
