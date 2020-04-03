using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")] // allows for creation of new equipment objects in the assets menu
public class Equipment : Item
{

    public EquipmentSlot equipSlot; // reference to equipment objects.

    public int armorModifier; // armor health

    public int damageModifier; // damage amount

    public SkinnedMeshRenderer mesh;

    public EquipmentMeshRegion[] coveredMeshRegions;

    public override void Use()
    {
        base.Use();

        // Equip the item.
        EquipmentManager.instance.Equip(this);
        //Remove it from inventory
        RemoveFromInventory();

    }
  

    
}


public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet } // this will allow access from the inspector to set different types of quipment slots.
public enum EquipmentMeshRegion { Legs, Arms, Torso } // corresponds to body blendshapes