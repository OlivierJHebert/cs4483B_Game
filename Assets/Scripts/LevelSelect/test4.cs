using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test4 : MonoBehaviour
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
                PlayerPrefs.SetInt("levelComp", 6);
        SceneManager.LoadScene("Level Select");
    }
}
