using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorActivator : MonoBehaviour
{

    // local fields
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

       // setting fields
        pickups = GameObject.Find("Armor New");
        Debug.Log(pickups.gameObject.name);
        holder = new List<GameObject>();
        hasInteract = false;
        reference = PlayerManager.instance;



    }

    public void Update()
    {


        Vector3 pos = this.transform.position; // get the position of the activator
        float distance = Vector3.Distance(player.position, interactionTransform.position); // get the distance to the player



        // if we havent interacted with the player and the player is within range spawn enemies
        if (!hasInteract && distance <= radius)
        {
                Spawn(pos);
            
         
        }



        // if we have interacted with the player adn all enenmies are destroyed level the player up.
        if (hasInteract && checkDestroy())
        {
            hasInteract = false;
            UpgradePlayer();
            reference.Score(100);
            pickups.SetActive(true);
         
            Destroy(this.gameObject);



        }






    }





    // method loops through holder list to check if thera are no more game objects
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


    // draws the proximity sphere
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }



    // spawning the enemies
    void Spawn(Vector3 pos)
    {
        // checks if we have not already interacted yet so we dont keep spawning items.

        pickups.SetActive(false); // make the armor dissappear
        Debug.Log("in spawn;");


        // spawns enemies
        for (int i = 0; i < spawn; i++)
        {
            
            if(direction ==true)
            {
                pos.z += 1.0f; // moving spawn distance
            }
            else
            {
                pos.x += 1.0f; // moving spawn distance
            }
              
            holder.Add(Instantiate(enemy, pos, this.transform.rotation)); // spawn


        }

        radius = 0.0f;
    





        hasInteract = true;




    }


    // calls teh playre manaer to upgrade the player
    void UpgradePlayer()
    {
        PlayerManager.instance.Upgrade();
    }


}
