using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public int HPCurrent, currentHearts, partialHeart, maxHearts, attack, speed, magic, magicPool;
    public GameObject player;
    private PlayerHealth playerHealth;
    private PlayerAttack playerAttack;
    public Text attackText, speedText;
    public Image[] hearts;
    public Sprite fullHeart, emptyHeart, quarterHeart, halfHeart, threequarterHeart;
    public Image[] magicPts;
    public Sprite fullMagic, emptyMagic;

    void Start() {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerAttack = player.GetComponent<PlayerAttack>();
    }

    void Update()
    {
        maxHearts = PlayerPrefs.GetInt("HP");
        HPCurrent = playerHealth.getHealth();
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

            if (i < maxHearts) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }

        attack = PlayerPrefs.GetInt("attack");
        speed = PlayerPrefs.GetInt("speed");

        attackText.text = "Attack: " + attack.ToString();
        speedText.text = "Speed: " + speed.ToString();

        magic = PlayerPrefs.GetInt("magic");
        magicPool = playerAttack.getMagicPool();
        for (int j = 0; j < magicPts.Length; j++) {
            if (j < magicPool) magicPts[j].sprite = fullMagic;
            else magicPts[j].sprite = emptyMagic;

            if (j < magic) magicPts[j].enabled = true;
            else magicPts[j].enabled = false;
        }

    }
}
