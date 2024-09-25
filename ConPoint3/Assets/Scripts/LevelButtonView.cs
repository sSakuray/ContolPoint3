using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelIdlabel;
    [SerializeField] private List<GameObject> stars;

    public void SetupLevelButton(int levelId, int starsCount)
    {
        levelIdlabel.text = $"{levelId}";
        SetupStars(starsCount);
    }

    private void SetupStars(int startCount)
    {
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].SetActive(i < startCount); 
        }
    }

}

