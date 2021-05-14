using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Game Event", fileName = "New Game Event")]
public class GameEvent : ScriptableObject
{
    // HashSet is a list where all entries are unique
    //HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();

    //// This will invoke the events on all GameEventListeners with this scriptable object
    //public void Invoke()
    //{
    //    foreach(GameEventListener globalEventListener in _listeners)
    //    {
    //        globalEventListener.RaiseEvent();
    //    }
    //}

    //// Shortening of a regular function 
    //public void Register(GameEventListener gameEventListener) => _listeners.Add(gameEventListener);
    //public void Deregister(GameEventListener gameEventListener) => _listeners.Remove(gameEventListener);
}
