using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static BubbleObjectBase;

public class BubbleMakingComponent : MonoBehaviour
{
    private BubbleObjectBase currentBubble;
    private Queue<BubbleObjectBase> currentBubbleQueue;

    [SerializeField] private int maxBubbleNumber;
    [SerializeField] private Image textureUIImage;


    public int bubbleTypeIndex = 0;
    public BubbleType currentBubbleType;
    public List<BubbleType> usableBubbleTypes;
    public List<BubbleObjectBase> bubblePrefabs;

    // BUBBLE SPAWN PARAMETERS

    [SerializeField] private float bubbleMinSize;
    [SerializeField] private float bubbleMaxSize;
    [SerializeField] private float growToMaxSizeDuration;

    [SerializeField] private float spawnToCameraDistance;

    [SerializeField] private Vector3 defaultBubbleSize = new Vector3(1.0f, 1.0f, 1.0f);

    public float minDistanceToGround = 0.05f;

    [SerializeField] private float finishedBubbleForcefactor;

    // use for non linear bubble growth
    [SerializeField] AnimationCurve bubbleGrowCurve;

    public bool bIsGrowingBubble;
    public bool bCanBlowBubbles = true;

    Vector3 GizmoPos1;
    Vector3 GizmoPos2;

    private void Start()
    {
        currentBubbleQueue = new Queue<BubbleObjectBase>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SpawnBubble();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentBubbleType = GetUsuableTypeByIndex(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentBubbleType = GetUsuableTypeByIndex(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentBubbleType = GetUsuableTypeByIndex(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentBubbleType = GetUsuableTypeByIndex(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentBubbleType = GetUsuableTypeByIndex(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentBubbleType = GetUsuableTypeByIndex(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            currentBubbleType = GetUsuableTypeByIndex(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentBubbleType = GetUsuableTypeByIndex(7);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentBubbleType = GetUsuableTypeByIndex(8);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentBubbleType = GetUsuableTypeByIndex(0);
        }
    }

    // Blubberblasenblasende Blubberblasenfunction zum Blubberblasenblasen
    public void SpawnBubble()
    {
        if (!bCanBlowBubbles)
            return;

        Vector3 spawnPosition = GetRelativeClickPosition();

        // TODO dynamic ground position
        if (spawnPosition.y < minDistanceToGround)
        {
            spawnPosition.y = minDistanceToGround;
        }

        BubbleObjectBase bubblePrefab = GetBubblePrefabOfType(currentBubbleType);

        if (bubblePrefab == null)
            return;

        var newBubbleObject = GameObject.Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        newBubbleObject.transform.localScale = new Vector3(bubbleMinSize, bubbleMinSize, bubbleMinSize);

        currentBubble = newBubbleObject.GetComponent<BubbleObjectBase>();

        AddBubbleToQueue(currentBubble);

        StartCoroutine(GrowBubble());
    }

    private IEnumerator GrowBubble()
    {

        bIsGrowingBubble = true;
        float t = 0.0f;

        while (currentBubble != null && t <= growToMaxSizeDuration && Input.GetMouseButton(1))
        {
            // Increase size to max while mouse key is pressed

            // Get grow factor from curve
            // Make sure not to divide by 0
            t += Time.deltaTime;

            float tSave = t == 0.0f ? 0.001f : t;
            float growFactor = bubbleGrowCurve.Evaluate(tSave / growToMaxSizeDuration);

            float currentSize = bubbleMaxSize * growFactor;

            if (t >= bubbleMinSize)
            {
                currentBubble.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
            }

            // Move bubble here to not clash with update timing
            currentBubble.MoveTo(GetRelativeClickPosition() - GetBubbleSurfaceOffset(currentBubble));

            yield return 0;
        }

        if (currentBubble == null)
            yield return 0;

        if (t >= growToMaxSizeDuration)
        {
            currentBubble.PopBubble();
            bIsGrowingBubble = false;
            yield return 0;
        }

        if (t < bubbleMinSize)
        {
            // Bubble too small, use default size instead
            currentBubble.transform.localScale = defaultBubbleSize;
        }

        if (currentBubble != null)
        {
            // Apply little force at the end
            Vector3 forceDirection = (currentBubble.transform.position - Camera.main.transform.position).normalized;
            currentBubble.rigidBody.AddRelativeForce(forceDirection * finishedBubbleForcefactor, ForceMode.Force);
        }


        bIsGrowingBubble = false;
        yield return 0;
    }

    // Returns Position of mouseclick onscreen at set distance to camera
    private Vector3 GetRelativeClickPosition()
    {
        Vector3 clickPosition = Input.mousePosition;
        clickPosition.z = spawnToCameraDistance;
        clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);

        Vector3 direction = (clickPosition - Camera.main.transform.position).normalized;

        Vector3 relativeClickPosition = Camera.main.transform.position + (direction * spawnToCameraDistance);

        return relativeClickPosition;
    }

    // Returns Position offset from given bubbles center to Surface position facing camera
    private Vector3 GetBubbleSurfaceOffset(BubbleObjectBase bubble)
    {
        var collider = bubble.GetComponent<Collider>();
        float radius = (collider.bounds.size.y / 2);

        float minHeight = (collider.bounds.size.y / 2) + minDistanceToGround;

        Vector3 bubbleCenter = bubble.transform.position;
        Vector3 cameraPosition = Camera.main.transform.position;

        Vector3 direction = (cameraPosition - bubbleCenter).normalized;
        Vector3 surfaceOffset = (direction * radius);

        return surfaceOffset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(GizmoPos1, GizmoPos2);
    }

    public void AddBubbleToQueue(BubbleObjectBase newBubble)
    {
        currentBubbleQueue.Enqueue(newBubble);

        if (currentBubbleQueue.Count > maxBubbleNumber)
        {
            var bubbleToDelete = currentBubbleQueue.Dequeue();

            if (bubbleToDelete != null)
            {
                bubbleToDelete.PopBubble();
            }
        }
    }


    // BUBBLE TYPE LOGIC
    public BubbleObjectBase GetBubblePrefabOfType(BubbleType type)
    {
        foreach (BubbleObjectBase bubble in bubblePrefabs)
        {
            if (bubble.bubbleType == type)
            {
                return bubble;
            }
        }

        return null;
    }

    public BubbleType GetUsuableTypeByIndex(int index)
    {
        if (index > usableBubbleTypes.Count)
            return BubbleType.Default;

        var type = usableBubbleTypes[index];
        var bubble = GetBubblePrefabOfType(type);

        if (bubble == null)
            return BubbleType.Default;

        var renderer = bubble.gameObject.GetComponent<Renderer>();
        var material = renderer.sharedMaterial;

        SetUITexture(material);

        return usableBubbleTypes[index];
    }

    private void SetUITexture(Material material)
    {
        textureUIImage.material = material;
    }

}
