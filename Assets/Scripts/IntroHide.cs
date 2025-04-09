using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyByTag : MonoBehaviour
{
    public string targetTag = "Intro";

    void Update()
    {
        //not using input system because this action is made one time and after the object is destroyed. 
        //No need to keep in memeory method during all the game
        bool spacePressed = Keyboard.current != null && Keyboard.current.xKey.wasPressedThisFrame;
        bool xPressed = Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame;

        if (spacePressed || xPressed)
        {
            GameObject target = GameObject.FindGameObjectWithTag(targetTag);
            if (target != null)
            {
                Destroy(target);
            }
        }
    }
}
