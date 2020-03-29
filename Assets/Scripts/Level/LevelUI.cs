using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public int HPCurrent, currentHearts, partialHeart, maxHearts, attack, speed, magic;
    public GameObject player;
    private PlayerHealth playerHealth;
    public Text attackText, speedText, magicText;
    public Image[] hearts;
    public Sprite fullHeart, emptyHeart, quarterHeart, halfHeart, threequarterHeart;

    void Start() {
        playerHealth = player.GetComponent<PlayerHealth>();
        PlayerPrefs.SetInt("HP", 4);
    }

    void Update()
    {
        maxHearts = PlayerPrefs.GetInt("HP");
        HPCurrent = playerHealth.getHealth();
        Debug.Log(HPCurrent);
        currentHearts = HPCurrent / 4;
        partialHeart = HPCurrent % 4;

        for (int i = 0; i < hearts.Length; i++) {
            if (i < currentHearts) hearts[i].sprite = fullHeart;
            else if (partialHeart == 1) {
                hearts[i].sprite = quarterHeart;
                partialHeart = 0;
            }
            else if (partialHeart == 2) {
                hearts[i].sprite = halfHeart;
                partialHeart = 0;
            }
            else if (partialHeart == 3) {
                hearts[i].sprite = threequarterHeart;
                partialHeart = 0;
            }
            else hearts[i].sprite = emptyHeart;

            if (i < maxHearts) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
                break;
            }
        }

        attack = PlayerPrefs.GetInt("attack");
        speed = PlayerPrefs.GetInt("speed");
        magic = PlayerPrefs.GetInt("magic");

        attackText.text = "Attack: " + attack.ToString();
        speedText.text = "Speed: " + speed.ToString();
        magicText.text = "Magic: " + magic.ToString();
    }
}
