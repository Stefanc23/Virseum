using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string aRModeSceneName;
    public string vRModeSceneName;
    
    public void LoadMainMenuScene() {
        SceneManager.LoadScene(0);
    }

    public void LoadARModeScene() {
        SceneManager.LoadScene(aRModeSceneName);
    }

    public void LoadVRModeScene() {
        SceneManager.LoadScene(vRModeSceneName);
    }

    public void LoadPreviousScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void Quit() {
        Application.Quit();
    }
}
