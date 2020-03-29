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
        PlayerPrefs.SetInt("levelComp", 0);
        PlayerPrefs.SetInt("points", 0);
        PlayerPrefs.SetInt("HP", 12);
        PlayerPrefs.SetInt("magic", 1);
        PlayerPrefs.SetInt("attack", 1);
        PlayerPrefs.SetInt("speed", 1);
    }
    
    public void OnExitButtonClicked() 
    {
        Application.Quit();
    }
}
