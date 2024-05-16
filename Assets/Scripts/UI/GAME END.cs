using UnityEngine;
using UnityEngine.SceneManagement;
public class GAMEEND : MonoBehaviour
{
    public string winSceneName; // Name of the scene to load when the game ends

    public void ENDGAME()
    {
        if (!string.IsNullOrEmpty(winSceneName))
        {
            SceneManager.LoadScene(winSceneName); // Load the win scene
        }
        else
        {
            Debug.LogWarning("Win scene name is not set!");
        }
    }
}
