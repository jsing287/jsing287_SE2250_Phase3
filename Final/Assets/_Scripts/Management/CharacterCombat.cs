using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private CharacterStats myStats; // stats of current character;
  
   

    public float attackSpeed = 1f; // speed of attack
    private float attackCoolDown = 0f; // initial cool down amount
    const float combatCoolDown = 5f;



    private float lastAttackTime;

    public float attackDelay = 5f;

    public System.Action OnAttack;


    public bool InCombat { get; private set; }
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
       
       
    }


    void Update()
    {
        attackCoolDown -= Time.deltaTime; // everyframe cool down the characters attacking

        if(Time.time - lastAttackTime > combatCoolDown)
        {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCoolDown <= 0f)
        {

            StartCoroutine(DoDamage(targetStats, attackDelay)); // a delay for the attack to make time for the animation.

            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCoolDown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }

       
       
        
        
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());

        
        if(stats.CurrentHealth<=0)
        {
            InCombat = false;
        }
    }
}
