using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Search : MonoBehaviour
{
    private const string SearchingBool = "Searching"; // Trigger for dog's animation
    private const string FlyingUpBool = "FlyingUp";   // Trigger for crystals' animation

    [SerializeField] private Animator m_AnimatorUser; // Animator for the dog

    private List<GameObject> allCrystals = new List<GameObject>(); // Cached list of all crystals
    private List<Animator> crystalAnimators = new List<Animator>(); // Cached list of animators

    private void Start()
    {
        //Collect all crystals and their animators at the start to not do it every time 
        //when we user serach for dog
        GameObject[] foundCrystals = GameObject.FindGameObjectsWithTag("Crystal");

        foreach (GameObject crystal in foundCrystals)
        {
            if (crystal.TryGetComponent(out Animator crystalAnimator))
            {
                allCrystals.Add(crystal);
                crystalAnimators.Add(crystalAnimator);
            }
        }

    }

    public void OnSearching(InputValue value)
    {
        Debug.Log("Searching CALLED");

        bool isPressed = value.isPressed;

        if (isPressed)
        {
            // Dog starts the search animation
            m_AnimatorUser.SetBool(SearchingBool, true);
            Debug.Log("Searching is TRUE");


            ActivateCrystalAnimations();
        }
        else
        {
            // Dog stops the search animation
            m_AnimatorUser.SetBool(SearchingBool, false);
            Debug.Log("Searching is FALSE");

            DeactivateCrystalAnimations();
        }
    }

    private void ActivateCrystalAnimations()
    {
        // clean the list from collected crystals
        RemoveDestroyedCrystals();

        foreach (Animator animator in crystalAnimators)
        {
            animator.SetBool(FlyingUpBool, true);
        }

        Debug.Log("Activated animation for all available crystals.");
    }

    private void DeactivateCrystalAnimations()
    {
        foreach (Animator animator in crystalAnimators)
        {
            animator.SetBool(FlyingUpBool, false);
        }

        Debug.Log("Deactivated animation for all available crystals.");
    }

    private void RemoveDestroyedCrystals()
    {
        for (int i = allCrystals.Count - 1; i >= 0; i--)
        //cleaneg collected crydtsld from back to front to not have problems with indexes
        {
            if (allCrystals[i] == null)
            {
                allCrystals.RemoveAt(i);
                crystalAnimators.RemoveAt(i);
            }
        }

    }
}
