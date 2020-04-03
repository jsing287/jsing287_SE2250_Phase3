using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WiseElderTwoController : Interactable
{

    private TextMeshPro textPro;

    public override void Interact()
    {
        base.Interact();

        textPro = GetComponent<TextMeshPro>();
        textPro.text = "CAN YOU UNLOCK \n THE INFINITY BLADE";


    }
}
