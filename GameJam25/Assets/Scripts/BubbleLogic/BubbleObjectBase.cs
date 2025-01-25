using UnityEngine;

public abstract class BubbleObjectBase : MonoBehaviour
{
    // Distance to the Camera in which we can pop this bubble
    public float maxPopDistance = 30.0f;

    public float minDistanceToGround = 0.05f;

    public Rigidbody rigidBody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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

    void OnCollisionEnter(Collision collision)
    {
        var collidedObject = collision.gameObject;
        BubbleObjectBase collidedBubble = collidedObject.GetComponent<BubbleObjectBase>();

        if (collidedBubble != null)
        {
            // Maybe animation here
            return;
        }

        PopBubble();
    }

}
