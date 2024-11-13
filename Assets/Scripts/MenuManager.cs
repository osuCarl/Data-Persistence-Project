using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public ScoreData currentData;
    public string playerName;
    public TextMeshProUGUI InputField;
    public TextMeshProUGUI BestScore;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        LoadData();
        BestScore.text = $"BestScore: {currentData.username}: {currentData.bestScore}";
        DontDestroyOnLoad(this);
    }

    public void SaveData()
    {
        if (currentData != null)
        {
            string json = JsonUtility.ToJson(currentData);
            string path = Application.persistentDataPath + "/savedata.json";
            File.WriteAllText(path, json);
            Debug.Log("data " + currentData.toString() + " saved to location " + path);
        }
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json  = File.ReadAllText(path);
            currentData = JsonUtility.FromJson<ScoreData>(json);
        } else
        {
            currentData = new ScoreData();
            currentData.bestScore = 0;
            currentData.username = String.Empty;
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        playerName = InputField.text;
        SceneManager.LoadScene(1);
    }
}

[Serializable]
public class ScoreData
{
    public int bestScore;
    public string username;

    public string toString()
    {
        return $"bestScore: {bestScore}, username: {username}";
    }
}
