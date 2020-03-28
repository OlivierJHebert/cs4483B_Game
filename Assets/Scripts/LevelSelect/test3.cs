using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPauseButtonClicked() 
    {
                PlayerPrefs.SetInt("levelComp", 5);
        SceneManager.LoadScene("Level Select");
    }
}
