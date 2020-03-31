using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{   public GameObject player;

    public int HPCurrent, currentHearts, partialHeart, maxHearts;
    private PlayerHealth playerHealth;
    public GameObject[] hearts;

    public int attack, speed;
    public Text attackText, speedText;
    
    public int magic, magicPool;
    private PlayerAttack playerAttack;
    public GameObject[] magicPts;
    
    public int statusBuildup, fireResist, waterResist;
    public Slider slider;
    

    void Start() {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerAttack = player.GetComponent<PlayerAttack>();
        fireResist = playerHealth.getFireResist();
        waterResist = playerHealth.getWaterResist();
        slider.minValue = waterResist;
        slider.maxValue = fireResist;
        slider.value = 0;

        maxHearts = PlayerPrefs.GetInt("HP");
        magic = PlayerPrefs.GetInt("magic");

        // Remove unused hearts
        for (int i = maxHearts; i < hearts.Length; i++)
            Destroy(hearts[i]);

        // Remove unused magic
        for (int i = magic; i < magicPts.Length; i++)
            Destroy(magicPts[i]);
    }

    void Update()
    {
        HPCurrent = playerHealth.getHealth();
        currentHearts = HPCurrent / 4;
        partialHeart = HPCurrent % 4;

        // Update health sprites
        for (int i = 0; i < maxHearts; i++)
        {
            Animator anim = hearts[i].GetComponent<Animator>();

            if (i < currentHearts)
            {
                anim.SetInteger("Health", 4);
            }
            else
            {
                anim.SetInteger("Health", partialHeart);
                partialHeart = 0;
            }
        }

        attack = PlayerPrefs.GetInt("attack");
        speed = PlayerPrefs.GetInt("speed");

        attackText.text = "Attack: " + attack.ToString();
        speedText.text = "Speed: " + speed.ToString();

        magicPool = playerAttack.getMagicPool();

        // Update magic sprites
        for (int i = 0; i < magicPts.Length; i++)
        {
            Animator anim = magicPts[i].GetComponent<Animator>();

            if (i >= magicPool)
                anim.SetBool("Full", false);
        }

        statusBuildup = playerHealth.getStatusBuildup();
        slider.value = statusBuildup;
    }
}
