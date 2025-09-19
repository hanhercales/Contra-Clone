using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    private void Start()
    {
        if (continueButton == null) return;
        if(!PlayerPrefs.HasKey("CurrentScene")) 
            continueButton.interactable = false;
        continueButton.interactable = true;
    }

    public void NewGame()
    {
        ChangeScene(1);
    }

    public void ContinueGame()
    {
        ChangeScene(PlayerPrefs.GetInt("CurrentScene"));
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ToMainMenu(int sceneIndex)
    {
        PlayerPrefs.SetInt("CurrentScene", sceneIndex);
        PlayerPrefs.Save();
        Debug.Log("Saved Current Scene" + PlayerPrefs.GetInt("CurrentScene"));
        ChangeScene(0);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
