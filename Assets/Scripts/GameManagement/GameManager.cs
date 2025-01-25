using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("DontDestroyOnLoad")]
    [SerializeField] private GameObject[] _dontDestroyOnLoad;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    private void Start()
    {
        foreach (var element in _dontDestroyOnLoad)
        {
            DontDestroyOnLoad(element); // Permet la conservation de tout les gameobjects lors des changemnt de niveau
        }
    }

    public void LoadScene(string sceneName)
    {
        switch (sceneName)
        {
            default:
                Debug.Log("Scene not found");
                break;
            case "Level_01":
                SceneManager.LoadScene("S_Level_01");
                break;
            case "Level_02":
                SceneManager.LoadScene("S_Level_02");
                break;
            case "Level_03":
                SceneManager.LoadScene("S_Level_03");
                break;
            case "Level_04":
                SceneManager.LoadScene("S_Level_04");
                break;
            case "Test":
                SceneManager.LoadScene("S_Test_Mechaniques");
                break;
            case "Level_Kitchen":
                SceneManager.LoadScene("S_Level_Kitchen");
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("S_Level_01");
    }
}
