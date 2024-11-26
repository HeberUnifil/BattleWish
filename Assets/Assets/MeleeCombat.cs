using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement)), RequireComponent(typeof(Stats))]
public class MeleeCombat : MonoBehaviour
{

    private Movement moveScript;
    private Stats stats;
    private Animator anim;

    [Header("Target")]
    public GameObject targetEnemy;

    [Header("Melee Attack VAriables")]
    public bool performMeleeAttack = true;
    private float attackInterval;
    private float nextAttackTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Movement>();
        anim = GetComponent<Animator>();
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        attackInterval = stats.attackSpeed / ((500 + stats.attackSpeed)  * 0.01f);

        targetEnemy = moveScript.targetEnemy;


        if (targetEnemy != null && performMeleeAttack && Time.time > nextAttackTime)
        {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= moveScript.stoppingDistance)
            {
                StartCoroutine(MeleeAttackInterval());
            }
        }
    }

    private IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;

        anim.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackInterval);    

        if(targetEnemy != null)
        {
            anim.SetBool("isAttack", false);
            performMeleeAttack = true;
        }
    }


    private void MeleeAttack()
    {
        stats.TakeDamage(targetEnemy, stats.damage);

        nextAttackTime = Time.time + attackInterval;
        performMeleeAttack = true;

        anim.SetBool("isAttacking", false);
    }
}
