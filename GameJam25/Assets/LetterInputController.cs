using System;
using UnityEngine;

public class LetterInputController : MonoBehaviour
{
    public Action OnSolutionFound;
    public string solution = "AMELIA";
    private string currentString = "";
    public AudioSource audioSource;
    public void AddLetterToString(string letter)
    {
        audioSource.Play();
        
        if (!solution.Contains(letter))
        {
            currentString = "";
            return;
        }

        currentString += letter;

        if (currentString == solution)
        {
            FoundSolution();
        }
    }

    public void FoundSolution()
    {
        Debug.Log("WINNER!");

        if (OnSolutionFound != null)
        {
            OnSolutionFound.Invoke();
        }
    }
}
