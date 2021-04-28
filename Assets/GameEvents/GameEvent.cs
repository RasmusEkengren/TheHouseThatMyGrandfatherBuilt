using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Game Event", fileName = "New Game Event")]
public class GameEvent : ScriptableObject
{
    // HashSet is a list where all entries are unique
    HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

    // This will invoke the events on all GameEventListeners with this scriptable object
    public void Invoke()
    {
        foreach(GameEventListener globalEventListener in listeners)
        {
            globalEventListener.RaiseEvent();
        }
    }

    // Shortening of a regular function 
    public void Register(GameEventListener gameEventListener) => listeners.Add(gameEventListener);
    public void Deregister(GameEventListener gameEventListener) => listeners.Remove(gameEventListener);
}
