using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHazardCollider : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup killedImageCanvasGroup;

    private bool m_IsPlayerInKillzone;
    private float m_Timer;

    protected virtual void Start()
    {
        m_IsPlayerInKillzone = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Player detected in killzone...");
            m_IsPlayerInKillzone = true;
        }
    }

    protected virtual void Update()
    {
        if (m_IsPlayerInKillzone)
        {
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        Destroy(player);

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
