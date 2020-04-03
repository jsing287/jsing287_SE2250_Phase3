using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{

    public AnimationClip replaceableAttackAnim;
    const float locomotionAnimatorSmoothTime = .1f; // transistion time between animations.
    public Animator animator; // animator reference.
    public NavMeshAgent agent; // nav mesh reference.

    protected AnimatorOverrideController overrideController;

    protected CharacterCombat combat;

    protected AnimationClip[] currentAttackAnimSet;

    public AnimationClip[] defaultAttackAnimSet;

    public PlayerController playerCon;


    void Awake()
    {
        playerCon = GetComponent<PlayerController>();
    }

    
    // get references.
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack = OnAttack;

      

      
    }

    // Update is called once per frame
    void Update()
    {
       float speedPercent = agent.velocity.magnitude / agent.speed; ;

        animator.SetFloat("speedPercent", speedPercent, locomotionAnimatorSmoothTime, Time.deltaTime);
        animator.SetBool("inCombat", combat.InCombat);

    }


    protected virtual void OnAttack()
    {
        Debug.Log(combat.InCombat);
        Debug.Log(EquipmentManager.instance.EquipSword);
        animator.SetBool("attackSword", EquipmentManager.instance.EquipSword);
        animator.SetTrigger("attack");
       
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
    
    }


 
}
