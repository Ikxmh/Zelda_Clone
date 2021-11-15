using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    public GameObject dialogBox; // refer to the text gameObject
    public Text dialogText; // refer to the text
    public string dialog; // text in the box
 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && PlayerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            dialogBox.SetActive(false);

            base.OnTriggerExit2D(other);
        }
    }
}
