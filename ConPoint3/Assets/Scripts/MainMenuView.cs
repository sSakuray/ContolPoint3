using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private RectTransform root;
    [SerializeField] private int levelCount;
    [SerializeField] private Button backButton;
    [SerializeField] private int lockedLevelStartIndex;
    [SerializeField] private GameObject LockedLevelButton;

    [Header("Stars Settings")]
    [SerializeField] private StarsSetting[] starsSettings;
    

    private const int _minStarsCount = 0;
    private const int _maxStarsCount = 4;

    void Start()
    {
        levelMenu.SetActive(false);
        playButton.onClick.AddListener(OnPlayButtonPressed);
        backButton.onClick.AddListener(OnBackButtonPressed);
    }

    private void OnPlayButtonPressed()
    {
        playButton.gameObject.SetActive(false);
        ClearLevelMenu();
        FillLevelMenu();
        levelMenu.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    private void OnBackButtonPressed()
    {
        levelMenu.SetActive(false);
        playButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonPressed);
        backButton.onClick.RemoveListener(OnBackButtonPressed);
    }

    private void FillLevelMenu()
    {
        ClearLevelMenu();
        for (int i = 0; i < levelCount; i++)
        {
            GameObject levelButton;
            if (i >= lockedLevelStartIndex)
            {
                levelButton = Instantiate(LockedLevelButton, root);
                LockedLevelButton.SetActive(true);
            }
            else
            {
                levelButton = Instantiate(levelButtonPrefab, root);
                if (levelButton.TryGetComponent(out LevelButtonView levelButtonView))
                {
                    levelButtonView.SetupLevelButton(i + 1,
                        /*UnityEngine.Random.Range(_minStarsCount, _maxStarsCount)*/
                        GetStarsForLevel(i + 1));
                }
            }
        }
    }

    private void ClearLevelMenu()
    {
        for (int i = 0; i < root.childCount; i++)
        {
            Destroy(root.GetChild(i).gameObject);
        }
    }

    private int GetStarsForLevel(int levelId)
    {
        for (int i = 0; i < starsSettings.Length; i++)
        {
            if(levelId == starsSettings[i].Level)
                return starsSettings[i].StarsCount;
        }
        return 0;
    }
}

[Serializable]
public class StarsSetting
{   
    [SerializeField] public int Level;
    [SerializeField] public int StarsCount;
}

