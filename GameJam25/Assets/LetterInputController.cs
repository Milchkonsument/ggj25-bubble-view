using System;
using UnityEngine;

public class LetterInputController : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;

    public Action OnSolutionFound;
    public string solution = "AMELIA";
    private string currentString = "";
    public void AddLetterToString(string letter)
    {
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

        tutorialManager.OnWin();
    }
}
