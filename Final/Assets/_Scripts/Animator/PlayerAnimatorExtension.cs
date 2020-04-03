using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimatorExtension : CharacterAnimator
{
    protected override void OnAttack()
    {
        base.OnAttack();
        EnemyController enemy = PlayerController.acting.GetComponent<EnemyController>();
        TrollController troll = PlayerController.acting.GetComponent<TrollController>();
        if (enemy != null)
        {
         
            Animator animator = enemy.GetComponentInChildren<Animator>();
            animator.SetTrigger("damage");
        }
        else if (troll != null)
        {
            Animator animator = enemy.GetComponentInChildren<Animator>();
            animator.SetTrigger("damage");
        }
    }

}
