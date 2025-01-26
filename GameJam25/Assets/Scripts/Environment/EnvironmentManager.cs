using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class EnvironmentManager
{
    private static Dictionary<EnvironmentComponent, EnvironmentComponentStateSnapshot> environmentComponentsWithSnapshot = new();

    /// <summary>
    /// Adds component for global modification.
    /// </summary>
    public static void Register (EnvironmentComponent component)
    {
        environmentComponentsWithSnapshot.Add(component, new EnvironmentComponentStateSnapshot(component));
    }

    /// <summary>
    /// Removes component from global modification.
    /// </summary>
    public static void Deregister (EnvironmentComponent component)
    {
        environmentComponentsWithSnapshot.Remove(component);
    }

    /// <summary>
    /// Restores all environment components to their original, unmodified state.
    /// </summary>
    public static void RestoreAll ()
    {
        foreach(var snapshot in environmentComponentsWithSnapshot.Values)
        {
            snapshot.Restore();
        }
    }

    /// <summary>
    /// Restore this exact environment component
    /// </summary>
    /// <param name="component"></param>
    public static void Restore (EnvironmentComponent component)
    {
        environmentComponentsWithSnapshot[component].Restore();
    }

    /// <summary>
    /// Modify either all registered, or all components with layerName
    /// </summary>
    /// <param name="modification"></param>
    /// <param name="layerName"></param>
    public static void Modify (Action<EnvironmentComponent> modification, string layerName = null)
    {
        foreach(var component in environmentComponentsWithSnapshot.Keys.Where(c => layerName == null || c.componentLayer == layerName))
        {
            modification(component);
        }
    }
}


public class EnvironmentComponentStateSnapshot
{
    private EnvironmentComponent component;

    private Material material;
    private MeshRenderer meshRenderer;
    private bool isActive;
    private bool isRendering;

    public EnvironmentComponentStateSnapshot (EnvironmentComponent component)
    {
        this.component = component;
        material = component.GetComponent<MeshRenderer>().material;
        isActive = true;
        isRendering = component.GetComponent<MeshRenderer>().enabled;
    }

    /// <summary>
    /// Applies all recorded snapshot data to given component.
    /// </summary>
    public void Restore ()
    {
        meshRenderer = component.GetComponent<MeshRenderer>();
        component.GetComponent<MeshRenderer>().material = material;
        component.gameObject.SetActive(isActive);
        meshRenderer.enabled = isRendering;
    }
}
