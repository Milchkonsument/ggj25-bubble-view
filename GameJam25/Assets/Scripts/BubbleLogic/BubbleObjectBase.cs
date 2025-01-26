using StarterAssets;
using System;
using UnityEngine;

public abstract class BubbleObjectBase : MonoBehaviour
{
    // Distance to the Camera in which we can pop this bubble
    public float maxPopDistance = 30.0f;

    public float minDistanceToGround = 0.05f;

    public Rigidbody rigidBody;

    public BubbleType bubbleType;

    public enum BubbleType
    {
        Default,
        Reflection,
        BlackAndWhite,
        RedFilter,
        BlueFilter,
        GreenFilter,
        Light,
        Rage,
        Void,
        XRay,
    }

    private void OnMouseDown()
    {
        PopBubble();
    }

    public void PopBubble()
    {
        // TODO animation stuff


        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }

    public void MoveTo(Vector3 position)
    {
        //position = AdjustHeightForGround(position);
        rigidBody.MovePosition(position);
    }

    void OnTriggerEnter(Collider collider)
    {
        var collidedObject = collider.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            OnPlayerEnter();
            return;
        }

        if (collidedObject.TryGetComponent<BubbleObjectBase>(out var collidedBubble) == false)
        {
            PopBubble();
            Debug.Log("WHY?");
            return;
        }
    }

    private void OnTriggerExit (Collider collider)
    {
        var collidedObject = collider.gameObject;

        if (collidedObject.TryGetComponent<FirstPersonController>(out var player))
        {
            OnPlayerExit();
            return;
        }
    }

    protected virtual void OnPlayerEnter()
    {

    }

    protected virtual void OnPlayerExit()
    {

    }
}
