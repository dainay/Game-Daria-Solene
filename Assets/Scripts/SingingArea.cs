using UnityEngine;
using UnityEngine.InputSystem;

public class RuneMusic : MonoBehaviour
{
    [SerializeField] private Transform[] crystals;  // crystals of this rock
    [SerializeField] private float lowerDistance = 5f;  // distance to lower the crystals
    [SerializeField] private float speed = 2f;  

    private Vector3[] initialPositions; // to stock initial positions of the crystals
    private bool playerInZone = false;

    private PlayerInput playerInput; //needed to connect input assets system

    private void Start()
    {
        initialPositions = new Vector3[crystals.Length];
        for (int i = 0; i < crystals.Length; i++)
        {
            //stock initial positions and lower the crystals
            initialPositions[i] = crystals[i].position;
            crystals[i].position -= Vector3.up * lowerDistance;
        }

        //connect input assets system
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        playerInput.actions["Singing"].performed += ctx => OnSinging(true);
        playerInput.actions["Singing"].canceled += ctx => OnSinging(false);
}

    public void OnSinging(bool isPressed)
    {
        if (playerInZone && isPressed) // Move the crystals up when the player is in the zone and singing
        {
            //Invokes the method in time seconds, then repeatedly every repeatRate seconds

            InvokeRepeating(nameof(MoveCrystalsUp), 0f, 0.02f);  
        }
        else
        {
            CancelInvoke(nameof(MoveCrystalsUp));   
        }
    }

    private void MoveCrystalsUp()
    {
        bool allReached = true; //let's assume all crystals are in place

        for (int i = 0; i < crystals.Length; i++) 
        {
            if (crystals[i].position != initialPositions[i]) //if the crystal is not in place move it
            {
                crystals[i].position = Vector3.MoveTowards(crystals[i].position, initialPositions[i], speed * Time.deltaTime);
                allReached = false;  // if at least one crystal is not in place, allReached is false
            }
        }

        //if all crystals are in place, stop invoking the method
        if (allReached)
        {
            CancelInvoke(nameof(MoveCrystalsUp));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            CancelInvoke(nameof(MoveCrystalsUp));  //stop moving if character left zone of the stone
        }
    }
}
