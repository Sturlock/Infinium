using Infinium.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Health target;
    [SerializeField] float damage = 40f;

    void Start()
    {
        target = FindObjectOfType<Health>();
    }

    public void AttackHitEvent()
    {
        if (target == null) return;
        target.TakeDamage(damage);
    }
}
