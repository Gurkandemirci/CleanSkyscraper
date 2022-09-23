using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : MonoBehaviour
{
    int levelIndex;
    private void Awake()
    {
        levelIndex = PlayerPrefs.GetInt("Level");
        if (levelIndex == 0)
        {
            levelIndex = 2;
            PlayerPrefs.SetInt("Level", levelIndex);
        }
        SceneManager.LoadScene(levelIndex);
    }
}