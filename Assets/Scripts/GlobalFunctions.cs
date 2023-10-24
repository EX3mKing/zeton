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
    
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    
    public static void QuitGame()
    {
        Application.Quit();
        Debug.LogWarning("Quit Game");
    }
}
