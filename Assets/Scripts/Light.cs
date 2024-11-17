using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.HighDefinition.ProbeSettings;

public class LightActionController : MonoBehaviour
{
    [SerializeField] Animator m_Animator;
    [SerializeField] private GameObject m_MagicBall;
    private bool isLighting = false;
    private GameObject instantiatedLight;

    public void OnLight(InputValue value)
    {
        DoLight();
    }

    private void DoLight()
    {
        isLighting = !isLighting;
        m_Animator.SetBool("Lighting", isLighting);

        if (isLighting)
        {
            StartCoroutine("AddMagicBall", 1000f);
        }
        else
        {
            Destroy(instantiatedLight);
        }
    }

    private void AddMagicBall() {
        Vector3 spawnPosition = transform.position + transform.forward * 1.25f + transform.up * 1.3f;
        instantiatedLight = Instantiate(m_MagicBall, spawnPosition, Quaternion.identity);
        instantiatedLight.transform.SetParent(transform);
    }
}
