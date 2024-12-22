using UnityEngine;

public class te_run : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float attackRange = 3f;
    private Testenemypatrolle patrolScript;

    Transform player;
    Rigidbody2D rb;
    testenemy testenemy;
    bool isAttacking = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        testenemy = animator.GetComponent<testenemy>();
        isAttacking = false;

        patrolScript = animator.GetComponent<Testenemypatrolle>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (testenemy.playerInZone)
        {
            patrolScript.enabled = false;
        }

        if (!testenemy.playerInZone)
        {
            return;
        }

        testenemy.LookAtPlayer();

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true; 
                rb.linearVelocity = Vector2.zero; 
                animator.SetTrigger("Attack"); 
            }
        }
        else
        {
            isAttacking = false;
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        } 
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        isAttacking = false;
    }
}