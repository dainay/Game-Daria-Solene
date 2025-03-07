using TMPro; // Import TextMeshPro namespace
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text crystalScoreText;

    public static ScoreDisplay Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the global instance
        }
        else
        {
            Debug.LogError("Multiple ScoreDisplay instances detected!");
            Destroy(gameObject); // Destroy duplicates
        }
    }
    private void Start()
    {
        UpdateScore(0, 0); // Call function wiht 0 0 to have initial value
    }

    public void UpdateScore(int collectedCount, int totalCount)
    {
        Debug.Log("DDDDDDDDDDDDDDDD: " + collectedCount + totalCount);

        if (crystalScoreText == null)
        {
            Debug.LogError("CrystalScoreText is not assigned in the Inspector!");
        }

        if (crystalScoreText == null)
        {
            Debug.LogError("CrystalScoreText is not assigned in ScoreDisplay!");
            return;
        }

        crystalScoreText.text = $"Crystals: {collectedCount}/{totalCount}";
    }

}
 
 