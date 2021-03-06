﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour {

    public int HP, attack, speed, magic, points;
    public Text pointsText, HPText, AttackText, SpeedText, MagicText;

    public void HPButton() 
    {
        points = PlayerPrefs.GetInt("points");
        if (points > 0) {
            allocatePoint("HP");
        }
    }

    public void AttackButton()
    {
        points = PlayerPrefs.GetInt("points");
        if (points > 0) {
            allocatePoint("attack");
        }
    }

    public void SpeedButton()
    {
        points = PlayerPrefs.GetInt("points");
        if (points > 0) {
            allocatePoint("speed");
        }        
    }

    public void MagicButton()
    {
        points = PlayerPrefs.GetInt("points");
        if (points > 0) {
            allocatePoint("magic");
        }        
    } 

    public void ConfirmButton() 
    {
        SceneManager.LoadScene(sceneName: "Level Select");
    }

    public void allocatePoint(string stat) {
        if (string.Compare("HP", stat) == 0 && HP < 5) {
            PlayerPrefs.SetInt("HP", HP+1);
            PlayerPrefs.SetInt("points", points-1);
        }
        else if (string.Compare("attack", stat) == 0 && attack < 5) {
            PlayerPrefs.SetInt("attack", attack+1);
            PlayerPrefs.SetInt("points", points-1);
        }
        else if (string.Compare("speed", stat) == 0 && speed < 5) {
            PlayerPrefs.SetInt("speed", speed+1);
            PlayerPrefs.SetInt("points", points-1);
        }
        else if (string.Compare("magic", stat) == 0 && magic < 5) {
            PlayerPrefs.SetInt("magic", magic+1);
            PlayerPrefs.SetInt("points", points-1);
        }
    }

    void Start() {
        PlayerPrefs.SetInt("points", 4);
        //temporary:
        //PlayerPrefs.SetInt("HP", 1); 
        //PlayerPrefs.SetInt("attack", 1);
        //PlayerPrefs.SetInt("speed", 3);
        //PlayerPrefs.SetInt("magic", 2);
    }

    void Update() {
        points = PlayerPrefs.GetInt("points");
        HP = PlayerPrefs.GetInt("HP");
        attack = PlayerPrefs.GetInt("attack");
        speed = PlayerPrefs.GetInt("speed");
        magic = PlayerPrefs.GetInt("magic");

        pointsText.text = "Points remaining: " + points.ToString();
        HPText.text = HP.ToString();
        AttackText.text = attack.ToString();
        SpeedText.text = speed.ToString();
        MagicText.text = magic.ToString();
    }
}
