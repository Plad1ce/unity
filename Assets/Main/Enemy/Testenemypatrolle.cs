using UnityEngine;

public class Testenemypatrolle : MonoBehaviour
{
    public GameObject pointA; 
    public GameObject pointB; 
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint; 
    public float speed = 2f; 
    public float switchDistance = 0.6f; 

    public bool isFlipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform; 
        anim.SetBool("IsPatrolling", true);
    }

    void FixedUpdate()
    {

        Vector2 direction = (currentPoint.position - transform.position).normalized;

        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

        float distanceToTarget = Vector2.Distance(transform.position, currentPoint.position);
        if (distanceToTarget < switchDistance)
        {
            SwitchTarget(); 
            Flip();         
        }

    }

    void SwitchTarget()
    {
        if (currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        else
        {
            currentPoint = pointB.transform;
        }

    }

    void Flip()
    {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1; // Только инвертируем ось X
        transform.localScale = flipped;

        isFlipped = !isFlipped; // Меняем состояние
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}