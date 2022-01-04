using UnityEngine;

public class AttachObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Attachable"))
        {
            collision.transform.SetParent(transform);
        }
    }
}
