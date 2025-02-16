﻿/*
 *  Enemy bullet script. It ignores fellow enemies and is distroyed when it hits a solid wall or the player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBulletScript : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    private MCMovement playerscript;

    private void Start()
    {
        playerscript = FindObjectOfType<MCMovement>();
    }

    private void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("Solid"))
            {
                Destroy(gameObject);
                break;
            }

            if (other.CompareTag("PlayerBody"))
            {
                playerscript.DamagePlayer(1);
                Destroy(gameObject);
                break;
            }

        }
        transform.position = newPosition;
    }
}
