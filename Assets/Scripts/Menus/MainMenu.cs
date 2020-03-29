using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartButtonClicked() 
    {
        SceneManager.LoadScene("Level Select");
    }
    
    public void OnDelSaveButtonClicked() 
    {
        PlayerPrefs.DeleteAll();
    }
    
    public void OnExitButtonClicked() 
    {
        Application.Quit();
    }
}
