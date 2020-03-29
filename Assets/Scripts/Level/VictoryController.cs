using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryController : MonoBehaviour
{

    public GameObject[] Enemies;
    //Add other enemies as they are implemented in this format	
    
    // Update is called once per frame
    void Update()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (Enemies.Length == 0) 
        {
            if(PlayerPrefs.GetInt("curLevel") == 1) 
            {
                PlayerPrefs.SetInt("levelComp", PlayerPrefs.GetInt("levelComp") + 1);
                SceneManager.LoadScene("LevelUp");
            }
            else
            {
                SceneManager.LoadScene("Level Select");
            }
        }
    }
}
