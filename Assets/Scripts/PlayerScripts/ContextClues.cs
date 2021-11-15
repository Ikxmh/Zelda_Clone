using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClues : MonoBehaviour
{
    [SerializeField] private GameObject contextClues;
    [SerializeField] private bool contextActive = false;
   public void ChangeContext()
    {
        contextActive = !contextActive;
        if (contextActive)
        {
            contextClues.SetActive(true);
        }
        else
        {
            contextClues.SetActive(false);
        }
    }
}
