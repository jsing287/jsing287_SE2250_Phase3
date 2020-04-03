using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorController : MonoBehaviour
{
    public GameObject enemy;
    public float radius;
    public bool hasInteract;
    public Transform player;
    public Transform interactionTransform;
    public GameObject pickups;
    public bool direction;
    public PlayerManager reference;
 
    public int spawn;


    private int getCount;
  
    private List<GameObject> holder;
    void Start()
    {
        
        pickups = GameObject.Find("Armor Pickup");
        Debug.Log(pickups.gameObject.name);
        holder = new List<GameObject>();
        hasInteract = false;
        reference = PlayerManager.instance;

        
   
    }

   

    public void Update()
    {
       
        Vector3 pos = this.transform.position; // get the position of the activator
        float distance = Vector3.Distance(player.position, interactionTransform.position);


        if (!hasInteract && distance <= radius)
        {
            Spawn(pos);
        }


                
        if (hasInteract&& checkDestroy())
        {
            hasInteract = false;
            UpgradePlayer();
            reference.Score(100);
            pickups.SetActive(true);
            reference.Defeats();
            Destroy(this.gameObject);

        
       
          
        }
        
           
            
        


    }

  
   

    public bool checkDestroy()
    {
        foreach(GameObject witch in holder)
        {
            if(witch!=null)
            {
                return false;
            }
        }

        return true;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }


    void Spawn(Vector3 pos)
    {
        // checks if we have not already interacted yet so we dont keep spawning items.
        
            pickups.SetActive(false);
        Debug.Log("in spawn;");


        // spawns ten enemies
        for (int i = 0; i < spawn; i++)
        {
            if (i % 2 == 0) // checks if we are at an even number so we move position to the right
            {
                if (direction)
                {
                    pos.z += (3.0f + i);
                }
                else
                {
                    pos.x += (1.0f + i);
                }

            }
            else
            {
                if (direction)
                {
                    pos.z -= (3.0f + i);
                }
                else
                {
                    pos.x -= (1.0f + i);
                }

            }
            holder.Add(Instantiate(enemy, pos, this.transform.rotation)); // spawn


        }



      

        hasInteract = true;

        


    }

    void UpgradePlayer()
    {
        PlayerManager.instance.Upgrade();
    }


}
