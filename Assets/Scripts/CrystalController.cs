using UnityEngine;
using UnityEngine.Audio;

public class CrystalWrapper : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string CRYSTAL_TAG = "Crystal";
    private AudioSource audioSource;

    private void Awake()
    {
        // calculate total crystals existing in the game
        CountManager.IncrementTotal();
        audioSource = GetComponent<AudioSource>();
    }
 
    private void OnTriggerEnter(Collider other)
    {
       


        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
             

          
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
            
                CountManager.IncrementCollected();    // increment the score when one crystal is collected
                if (audioSource != null)
                {
                    audioSource.Play(); //play sound when crystal is collected
                }                                     // Play the sound

            }
            else
            {
                Debug.Log("No child with 'crystal' tag found under CrystalWrapper.");
            }
        }

    }
}
