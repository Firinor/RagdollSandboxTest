using UnityEngine;

public class SetObjectToStart : MonoBehaviour
{
    private void OnEnable()
    {
        EnableColliders();
        EnableRigidbodies();
        ResetColor();
        Destroy(this);
    }

    private void EnableColliders()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
    }
    private void EnableRigidbodies()
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (var body in bodies)
        {
            body.isKinematic = false;
        }
    }
    private void ResetColor()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var render in renderers)
        {
            foreach (var material in render.materials)
            {
                material.color = Color.white;
            }
        }
    }
}
