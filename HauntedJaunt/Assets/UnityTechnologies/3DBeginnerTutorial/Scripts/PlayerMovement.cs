using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private float maxSpeed;
    private float minSpeed;
    private float interpolatedSpeed;
    private float alpha;
    private bool canSprint;

    public float alphaChanger = .05f;
    public float turnSpeed = 20f;
    public float sprintTime = 5.0f;
    /**/
    //public Transform playerTransform;
    //public Transform enemTranform;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        maxSpeed = 2.0f;
        minSpeed = 1.0f;
        alpha = 0.0f;
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

        checkSprint();
    }

    void checkSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            if (alpha < 1.0f)
            {
                alpha += alphaChanger;
            }
            else
            {
                canSprint = false;
            }
            interpolatedSpeed = (1.0f - alpha) * minSpeed + alpha * maxSpeed;
        }
        else
        {
            if (alpha > 0.0f)
            {
                alpha -= alphaChanger;
            }
            else
            {
                canSprint = true;
            }
            interpolatedSpeed = (1.0f - alpha) * minSpeed + alpha * maxSpeed;
        }
    }



    /*
    void dotProduct()
    {
        Vector3 playerForward = playerTransform.forward;
        Vector3 playerToEnemy = enemTransform.forward - playerTransform.position;

        float dotPro = Vector3.Dot(playerForward, playerToEnemy)

        if (dotPro >= 0) {
            print("Forward");
        }
        else{
            print("Behined ")
        }
    }

    */

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude * interpolatedSpeed);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}
