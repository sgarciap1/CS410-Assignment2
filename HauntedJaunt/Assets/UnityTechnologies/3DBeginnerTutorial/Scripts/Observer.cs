using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public GameEnding gameEnding;

    public float dot;
    public float view_angle = 0.5f; // .5f is 60 degrees (if player is within that 60 degree angle of view from the enemy (while inside the hitbox they'll be seen)

    bool m_IsPlayerInRange;

    private void Update()
    {
        if(m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {
                    Vector3 playerDirection = (player.position - enemy.position).normalized;

                    Vector3 enemyForward = enemy.transform.forward;

                    float dotProduct = Vector3.Dot(playerDirection, enemyForward);
                    dot = dotProduct;
                    if (dotProduct > view_angle)
                    {
                        gameEnding.CaughtPlayer();
                    }
                }

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }
}