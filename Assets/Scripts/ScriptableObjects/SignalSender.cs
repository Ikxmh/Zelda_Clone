using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalSender : ScriptableObject
{
    [SerializeField] private List<SignalListener> listeners = new List<SignalListener>();
    public void Raise()
    {
        for(int i = listeners.Count - 1; i >= 0; i--) // go through it backward in case of out of the range execption                                             
        { //when it removed itself 
            listeners[i].OnSignalRaised(); 
        }
    }
    public void RegisterListener(SignalListener listener) // a way to add stuff to the list 
    {
        listeners.Add(listener);
    } 
    public void DeRegisterListener(SignalListener listener) // a way to remove stuff from the list
    {
        listeners.Remove(listener);
    }
}
