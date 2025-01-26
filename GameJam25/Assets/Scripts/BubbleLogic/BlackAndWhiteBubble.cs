using UnityEngine;

public class BlackAndWhiteBubble : BubbleObjectBase
{
    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();

        EnvironmentManager.Modify(c => c.gameObject.GetComponent<MeshRenderer>().enabled = true, "reflectionActivate");
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();

        EnvironmentManager.RestoreAll();
    }
}
