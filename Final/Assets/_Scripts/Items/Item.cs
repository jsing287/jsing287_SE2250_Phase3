using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";

    public Sprite icon = null;

    public bool isDefaultItem = false;


    // Overridable method for other items to use.
    public virtual void Use()
    {
        Debug.Log("using " + name);
    }

    public void RemoveFromInventory()
    {
       Inventory.instance.Remove(this);
    }
}
