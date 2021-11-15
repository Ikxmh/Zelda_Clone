using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [SerializeField] private Item contents;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private bool isOpen;
    [SerializeField] private SignalSender raiseItem;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text dialogText;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerInRange)
        {
            if (!isOpen)
            {
                // open the chest
                OpenChest();
            }

            else
            {
                // Chest is already open
                ChestAlreadyOpened();
            }
          
        }
    }

    private void OpenChest()
    {
        // dialog window on
        dialogBox.SetActive(true);
        // dialog text = context text
        dialogText.text = contents.itemDescription;
        // add context to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        // Raise the signal to the player to animate 
        raiseItem.Raise();
        // raise the context clue
        context.Raise();
        // set the chest to opened
        isOpen = true;
        anim.SetBool("opened", true);
    }

    private void ChestAlreadyOpened()
    {
            // Dialog off
            dialogBox.SetActive(false);

            // raise the signal to the player to stop animating
            raiseItem.Raise();   
    }
    public override void OnTriggerEnter2D(Collider2D other) // allowed to be overrided by sub classes 
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            PlayerInRange = true;
        }
    }
    public override void OnTriggerExit2D(Collider2D other) // same thing
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            PlayerInRange = false;
        }
    }

}
