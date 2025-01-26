using Unity.VisualScripting;
using UnityEngine;

public class LetterInputObject : MonoBehaviour
{
    public string letter;
    private LetterInputController letterInputController;
    private void Start()
    {
        letterInputController = gameObject.GetComponentInParent<LetterInputController>();
    }
    private void OnMouseDown()
    {
        // TODO feedback sound
        Debug.Log(letter);
        letterInputController.AddLetterToString(letter);
    }
}
