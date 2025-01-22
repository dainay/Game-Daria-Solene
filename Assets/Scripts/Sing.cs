using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.HighDefinition.ProbeSettings;

public class Sing : MonoBehaviour
{
    [SerializeField] private Animator m_AnimatorUser;
    [SerializeField] private AudioClip[] singingClips;
    [SerializeField] private AudioSource audioSource;

    public void OnSinging(InputValue value)
    {
        Debug.Log("Singing CALLED");

        bool isPressed = value.isPressed;

        if (isPressed)
        {
            m_AnimatorUser.SetBool("SingingBool", true);
            PlayRandomAudio();
            Debug.Log("Singing is TRUE");
        }
        else
        {
            m_AnimatorUser.SetBool("SingingBool", false);
            Debug.Log("Singing is FAALSE");
            audioSource.Stop();
        }
    }

    private void PlayRandomAudio()
    {
        if (singingClips.Length > 0)
        {
            audioSource.clip = singingClips[Random.Range(0, singingClips.Length)];
            audioSource.Play();
        }
    }
}
