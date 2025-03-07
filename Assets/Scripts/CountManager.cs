using UnityEngine;

public class CountManager : MonoBehaviour
{
    public static int TotalCount = 0;
    public static int CollectedCount = 0;
    private void Start()
    {
        // Initialize the score display
        if (ScoreDisplay.Instance != null)
        {
            ScoreDisplay.Instance.UpdateScore(CollectedCount, TotalCount);
        }
        else
        {
            Debug.LogError("ScoreDisplay.Instance is null! Ensure a ScoreDisplay exists in the scene.");
        }
    }
    public static void IncrementTotal()
    {
        TotalCount++;
        Debug.Log("Total crystals in scene: " + TotalCount);
        if (ScoreDisplay.Instance != null)
        {
            ScoreDisplay.Instance.UpdateScore(CollectedCount, TotalCount);
        }
    }
    public static void IncrementCollected()
    {
        CollectedCount++;
        Debug.Log("Crystals collected: " + CollectedCount);
        if (ScoreDisplay.Instance != null)
        {
            ScoreDisplay.Instance.UpdateScore(CollectedCount, TotalCount);
        }
    }     
}
