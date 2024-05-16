using UnityEngine;

public class RemoveQuestUI : MonoBehaviour
{
    public GameObject questPrefab; 
    public void DestroyQuestPrefab()
    {
        if (questPrefab != null)
        {
            Destroy(questPrefab);
        }
    }
}
