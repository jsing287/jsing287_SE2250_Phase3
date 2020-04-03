using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{

    PlayerManager playerManager; // refernce to the playr manager to obtain attributes.
    EnemyStats myStats;

    void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<EnemyStats>();
    }
        

 
    public override void Interact()
    {
        base.Interact();

        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>(); // retrieve our players combat attributes.

        if(playerManager!= null)
        {
            playerCombat.Attack(myStats);
            playerManager.Score(myStats.addScore);
        }
    }
}
