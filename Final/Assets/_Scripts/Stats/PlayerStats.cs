using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    private int displayArmor;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged; // adding method to delegate
        displayArmor = 0;
        
    }


    // this is the method to change the modifiers 
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {

        // add the new items modifiers to the list
        if(newItem!= null)
        {
            
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);

        }
  
        // remove the old items modifiers from the list
        if(oldItem!= null)
        {
            Debug.Log("Removing " + oldItem.name);
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }

      
    }


    public override void TakeDamage(int damage)
    {
        displayArmor = armor.GetBaseValue();

        armor.Reduce = damage;
       
        damage -= armor.GetValue();
        

        damage = Mathf.Clamp(damage, 0, int.MaxValue); // this insures that the damage value is not negative given an armour value greater than it
        Debug.Log(damage);
        CurrentHealth -= damage;


        Debug.Log(transform.name + " takes " + damage + "  damage.");



        if (CurrentHealth <= 0)
        {
            Die();
        }

    }

    public void SetArmor()
    {
        displayArmor = armor.GetBaseValue();
    }

    public int GetArmor()
    {
        return displayArmor;
    }

    public override  void Die()
    {
        base.Die();

        // Kill the player

        PlayerManager.instance.KillPlayer();
    }

    
}

