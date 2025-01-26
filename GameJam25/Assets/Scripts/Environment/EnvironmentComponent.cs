using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EnvironmentComponent : MonoBehaviour
{
    public string componentLayer;

    private void OnEnable ()
    {
        EnvironmentManager.Register(this);
    }

    private void OnDisable ()
    {
        EnvironmentManager.Deregister(this);
    }
}
