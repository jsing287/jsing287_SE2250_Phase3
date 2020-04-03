using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]// this allows the fields in this class to show up in the inspector
public class Stat 
{

    [SerializeField]
    private int baseValue;

    public int Reduce { get; set; }


    private List<int> modifiers = new List<int>(); // holds the modifiers

    public int Accumulated { get; set; }
    

    void Start()
    {
        Accumulated = 0;
    }
    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);

        
        // cehck if the accumulated reductions have met the armor values
        if(Accumulated>=finalValue)
        {
            return 0; // return a zero armor value if it has
        }
        else
        {
            // else reduce the armor by the accumulated amount and add the new damage.
            finalValue -= Accumulated;
            Accumulated += Reduce;
            return finalValue;
        }
     
    }

    public int GetBaseValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);

        finalValue -= Accumulated;

        if(finalValue<0)
        {
            Accumulated = 0;
            return 0;
        }
        return finalValue;

    }


    // adds the modifier to the list when a new item is equiped
    public void AddModifier(int modifier)
    {
        if(modifier != 0 )
        {
            modifiers.Add(modifier);
        }
    }

    // removes the modifier from the list when an item is unquipped
    public void RemoveModifier(int modifier)
    {
        Debug.Log("In remove modififer" + modifier);
        if(modifier!=0)
        {
            modifiers.Remove(modifier);
        }
    }
}
