using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
        }
        instance = this;
    }

    #endregion // singleton to insure there is only one inventory instance.

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int space = 20; // how many items we can pick up.

    public List<Item> items = new List<Item>(); // alist to hold the items.


    
    // Add an item.
    public bool Add(Item item)
    {
        // make sure its not a default item
        if(!item.isDefaultItem)
        {
            if(items.Count >= space) // check for space
            {
                Debug.Log("Not enough room!");
                return false;


            }

            items.Add(item); // if enough space add the item.

            // invoke a call back if its not null to the delegate.
            if(onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
         
          
        }

        return true;

    }


    // remove an item.
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
}
