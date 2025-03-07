using UnityEngine;

public class BillboardController : MonoBehaviour
{
    [SerializeField] private Transform Camera;

    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.forward);
    }
}
