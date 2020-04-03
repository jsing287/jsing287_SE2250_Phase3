using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ShootingController : MonoBehaviour
{

    public float lookRadius = 10f; // gizmos radius


    Transform target; // refernce to the player
    private NavMeshAgent agent; // reference to the Nav Mesh
    CharacterCombat combat; // refernce to the combat of the player
    public static Animator animator;
    private bool hasShot;
 
   

    public bool Hit { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("in start of shooting controller");
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        combat = GetComponent<CharacterCombat>();
        animator = GetComponentInChildren<Animator>();
        hasShot = false;
        Hit = false;





    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position); // get the distanace between the player and enemy

        // if we are within the enemies radius start chasing the player
        if (distance <= lookRadius)
        {
            if(!hasShot)
            {
                Shoot(target.position);
            }
          
           
            
        }

        if(hasShot)
        {
            StartCoroutine(Reset(0.5f));
        }


        FaceTarget();

    }

    public void Shoot(Vector3 pos)
    {
        agent.SetDestination(pos);
        hasShot = true;
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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("have collided");
        Hit = true;
        StartCoroutine(Reset(0.1f));
    }

   

  

    IEnumerator Reset(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
