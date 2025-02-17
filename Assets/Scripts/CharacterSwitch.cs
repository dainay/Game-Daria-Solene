using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] private GameObject nextCharacter; //   (Armature)
    [SerializeField] private Transform nextFollowTarget; // camera root
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera; // player follow camera

    public void OnCharacterSwitch()
    {
        Vector3 lastPosition = transform.position;
        Quaternion lastRotation = transform.rotation;

    
        gameObject.SetActive(false);

        nextCharacter.transform.SetPositionAndRotation(lastPosition, lastRotation);
         
        nextCharacter.SetActive(true);

        
        cinemachineCamera.Follow = nextFollowTarget;

        Debug.Log($"Switched character");
    }
}
