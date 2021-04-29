using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.Events;
using FMODUnity;


public class ObjectSceneLevel : MonoBehaviour
{
    [Tooltip("At what level this object should be active, starting from 0")]
    [Range(0, 5)]
    [SerializeField] private int sceneLevel = 0;
    public SceneType.Scene sceneType;
    [Tooltip("Move the player object to this location on this level")]
    [SerializeField] private bool movePlayer = false;
    private GameObject player = null;

    private void Start()
    {
        UpdateState();
    }

    public void MovePlayer()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        Debug.Log("Got player: " + player.gameObject.name, player.gameObject);

        player.gameObject.transform.rotation = transform.rotation;
        player.gameObject.transform.position = transform.position;
        Debug.Log("Moved player", gameObject);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateState();
    }

    public void UpdateState()
    {
#if UNITY_EDITOR

#endif
        if (sceneType == SceneController.instance.sceneType)
        {
            gameObject.SetActive(true);
            if (movePlayer)
            {
                MovePlayer();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}

