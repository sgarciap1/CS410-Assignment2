using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    //public float position;
    public float Dot;
    public float ang;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;
    //public float m_DistanceToFinish;

    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught) 
        {
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }

    }

    private void FixedUpdate()
    {
        // Direction to end from player
        Vector3 dirToTarget = (gameObject.transform.position - player.transform.position);
        dirToTarget.Normalize();
        Dot = Vector3.Dot(player.transform.forward, dirToTarget);

        // OTHER
        //Dot = dirToTarget.magnitude;
        // 
        //Vector3 v = transform.forward;
        //v.Normalize();
        //Dot = Vector3.Dot(v, dirToTarget);
        //Dot = Mathf.Acos(Vector3.Dot(player.transform.forward.Normalize(), dirToTarget));
        //ang = Mathf.Acos(Dot / (dirToTarget.magnitude * player.transform.forward.magnitude) ) * Mathf.Rad2Deg;
        //
        //ang = Mathf.Acos(Dot);
        //Dot = Vector3.Dot(gameObject.transform.position, player.transform.position);
        //ang = Mathf.Acos(Dot / (gameObject.transform.position.magnitude * player.transform.position.magnitude) ) * Mathf.Rad2Deg; 
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }


        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
