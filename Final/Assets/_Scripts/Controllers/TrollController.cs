using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrollController : MonoBehaviour
{


    public float lookRadius = 10f; // gizmos radius

    public float attackRadius;


    Transform target; // refernce to the player
    NavMeshAgent agent; // reference to the Nav Mesh
    CharacterCombat combat; // refernce to the combat of the player
    public static Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        combat = GetComponent<CharacterCombat>();
        animator = GetComponentInChildren<Animator>();





    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position); // get the distanace between the player and enemy

        // if we are within the enemies radius start chasing the player
        if (distance <= lookRadius)
        {

            agent.SetDestination(target.position);
            if (distance <= attackRadius)
            {
               
                // Attack the target
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }

                FaceTarget();



            }
        }

        FaceTarget();

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    // draws a wire sphere around our enemy allows for detection of player movement for further functionality
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }
















}
