using UnityEngine;

public class CrystalWrapper : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string CRYSTAL_TAG = "Crystal";
    public static int COUNT = 0;
    public static int TOTAL = 0;

    private void Awake()
    {
        Debug.Log("awake");
        Debug.Log(PLAYER_TAG);
        TOTAL++;
    }

    private void Update()
    {
        Debug.Log(TOTAL);
        Debug.Log(COUNT);

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{other} entered");


        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            // Add score and collect crystal
            //ScoreManager.Instance.AddScore();

          
            Transform crystal = null;

             
            foreach (Transform child in transform)
            {
                if (child.CompareTag(CRYSTAL_TAG))
                {
                    crystal = child;
                    break;
                }
            }

            if (crystal != null)
            {
                Debug.Log("Found crystal with 'crystal' tag, destroying it.");
                Destroy(crystal.gameObject);
                COUNT++;
            }
            else
            {
                Debug.Log("No child with 'crystal' tag found under CrystalWrapper.");
            }
        }

    }
}
