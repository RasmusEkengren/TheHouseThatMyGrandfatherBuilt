using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] protected GameEvent _gameEvent;
    [SerializeField] protected UnityEvent _unityEvent;

    // Same as regular Awake, just on one line
    private void Awake() => _gameEvent.Register(this);

    private void OnDestroy() => _gameEvent.Deregister(this);

    public virtual void RaiseEvent() => _unityEvent.Invoke();
}
