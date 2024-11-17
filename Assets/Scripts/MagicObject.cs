using UnityEngine;

public class MagicObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string magicTag = gameObject.tag;
            Debug.Log(magicTag);
            MagicCollectionManager.AddTag(magicTag); // Add tag to the table
            Destroy(gameObject); // Destroy this object
        }
    }
 
}
