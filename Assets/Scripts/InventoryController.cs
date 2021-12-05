using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public Inventory inventory;

    // Start is called before the first frame update
    private void Update()
    {
        if (FishingMiniGame.win == true)
        {
            IInventoryItem item = GetComponent<IInventoryItem>();
            if (item != null)
            {
                inventory.AddItem(item);
            }

        }


    }