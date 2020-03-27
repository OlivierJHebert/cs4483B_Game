using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public int HP;
    public int attack;
    public int speed;
    public int magic;

    public void allocatePoint(string stat) {
        if (string.Compare("HP", stat) == 0 && HP < 5) {
            HP += 1;
            PlayerPrefs.SetInt("HP", HP);
        }
        else if (string.Compare("attack", stat) == 0 && attack < 5) {
            attack += 1;
            PlayerPrefs.SetInt("attack", attack);
        }
        else if (string.Compare("speed", stat) == 0 && speed < 5) {
            speed += 1;
            PlayerPrefs.SetInt("speed", speed);
        }
        else if (string.Compare("magic", stat) == 0 && magic < 5) {
            magic += 1;
            PlayerPrefs.SetInt("magic", magic);
        }
    }
    
    void Start() {
        PlayerPrefs.SetInt("HP", HP);
        PlayerPrefs.SetInt("attack", attack);
        PlayerPrefs.SetInt("speed", speed);
        PlayerPrefs.SetInt("magic", magic);

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