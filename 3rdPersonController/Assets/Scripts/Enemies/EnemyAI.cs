using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;


    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;
    Animator enemyAnimator;

    private void OnDrawGizmosSelected()
    {
        //Display the Chase Radius of the enemy
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        enemyAnimator = GetComponent<Animator>();
        if(enemyAnimator != null)
        {            
            //enemyAnimator.applyRootMotion = false;
        }

    }


    void Update()
    {
        if (health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
            GetComponent<AudioSource>().enabled = false;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
           
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            
        }
        
    }


    public void OnDamageTaken()
    {
        isProvoked = true;
    }


    private void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetFloat("speedPercent", .8f , .1f, Time.deltaTime);
        //GetComponent<Animator>().SetTrigger("Move");
        
        //if(navMeshAgent.enabled == true)
        if(target.position != navMeshAgent.destination)
        {
            navMeshAgent.SetDestination(target.position);
        }        
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetFloat("speedPercent", 0, .1f, Time.deltaTime); ;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        //transform.rotation = our rotation, target rotation, speed

    }
}

