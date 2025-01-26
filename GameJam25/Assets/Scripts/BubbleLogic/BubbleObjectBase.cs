using StarterAssets;
using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BubbleObjectBase : MonoBehaviour
{
    // Distance to the Camera in which we can pop this bubble
    public float maxPopDistance = 30.0f;

    public float minDistanceToGround = 0.05f;

    public Rigidbody rigidBody;

    public BubbleType bubbleType;

    public AudioClip PopSound;

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
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = PopSound;
            //audioSource.pitch = 0.8f;
            audioSource.outputAudioMixerGroup = AudioManager.Instance.mixerGroup;
            audioSource.Play();
            
            Renderer rend = gameObject.GetComponent<Renderer>();
            rend.enabled = false;
            
            Destroy(gameObject, 1f);
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

        if (collidedObject.CompareTag("Player"))
        {
            OnPlayerExit();
            Debug.Log("Exit");
            return;
        }
    }

    protected virtual void OnPlayerEnter()
    {
        AudioManager.Instance.SetMixerEffectsByBubbleType(bubbleType);
    }

    protected virtual void OnPlayerExit()
    {
        AudioManager.Instance.ResetMixerEffects();
    }
}
