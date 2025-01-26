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
            case "Hub":
                SceneManager.LoadScene("S_Hub");
                break;
            case "Level_Kitchen":
                SceneManager.LoadScene("S_Level_Kitchen");
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("S_Hub");
    }
}
