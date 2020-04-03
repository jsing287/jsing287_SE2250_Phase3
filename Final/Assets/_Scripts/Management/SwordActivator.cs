using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordActivator : MonoBehaviour
{
    public GameObject mage; // hold the mage game object
    
    
    // holds the amount of enemies to spawn;
    public int spawnMage;
    public int spawnWizard;


    // trigger bool to check whenthe player has reached the radius
    private bool trigger;
   


    // hold the sowrd and wizard enemy
    public GameObject sword;
    public GameObject wizard;

    private List<GameObject> holder;

    void Start()
    {

        holder = new List<GameObject>();
      
        trigger = false;

    }


    void Update()
    {

        // make the sword appear when the player has defeated all enemies
        if(checkDestroy()==true && trigger == true)
        {
            sword.SetActive(true);
        }

    }


    public bool checkDestroy()
    {
        foreach (GameObject witch in holder)
        {
            if (witch != null)
            {
                return false;
            }
        }

        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 original = this.transform.position;
        // if the collided game object is the player and the trigger is false run
        if(other.gameObject.name == "Player" && !trigger)
        {
            // destroy the sphere collider of the game object and set the trigger to true 
            Destroy(this.gameObject.GetComponent<SphereCollider>());
            trigger = true;
            Vector3 pos = this.transform.position;


            // spawn the mages, spread them out, add them to the enemies list. 
            for (int i = 0; i < spawnMage; i++)
            {
                if (i % 2 == 0)
                {
                    pos.x += 2.0f+i;
                    holder.Add(Instantiate(mage, pos, Quaternion.identity));
                    pos.x -= 2.0f + i;
                }
                else
                {
                    pos.x -= 2.0f-i;
                   holder.Add(Instantiate(mage, pos, Quaternion.identity));
                    pos.x += 2.0f + i;
                }
            }

            pos = original;
            pos.z -= 3.0f; // move thw wizard back



            // spawn the wizards and add them to the enemies list.
            for (int i = 0; i < spawnWizard; i++)
            {
                if (i % 2 == 0)
                {
                    pos.x += 2.0f + i;
                    holder.Add(Instantiate(wizard, pos, Quaternion.identity));
                    pos.x -= 2.0f + i;
                }
                else
                {
                    pos.x -= 2.0f - i;
                    holder.Add(Instantiate(wizard, pos, Quaternion.identity));
                    pos.x += 2.0f + i;
                }
            }
        }
    
    }
}
