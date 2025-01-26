using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject winText;
    public List<GameObject> texts = new();
    public Button button;
    private int index = 0;

    private bool isWin = false;

    private void Start ()
    {
        AudioManager.Instance.SetMixerEffectsByBubbleType(BubbleObjectBase.BubbleType.Void);

        foreach(var item in texts)
        {
            item.SetActive(false);
        }

        winText.SetActive(false);

        texts[index].SetActive(true);
    }

    private void Update ()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (isWin)
            {
                return;
            }

            index++;
            texts[index - 1].SetActive(false);

            if(index == texts.Count)
            {
                AudioManager.Instance.ResetMixerEffects();
                gameObject.SetActive(false);
                return;
            }

            texts[index].SetActive(true);

        }
    }

    public void OnWin ()
    {
        isWin = true;
        gameObject.SetActive(true);

        foreach(var item in texts)
        {
            item.SetActive(false);
        }

        winText.SetActive(true);

        AudioManager.Instance.SetMixerEffectsByBubbleType(BubbleObjectBase.BubbleType.Void);

        StartCoroutine(waitandclose());
    }

    private IEnumerator waitandclose()
    {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
