using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : Interactable
{
    public Item item;


    public override void interact()
    {
        //base.interact();

        pickUp();        
    }

    void pickUp()
    {        
        Debug.Log("Picked up " + item.name);
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
