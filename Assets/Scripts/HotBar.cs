using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour

{
    public Sprite shittyfish;
    //public Inventory Inventory;
    // Start is called before the first frame update

    public AudioClip fishAddSFX;
    public AudioSource fishAddSource;

    private void Update()
    {
        if (FishingMiniGame.win == true)
        {
            Debug.Log("ITS GOING");
            InventoryScript_ItemAdded();
            fishAddSource.PlayOneShot(fishAddSFX);
            FishingMiniGame.win = false;
            Debug.Log(FishingMiniGame.win);
        }
    }


    private void InventoryScript_ItemAdded()
    {
        Transform inventoryPanel = transform.Find("Panel");
        foreach (Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            if (!image.enabled)
            {
                
                image.enabled = true;
                image.sprite = shittyfish;

                break;
            }
        }
    }
}
