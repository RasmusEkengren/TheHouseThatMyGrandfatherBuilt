using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneData : MonoBehaviour
{
    //[Serializable]
    //public class DataContainer
    //{
    //	public string name = null;
    //	public GeorgeState.State georgeState = GeorgeState.State.Porch;
    //	public GameObject obj;
    //}

    public static Vector3 lastLeahPosition;
    public static Quaternion lastLeahRotation;
    public SceneType sceneType;
    //public List<DataContainer> containerList = new List<DataContainer>();

    public static bool mg_porchFixed /*{ get { return mg_porchFixed; } private set { mg_porchFixed = value; } }*/;
    public static bool mg_windowsFixed;
    public static bool tutorialDone;
    public static bool introDone = false;

    public enum PorchFixed { None, Flat, Slanted }
    public static PorchFixed porchFixed
    {
        get
        {
            return porchFixed;
        }

        set
        {
            if (value != PorchFixed.None)
            {
                mg_porchFixed = true;
            }
            else { mg_porchFixed = false; }
            porchFixed = value;
        }
    }

    private GameObject player;

    void Update()
    {
        if (mg_windowsFixed) Debug.Log("Yay once again!");
    }

    public static void SaveLeahPosition(PlayerMovement player)
    {
        lastLeahPosition = player.transform.position;
        lastLeahRotation = player.transform.rotation;
    }


    /// Health component som man kan binda events till
    /// T�nk inte p� det f�r mycket
    /// Avbinda events
    /// ?.
}
