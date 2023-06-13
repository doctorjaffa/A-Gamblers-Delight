using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private LayerMask attackMask;
    [SerializeField]
    private Vector3 attackOffset;

    public void Attack()
    {
        Vector3 pos = transform.position;

        Collider2D collision = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (collision != null)
        {
            collision.GetComponent<PlayerGamble>().GambleCoins();
        }
    }
}
