using UnityEngine;

public class SetObjectToStart : MonoBehaviour
{
    private static Color blueprintColor = new Color(0.2f, 1, 0.2f, 0.2f);

    private void Awake()
    {
        SwitchColliders(isEnabled: false);
        SwitchRigidbodies(isKinematic: true);
        SwitchColor(newColor: blueprintColor);
        enabled = false;
    }
    private void OnEnable()
    {
        SwitchColliders(isEnabled: true);
        SwitchRigidbodies(isKinematic: false);
        SwitchColor(newColor: Color.white);
        Destroy(this);
    }

    private void SwitchColliders(bool isEnabled)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = isEnabled;
        }
    }
    private void SwitchRigidbodies(bool isKinematic)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (var body in bodies)
        {
            body.isKinematic = isKinematic;
        }
    }
    private void SwitchColor(Color newColor)
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var render in renderers)
        {
            foreach (var material in render.materials)
            {
                material.color = newColor;
            }
        }
    }
}
