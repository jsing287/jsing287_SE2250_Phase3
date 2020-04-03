using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{


    public int addScore;

    public override void Die()
    {
        //base.Die();

         // die animation.
         Animator animator = GetComponentInChildren<Animator>();

         animator.SetBool("death", true);
        


         StartCoroutine(DoDeath());
         PlayerManager.instance.Score(20);
       







    }



    IEnumerator DoDeath()
    {
        yield return new WaitForSeconds(4);
        Debug.Log(transform.name  + " has died");
        Destroy(gameObject);






    }
}

  
