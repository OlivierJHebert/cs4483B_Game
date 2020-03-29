using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public int HP;
    public int attack;
    public int speed;
    public int magic;
    
    void Start() {
        //PlayerPrefs.SetInt("HP", HP);
        //PlayerPrefs.SetInt("attack", attack);
        //PlayerPrefs.SetInt("speed", speed);
        //PlayerPrefs.SetInt("magic", magic);

        /* Loading stats from PlayerPrefs commented for testing
        if (PlayerPrefs.HasKey("HP")) {
            HP = PlayerPrefs.GetInt("HP");
            Debug.Log("HP: " + HP);
        }
        if (PlayerPrefs.HasKey("attack")) {
            attack = PlayerPrefs.GetInt("attack");
            Debug.Log("attack: " + attack);
        }
        if (PlayerPrefs.HasKey("speed")) {
            speed = PlayerPrefs.GetInt("speed");
            Debug.Log("speed: " + speed);
        }
        if (PlayerPrefs.HasKey("magic")) {
            magic = PlayerPrefs.GetInt("magic");
            Debug.Log("magic: " + magic);
        }
        */
    }
}