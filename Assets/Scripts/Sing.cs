using UnityEngine;
using UnityEngine.InputSystem;

public class Sing : MonoBehaviour
{
    [SerializeField] private Animator m_AnimatorUser;

    void Update()
    {
        bool isPressed = Keyboard.current.jKey.isPressed;

        if (isPressed)
        {
            Debug.Log("Key is being held.");
            m_AnimatorUser.SetBool("SingingBool", true);
        }
        else
        {
            Debug.Log("Key is released.");
            m_AnimatorUser.SetBool("SingingBool", false);
        }
    }

}
