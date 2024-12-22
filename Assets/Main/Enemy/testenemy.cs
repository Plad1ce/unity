using System.Collections.Generic;
using UnityEngine;

public class testenemy : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public bool playerInZone = false;
    private Animator animator;

    private bool playerWasInZone = false;
    public Testenemypatrolle patrolScript;

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.CompareTag("Player") && !playerWasInZone)
        {
            playerInZone = true;
            playerWasInZone = true;
            animator.SetBool("PlayerInZone", playerInZone);
        }
    }

    void Awake() {
        animator = GetComponent<Animator>(); 
    }

    public testenemyweapon enemyWeapon; 

    public void LookAtPlayer()
    {
        if (!playerInZone) return;

        Vector3 flipped = transform.localScale;

        if (transform.position.x > player.position.x && !isFlipped)
        {
            flipped.x = -Mathf.Abs(flipped.x);
            transform.localScale = flipped;
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            flipped.x = Mathf.Abs(flipped.x);
            transform.localScale = flipped;
            isFlipped = false;
        }

        if (enemyWeapon != null)
        {
            enemyWeapon.isFlipped = isFlipped;
        }
    }


}