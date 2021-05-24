using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneData : MonoBehaviour
{
    public static Vector3 lastLeahPosition;
    public static Quaternion lastLeahRotation;
    public static Vector3 lastCameraPosition;
    public static Quaternion lastCameraRotation;

    public enum LeahState { Entering, Building, Done }
    public static LeahState leahState = LeahState.Entering;

    public enum GeorgeState { Porch, Windows, Railing }
    public static GeorgeState georgeState;

    public enum PorchStyle { None, Flat, Slanted }
    public static PorchStyle porchStyle = PorchStyle.None;
    public enum PorchFixingState { Broken, Fixing, Fixed }
    public static PorchFixingState porchFixingState = PorchFixingState.Broken;

    public enum WindowsStyle { None, Ribbed, Solid }
    public static WindowsStyle windowsStyle = WindowsStyle.None;
    public enum WindowsFixingState { Broken, Fixing, Fixed }
    public static WindowsFixingState windowsFixingState = WindowsFixingState.Broken;

    public enum RailingStyle { None, FlatTop, Pillars }
    public static RailingStyle railingStyle = RailingStyle.None;
    public enum RailingFixingState { Broken, Fixing, Fixed }
    public static RailingFixingState railingFixingState = RailingFixingState.Broken;
    public static List<string> interactedObjectIDs = new List<string>();

    private PlayerMovement player;

    public static bool tutorialFinished = false;

    private bool loadPositions = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "George" || SceneManager.GetActiveScene().name == "Inside") { tutorialFinished = true; }
        SceneManager.sceneLoaded += OnSceneLoaded;
        // Ugly solution for the Tester Helper tool
        lastLeahPosition = new Vector3(18f, 1f, -38f);
        lastCameraPosition = new Vector3(20f, 2f, -36f);
    }

    // When message from SceneController has been recieved
    public void OnChangingScene(string nextScene)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // If Leah to George or Leah to Leah, save positions
        if (currentScene == "Leah" && nextScene == "George" || currentScene == "Leah" && nextScene == "Leah")
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            CameraController camera = FindObjectOfType<CameraController>();

            SaveLeahPosition(player);
            SaveLeahCameraPosition(camera);
        }

        // If George to Leah or Leah to Leah, load positions
        if (currentScene == "George" && nextScene == "Leah" || currentScene == "Leah" && nextScene == "Leah")
        {
            loadPositions = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (loadPositions)
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            CameraController camera = FindObjectOfType<CameraController>();

            player.gameObject.transform.position = lastLeahPosition;
            player.gameObject.transform.rotation = lastLeahRotation;

            camera.gameObject.transform.position = lastCameraPosition;
            camera.gameObject.transform.rotation = lastCameraRotation;
        }
    }

    public static void SaveLeahPosition(PlayerMovement player)
    {
        lastLeahPosition = player.transform.position;
        lastLeahRotation = player.transform.rotation;

    }
    public static void SaveLeahCameraPosition(CameraController camera)
    {
        lastCameraPosition = camera.transform.position;
        lastCameraRotation = camera.transform.rotation;
    }
    public static bool FindInteractedState(string _id)
    {
        foreach (string id in interactedObjectIDs)
        {
            if (id == _id) return true;
        }
        return false;
    }

    public void ResetEverything()
    {
        Debug.Log("RESET EVERYTHING!!!!!!!");
        leahState = LeahState.Entering;
        georgeState = GeorgeState.Porch;
        porchFixingState = PorchFixingState.Broken;
        porchStyle = PorchStyle.None;
        windowsFixingState = WindowsFixingState.Broken;
        windowsStyle = WindowsStyle.None;
        railingFixingState = RailingFixingState.Broken;
        railingStyle = RailingStyle.None;
    }
}
