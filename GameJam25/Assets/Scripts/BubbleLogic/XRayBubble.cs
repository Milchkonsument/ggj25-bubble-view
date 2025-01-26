using UnityEngine;

public class XRayBubble : BubbleObjectBase
{
    public Material xrayMaterial;
    public Material redMaterial;

    protected override void OnPlayerEnter ()
    {
        base.OnPlayerEnter();

        EnvironmentManager.Modify(comp => comp.GetComponent<MeshRenderer>().material = xrayMaterial);
        EnvironmentManager.Modify(comp => comp.GetComponent<MeshRenderer>().material = redMaterial, "red");
    }

    protected override void OnPlayerExit ()
    {
        base.OnPlayerExit();

        EnvironmentManager.RestoreAll();
    }
}
