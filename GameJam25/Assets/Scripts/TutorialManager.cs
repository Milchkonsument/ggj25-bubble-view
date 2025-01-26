using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> texts = new();
    public Button button;
    private int index = 0;

    private void Start ()
    {
        AudioManager.Instance.SetMixerEffectsByBubbleType(BubbleObjectBase.BubbleType.Void);

        foreach(var item in texts)
        {
            item.SetActive(false);
        }

        texts[index].SetActive(true);

        button.onClick.AddListener(() =>
        {
            index++;
            texts[index - 1].SetActive(false);

            if(index == texts.Count)
            {
                AudioManager.Instance.ResetMixerEffects();
                Destroy(gameObject);
                return;
            }

            texts[index].SetActive(true);

        });
    }
}
