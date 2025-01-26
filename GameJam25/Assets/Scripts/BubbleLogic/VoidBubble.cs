using System.ComponentModel;
using UnityEngine;

public class VoidBubble : BubbleObjectBase
{
    protected override void OnPlayerEnter ()
    {
        base.OnPlayerEnter();
        EnvironmentManager.Modify(c => c.GetComponent<MeshRenderer>().enabled = true, "textActivate");
    }

    protected override void OnPlayerExit ()
    {
        base.OnPlayerExit();
        EnvironmentManager.RestoreAll();
    }
}
