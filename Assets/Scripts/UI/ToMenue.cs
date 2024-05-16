
using UnityEngine;

using UnityEngine.SceneManagement;
public class ToMenue : MonoBehaviour
{   
    public void ENDGAME(int id)
    {
        
            SceneManager.LoadScene(id); // Load the win scene
        
    }
}
