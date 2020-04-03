using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;


    void Awake()
    {
        instance = this;
    }
    #endregion // Sing // Singleton to hold the one instance of Equipment manager




    Equipment[] currentEquipment; // array to hold all equipment.
    SkinnedMeshRenderer[] currentMeshes; // keeps trakc oif meshes spawned into scene
    public SkinnedMeshRenderer targetMesh;

    public delegate void OnEquipementChanged(Equipment newItem, Equipment oldItem);
    public OnEquipementChanged onEquipmentChanged;

    public Transform shieldHand;
    public Transform swordHand;
    public PlayerStats stats; // holds the player stats



    public Equipment[] defaultItems;

    Inventory inventory;

    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; // retrieving number of enums which translates to the length of the equipment array.
        currentEquipment = new Equipment[numSlots];

        inventory = Inventory.instance; // retrieve singleton reference..

        currentMeshes = new SkinnedMeshRenderer[numSlots];
        EquipDefaultItems();

        stats = GameObject.Find("Player").GetComponent<PlayerStats>();

    }


    public bool EquipSword{get; private set;} // property for other classes to know if the sword has been equipped or not.

    public bool EquipShield { get; private set; } // property for other classes to know if the shield has been equipped or not


    public void Equip(Equipment newItem)
    {
        if(newItem.name == "ImmortalSword")
        {
            PlayerManager.instance.EndGame();
            return;
        }

        int slotIndex = (int)newItem.equipSlot; // retrieves the index of the item from the enum.

        


        Equipment oldItem = Unequip(slotIndex);

        if (currentEquipment[slotIndex] != null) // if we already have a equipment of the same type swap it out.
        {
            oldItem = currentEquipment[slotIndex]; // retrieve old item
            inventory.Add(oldItem); // add the old item to the inventory.
        }

        // when equipping run all delegate methods passing the new and old item equipped.
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged(newItem, null);
        }
        SetEquipmentBlendShapes(newItem, 100); // blend the new item to the player

        currentEquipment[slotIndex] = newItem; // the new equipment is placed in the array at the exact same index as it is in the enum.

       SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh); // creating a new mesh based on the mesh of the new object.

        if(newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            EquipSword = true;
            Debug.Log("equipped sword true");
            Debug.Log(EquipSword);
            
          


            newMesh.transform.parent = targetMesh.transform; // player mesh telling new mesh how to deform.
            newMesh.bones = targetMesh.bones; //using bones to deform
           
            newMesh.rootBone = swordHand;

            
        }
        else if(newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
        {
            EquipShield = true;
            newMesh.rootBone = shieldHand;
            Debug.Log(targetMesh.rootBone);
            
          

        }
        else
        {
            newMesh.transform.parent = targetMesh.transform; // player mesh telling new mesh how to deform.
            newMesh.bones = targetMesh.bones; //using bones to deform
            newMesh.rootBone = targetMesh.rootBone;

        }

       
        currentMeshes[slotIndex] = newMesh;


        if (stats != null)
        {
           
            stats.SetArmor();
        }



    }

    // method to unquip a specific item
    public Equipment Unequip(int slotIndex)
    {
       
        // first check if the index is not null
        if (currentEquipment[slotIndex] != null)
        {

            // check if the mesh at this slot is not null
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject); // destroy the mesh
            }
            // retrieve the old item from the index.
            Equipment oldItem = currentEquipment[slotIndex];

            SetEquipmentBlendShapes(oldItem, 0); // blend the old item from the player
            inventory.Add(oldItem); // add it back to inventory.


            // updates equipsword boolean
            if (currentEquipment[slotIndex] != null && currentEquipment[slotIndex].equipSlot == EquipmentSlot.Weapon)
            {
                Debug.Log("equipped sword false");
                EquipSword = false;
            }

            // updates equipshield boolean
            if (currentEquipment[slotIndex] != null && currentEquipment[slotIndex].equipSlot == EquipmentSlot.Shield)
            {
                Debug.Log("equipped shield false");
                EquipShield = false;
            }


            currentEquipment[slotIndex] = null; // set equipment to null as we unequiped it.


            if (onEquipmentChanged != null)
            {
                onEquipmentChanged(null, oldItem);
            }


            if (stats != null)
            {
                stats.SetArmor();
            }
            return oldItem;

        }

        if (stats != null)
        {
            stats.SetArmor();
        }

        return null;




    }

    // Call the unquip method for all equipment items in the array.
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
          
            Unequip(i);
        }
        EquipDefaultItems();
    }

    void Update()
    {
        // checks every frame to see if the key U is pressedthen it unquips all.
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }


    // This method blends the body to the new equipment the player picksup
    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        // loops through each mesh on the player and changes the blendShape weight.
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }

    }

    // equips the default items
    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }
}


