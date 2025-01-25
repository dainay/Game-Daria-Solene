using UnityEngine;

public class BillboardController : MonoBehaviour
{
    [SerializeField] private Transform m_Camera;

    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.forward);
    }
}
