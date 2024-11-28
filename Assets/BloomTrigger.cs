using UnityEngine;

public class CrystalBloom : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string BLOOM_ANIM_PARAM = "Bloom"; 
    private Animator animator;


    private void Start()
    {
        
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator  is not found for this object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
         
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
          
            animator.SetBool(BLOOM_ANIM_PARAM, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
         
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            animator.SetBool(BLOOM_ANIM_PARAM, false);
        }
    }
}
