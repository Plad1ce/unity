using UnityEngine;

public class testenemyweapon : MonoBehaviour
{
    public int attackDamage = 20;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public bool isFlipped; // Связь с состоянием врага

    public void Attack()
    {
        Vector3 pos = transform.position;

        if (isFlipped)
        {
            pos -= transform.right * attackOffset.x; 
        }
        else
        {
            pos += transform.right * attackOffset.x;
        }
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;

        // Учитываем направление врага
        if (isFlipped)
        {
            pos -= transform.right * attackOffset.x; // Отзеркаливаем смещение по X
        }
        else
        {
            pos += transform.right * attackOffset.x;
        }
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
