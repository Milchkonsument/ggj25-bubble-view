using UnityEngine;

public class ReflectionBubble : BubbleObjectBase
{
    public Material reflectionMaterial;

    protected override void OnPlayerEnter ()
    {
        base.OnPlayerEnter();

        EnvironmentManager.Modify(comp => comp.GetComponent<MeshRenderer>().material = reflectionMaterial);
    }

    protected override void OnPlayerExit ()
    {
        base.OnPlayerExit();

        EnvironmentManager.RestoreAll();
    }
}
