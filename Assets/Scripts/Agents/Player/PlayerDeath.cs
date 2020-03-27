using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public CanvasGroup killedImageCanvasGroup;
    private float m_Timer = 0f;//timer for canvas appearance
    public GameObject player;

    private bool isPlayerDead = false;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;


    void Update()
    {
        if (isPlayerDead)
        {
            ResetLevel();
        }
    }

    public void KillPlayer()
    {
        isPlayerDead = true;
        Destroy(player);
    }

    void ResetLevel()
    {
        Debug.Log("in ResetLevel...");
        m_Timer += Time.deltaTime;

        killedImageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            Debug.Log("Resetting Scene...");
            SceneManager.LoadScene(0);
        }
    }
}
