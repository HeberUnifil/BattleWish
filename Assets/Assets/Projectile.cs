using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 10f;
    private Transform target;

    private Stats towerStats;
    // Start is called before the first frame update
    public void Start()
    {
        towerStats = GameObject.FindGameObjectWithTag("Turret").GetComponent<Stats>();
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    private void Update()
    {
        if (target == null)
        { 
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
     }

    private void HitTarget()
    {
        Stats targetStats = target.gameObject.GetComponent<Stats>();
        targetStats?.TakeDamage(target.gameObject, towerStats.damage);  

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject == target.gameObject)
        {
            HitTarget();
            Destroy(gameObject);
        }
    }
}
