using Cinemachine;
using UnityEngine;

public class EnterWater : MonoBehaviour
{
    [SerializeField] private Transform humanRoot;
    [SerializeField] private Transform fishRoot;

    [SerializeField] private Transform humanPlayer;
    [SerializeField] private Transform fishPlayer;

    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

    private Transform currentPlayer; 
    private bool isUnderwater = false;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUnderwater) // Enter water only once
        {
            currentPlayer = other.transform;  // Store the current character entering water

            fishPlayer.transform.SetPositionAndRotation(currentPlayer.position, currentPlayer.rotation);

            // Disable current character and enable fish
            currentPlayer.gameObject.SetActive(false);
            fishPlayer.gameObject.SetActive(true);

            // Update camera target
            cinemachineCamera.Follow = fishRoot;

            isUnderwater = true; // Mark that we are underwater
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isUnderwater && other.CompareTag("Player")) // Ensure it's an actual exit
        {
            TransformBackToHuman();
        }
    }

    private void TransformBackToHuman()
    {
        Debug.Log("Transforming back to Human");

        // Move the player back to the fish's position
        humanPlayer.transform.SetPositionAndRotation(fishPlayer.position, fishPlayer.rotation);

        // Disable fish, enable human
        fishPlayer.gameObject.SetActive(false);
        humanPlayer.gameObject.SetActive(true);

        // Update camera
        cinemachineCamera.Follow = humanRoot;

        isUnderwater = false; // Reset underwater state
    }
}
