using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MageController : MonoBehaviour
{

    public float lookRadius = 10f; // gizmos radius


    Transform target; // refernce to the player
    NavMeshAgent agent; // reference to the Nav Mesh
   
    public  GameObject spell;
    ShootingController shoot;
    private Animator animator;
    private float timer;
    public float waitTime;

    private Vector3 offset;

    private GameObject holder;

    public CharacterCombat combat;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        
        timer = 0.0f;

        offset = new Vector3(0.0f, 1.0f, 0.0f);

        animator = this.GetComponentInChildren<Animator>();


        combat = GetComponent<CharacterCombat>();
    









    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(target.position, transform.position); // get the distanace between the player and enemy

        // if we are within the enemies radius start chasing the player
        if (distance <= lookRadius && timer>=waitTime)
        {
            timer -= waitTime;
            animator.SetTrigger("attack");
            GameObject holder = Instantiate(spell, this.transform.position+offset, this.transform.rotation);
            shoot = holder.GetComponent<ShootingController>();
           
            
        
        }

        if(shoot!=null)
        {
            if(shoot.Hit==true)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }

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

    }
}
