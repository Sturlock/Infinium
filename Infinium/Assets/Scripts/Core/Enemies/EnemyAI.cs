using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Infinium.Core.Enemy
{
	public class EnemyAI : MonoBehaviour
	{
	    public NavMeshAgent agent;
	    public GameObject player;
	    Vector3 lastSeenPos;
	
	    public float sightRadius;
	    public float sightAngle;
	
	    public List<Collider> overlaps;
	
	    bool hasSeenPlayer = false;
	    bool isPatrolling;
	
	    public float patrolRadius = 3f;
	
	    void Awake()
	    {
	        agent = GetComponent<NavMeshAgent>();
	        player = GameObject.FindWithTag("Player");
	
	        overlaps.Add(player.GetComponent<Collider>());
	    }
	
	    void Update()
	    {
	        hasSeenPlayer = SeenPlayer(transform, player.transform, sightAngle, sightRadius);
	
	        if(hasSeenPlayer)
	        {
	            StopCoroutine(Patrol());
	            agent.SetDestination(player.transform.position);
	        }
	        else { StartCoroutine(Patrol()); }
	    }
	
	    public bool SeenPlayer(Transform checkingObj, Transform target, float maxAngle, float maxRadius)
	    {
	        for(int i = 0; i < overlaps.Count; i++)
	        {
	            if(overlaps[i].transform == target)
	            {
	                Vector3 directionBetween = (target.position - checkingObj.position).normalized;
	                directionBetween.y *= 0;
	
	                float angle = Vector3.Angle(checkingObj.forward, directionBetween);
	
	                if(angle <= maxAngle)
	                {
	                    Ray ray = new Ray(checkingObj.position, target.position - checkingObj.position);
	                    RaycastHit hit;
	
	                    if(Physics.Raycast(ray, out hit, maxRadius))
	                    {
	                        if(hit.transform == target)
	                        {
	                            return true;
	                        }
	                    }
	                }
	            }
	        }
	        return false;
	    }
	
	    public IEnumerator StunAI(float stunTime)
	    {
	        agent.isStopped = true;
	
	        Debug.Log("i should be stoping");
	        yield return new WaitForSeconds(stunTime);
	        agent.isStopped = false;
	
	        Debug.Log("i should be going");
	    }
	
	    private void OnDrawGizmos()
	    {
	        Gizmos.color = Color.yellow;
	        Gizmos.DrawWireSphere(transform.position, sightRadius);
	        //FOV Line creation
	        Vector3 fovLine1 = Quaternion.AngleAxis(sightAngle, transform.up) * transform.forward * sightRadius;
	        Vector3 fovLine2 = Quaternion.AngleAxis(-sightAngle, transform.up) * transform.forward * sightRadius;
	
	        Gizmos.color = Color.blue;
	        Gizmos.DrawRay(transform.position, fovLine1);
	        Gizmos.DrawRay(transform.position, fovLine2);
	        //Player tracking visualized
	        if(!hasSeenPlayer)
	            Gizmos.color = Color.red;
	        else
	            Gizmos.color = Color.green;
	        Gizmos.DrawRay(transform.position, (player.transform.position-transform.position).normalized * sightRadius);
	        //Forward facing
	        Gizmos.color = Color.black;
	        Gizmos.DrawRay(transform.position, transform.forward * sightRadius);
	
	        //Patrol Radius
	        Gizmos.color = Color.magenta;
	        Gizmos.DrawWireSphere(transform.position, patrolRadius);
	    }
	
	    IEnumerator Patrol()
	    {
	        if (isPatrolling || hasSeenPlayer)
	            yield break;
	
	        while(true)
	        {
	            isPatrolling = true;
	
	            yield return new WaitForSeconds(3f);
	
	            Vector3 pos = transform.position + new Vector3(Random.Range(-patrolRadius, patrolRadius), Random.Range(-patrolRadius, patrolRadius), Random.Range(-patrolRadius, patrolRadius));
	
	            agent.SetDestination(pos);
	
	            bool pathDone = (agent.pathStatus == NavMeshPathStatus.PathComplete);
	
	            yield return new WaitUntil(()=> pathDone);
	
	            isPatrolling = false;
	        }
	    }
	}
}
