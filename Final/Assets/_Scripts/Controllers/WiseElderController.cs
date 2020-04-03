using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WiseElderController : Interactable
{
   
    private TextMeshPro textPro;
    
    public override void Interact()
    {
        base.Interact();

        textPro = GetComponent<TextMeshPro>();
        textPro.text = "GO TO THE HOUSE \n AND MEET YOUR DESTINY";


    }
}
