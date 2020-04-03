using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    public LayerMask movementMask; // the ground, filters out everything thats not walkable.
    public PlayerMotor motor;
    public Interactable focus; // this is the players current focus, Item, enemy, etc.
    public Animator animator;
   
  

    // retrieving our references.
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        animator = GetComponentInChildren<Animator>();
       
        
    }

    public static GameObject acting { get; private set; }

    // Update is called once per frame
    void Update()
    {
        // this checks if the pointe is hovering of UI. if it is the movement clicks wont happen as the method will kick out.
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Execute if the left mouse is clicked.
        if(Input.GetMouseButtonDown(0))
        {
            // Shoot a ray out to hit an object.
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
          
            // if we hit an object.
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {

                motor.MoveToPoint(hit.point); // move the player to what we hit.
                RemoveFocus(); // stop focusing on previous object.
            }

           
        }

        // if the right mouse is clicked.
        if (Input.GetMouseButtonDown(1))
        {
            // shoot a ray out.
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if we hit something
            if (Physics.Raycast(ray, out hit, 100))
            {
                // Check if we hit an interactable, if we did do something.
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                acting = hit.collider.gameObject; 

                // if we hit an interactable focus on the object.
                if(interactable!= null)
                {
                    SetFocus(interactable);
                }

            }
        }

    }


    // Sets the focus to a new focus when the ray hits an interactable.
    void SetFocus(Interactable newFocus)
    {

        // if the newFocus is  a new focus
        if(newFocus!= focus)
        {
            // defocus the previous focus.
            if(focus != null)
                focus.OnDefocused();
            

            // set the new focus.
            focus = newFocus;
            // follow the new focus
            motor.FollowTarget(newFocus);
           
        }
        // Focus on the new focus.
        newFocus.OnFocused(transform);

    }


    // method removes a focus from interactable.
    void RemoveFocus()
    {
        // defocus first.
        if (focus != null)
            focus.OnDefocused();
        
        // stop folowing target and set refernce to null.
        focus = null;
        motor.StopFollowingTarget();

    }

   
    IEnumerator Die(Collider collide)
    {
        yield return new WaitForSeconds(1.0f);
        if (collide.gameObject.CompareTag("Gate"))
        {
            Destroy(this.gameObject);
        }


    }
        

    

  
}
