using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string targetPortalTag; //  target portal
    [SerializeField] private float offsetDistance = 3.0f; // Distance to move forward from the target portal
    [SerializeField] private AudioClip teleportingSound; 
    private GameObject player; 
    private CharacterController characterController; // Reference to the CharacterController
    private AudioSource audioSource; // Reference to the AudioSource connected to the portal

    private const string PLAYER_TAG = "Player";

    private void Start()
    {
        // Find the player object by tag
        player = GameObject.FindWithTag(PLAYER_TAG);

        if (player == null)
        {
            Debug.LogError("Player not found");
            return;
        }

        // Get the CharacterController from the player.
        // Need to disconnect it during teleportation because it conflicts with the moving made by script.
        // If not disconnected, the portals work not every time
        characterController = player.GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController not found !!");
        }

        // Get AudioSource from the portal 
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found on the portal");
        }
    }
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag(PLAYER_TAG) && characterController != null)
        {
         
            GameObject targetPortal = GameObject.FindWithTag(targetPortalTag); //found the target portal by tag

            if (targetPortal != null)
            {
                // Calculate the new position
                Vector3 targetPosition = targetPortal.transform.position;
                Vector3 offset = targetPortal.transform.forward * offsetDistance;
                Vector3 newPosition = targetPosition + offset;

                // Disable the CharacterController temporarily to allow position change
                characterController.enabled = false;
                player.transform.position = newPosition;
                characterController.enabled = true;

                Debug.Log($"Player teleported to {newPosition}");

                // Play teleport sound
                if (teleportingSound != null)
                {
                    audioSource.PlayOneShot(teleportingSound);
                }
                else
                {
                    Debug.LogWarning("Teleporting sound clip is not assigned!");
                }
            }
            else
            {
                Debug.LogError($"No portal found with tag {targetPortalTag}");
            }
        }
    }
}
