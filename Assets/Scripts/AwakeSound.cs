using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeSound : MonoBehaviour
{
    [FMODUnity.EventRef] [SerializeField] private string awakeSound = null;
    private void Awake()
    {
        FMODUnity.RuntimeManager.PlayOneShot(awakeSound);
    }
}
