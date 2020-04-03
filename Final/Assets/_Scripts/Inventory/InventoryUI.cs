
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory; // instance of an inventory classs;

    public Transform itemsParent; //refernce to the itemsParent to reach the children components.

    InventorySlot[] slots; // array to hold every slot in the inventory UI.

    public GameObject inventoryUI; // refernce to the UI


    // Start is called before the first frame update
    void Start()
    {

        inventory = Inventory.instance; // pointing the signleton to a local reference.
        inventory.onItemChangedCallBack += UpdateUI; // adding to the delegate
        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); // getting all of the inventory component and holding within a array.
    }

    // Update is called once per frame
    void Update()
    {
        // if the inventory button is clicked show the panel
        if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf); // always going to be the reverse state. if its open close it. if its closed open it.
        }
    }



    // Updates the UI 
    void UpdateUI()
    {
       // Debug.Log("UPDATING UI");

        // Loops through the slots array to add items to it.
        for(int i = 0; i< slots.Length; i++)
        {
            // if there are more items to add
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]); // add the item to the inventory. 
            }
            else
            {
                slots[i].ClearSlot(); // else clear the slots.
            }
        }
    }
}
