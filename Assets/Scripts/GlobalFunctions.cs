using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalFunctions : MonoBehaviour
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LoadScene(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    public IEnumerator LoadScene(int sceneIndex, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneIndex);
    }
    
    public static void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Quit Game");
    }
}
