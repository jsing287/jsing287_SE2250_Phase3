using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Interactable
{
   
    public override void Interact()
    {
        base.Interact();

        PlayerManager.instance.levelCompleted = true;
    }
}
