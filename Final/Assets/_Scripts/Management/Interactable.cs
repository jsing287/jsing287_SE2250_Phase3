using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool isFocus = false; // is this specific interactable currently being focus on

    bool hasInteracted = false; // have we already interacted with the object

    Transform player; // refernce to the player object.

    public Transform interactionTransform;


    // this method is meant to be overriden for various objects that the player needs to interact with.
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + interactionTransform.name);

    }

    // draws the wire frame to see the interactable area around the object.
    void OnDrawGizmosSelected()
    {
        if(interactionTransform ==null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    void Update()
    {
        if (isFocus) 
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius && !hasInteracted)
            {
                hasInteracted = true;
                Interact();
                
            }
        }
    }

    // Sets the appropriate fields when the object needs to be focus on.
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;

    }

    // Sets appropriate field when the object is no longer focused on.
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }


}
