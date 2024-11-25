using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MInionAI : MonoBehaviour
{

    private NavMeshAgent agent;
    private Transform currentTarget;
    public string enemyMinionTag = "Enemy";
    public string turretTag = "Terret";
    public float stopDistance = 2.0f;
    public float aggroRange = 5.0f;
    public float targetSwitchInterval = 2.0f;

    private float timeSinceLastTargetSwitch = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindAndSetTarget();
    }

    // Update is called once per frame
    private void Update()
    {
        timeSinceLastTargetSwitch += Time.deltaTime;

        if (timeSinceLastTargetSwitch >= targetSwitchInterval)
        {
            CheckAndSwitchTarget();
            timeSinceLastTargetSwitch = 0.0f;
        }

        if (currentTarget != null)
        {
            Vector3 directionToTarget = currentTarget.position - transform.position;
            Vector3 stoppingPosition = currentTarget.position - directionToTarget.normalized * stopDistance;
            agent.SetDestination(stoppingPosition);
        }
    }

    private void CheckAndSwitchTarget()
    {
        GameObject[] enemyMinions = GameObject.FindGameObjectsWithTag(enemyMinionTag);
        Transform closestEnemyMinions = GetClosestObjectInRadius(enemyMinions, aggroRange);

        if (closestEnemyMinions != null)
        {
            currentTarget = closestEnemyMinions;
        }
        else
        {
            GameObject[] turrents = GameObject.FindGameObjectsWithTag(turretTag);
            currentTarget = GetClosestObject(turrents);

        }
    }

    private Transform GetClosestObject(GameObject[] turrents)
    {
        throw new NotImplementedException();
    }

    private Transform GetClosestObject(GameObject[] objects, float radius)
    {
        float closestDistance = Mathf.Infinity;
        Transform closestObject = null;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(currentPosition, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj.transform;
            }
        }
        return closestObject;
    }

    private Transform GetClosestObjectInRadius(GameObject[] objects, float radius)
    {
        float closestDistance = Mathf.Infinity;
        Transform closestObject = null;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(currentPosition, obj.transform.position);
            if (distance < closestDistance && distance <= radius)
            { 
                closestDistance = distance;
                closestObject = obj.transform;
            }

        }

        return closestObject;
    }
    private void FindAndSetTarget()
    {
        GameObject[] enemyMinion = GameObject.FindGameObjectsWithTag(enemyMinionTag);
        Transform closestEnemyMinion = GetClosestObjectInRadius(enemyMinion, aggroRange);

        if (closestEnemyMinion != null)
        {
            currentTarget = closestEnemyMinion;
        }
        else 
        {
            GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);
            currentTarget = GetClosestObject(turrets);
        }

    }
}
