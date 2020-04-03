
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    Item item; // current item in the slot.

    public Image icon;
    public Button removeButton;


    // adding an item to the slot.
    public void AddItem(Item newItem)
    {
        // sets the graphics of the slot to "turn on".
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

    }

    // clear the slot.
    public void ClearSlot()
    {
        // set the graphics of the slot to "turn off".
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }


    // when the red x button is clicked remove the item from the inventory
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item); // call the remove method in the inventory system.
    }

    // Use item method called when item is pressed.
    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
