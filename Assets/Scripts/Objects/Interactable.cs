using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public bool PlayerInRange; // determine if user is in the range or not.

    public SignalSender context;
    // Start is called before the first frame update
    public virtual void OnTriggerEnter2D(Collider2D other) // allowed to be overrided by sub classes 
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            PlayerInRange = true;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other) // same thing
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            PlayerInRange = false;   
        }
    }
}
