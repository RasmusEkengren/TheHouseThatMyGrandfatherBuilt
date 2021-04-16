using UnityEngine;
using System.Collections;
using FMOD.Studio;
using FMODUnity;


public class AudioManager : MonoBehaviour
{
	public float deathWaitTime = 3.0f;
	public bool showTimeofdayDebug = false;
	public bool showMovementDebug = false;

	[FMODUnity.ParamRef]
	public string AmbianceGlobal;

	private StudioEventEmitter fire;
	private EventInstance Trote;
	private EventInstance steps;
	private EventInstance lands;
	private EventInstance Detect;

	[Header("Event Instances Player")]

	[FMODUnity.EventRef]
	public string PlayerLands = "";
	[FMODUnity.EventRef]
	public string PlayerFootsteps = "";
	[FMODUnity.EventRef]
	public string PlayerFire = "";
	[FMODUnity.EventRef]
	public string PlayerJumps = "";
	[FMODUnity.EventRef]
	public string PlayerDamage = "";

	[Header("Event Instances Enemy")]

	[FMODUnity.EventRef]
	public string MovingEnemyHit = "";
	[FMODUnity.EventRef]
	public string SentryRote = "";
	[FMODUnity.EventRef]
	public string TurretOn = "";



	[Header("Event Instances Objects")]

	[FMODUnity.EventRef]
	public string PickupSound = "";
	[FMODUnity.EventRef]
	public string CheckpointSound = "";
	[FMODUnity.EventRef]
	public string EventB = "";
	[FMODUnity.EventRef]
	public string Won = "";

	private void Awake()
	{
		FMODUnity.RuntimeManager.StudioSystem.setParameterByName(AmbianceGlobal, 0f);
		GameStart();
		//Makes mouse cursor invisible when playing
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	//Runs once when the scene loads
	public void GameStart()
	{
		Debug.Log("GameStart");
		PlayerSpawned();
	}

	//Runs everytime the player spawns
	public void PlayerSpawned()
	{
		Debug.Log("PlayerSpawned");
	}

	//Prints the name of the current Game Object the player steps on
	public void PlayerFootstep(Transform obj)
	{
		steps = RuntimeManager.CreateInstance(PlayerFootsteps);
		steps.start();


		if (showMovementDebug)
			Debug.Log("PlayerFootstep on object: " + obj.name);

		if (obj.name == "Water")
		{
			steps.setParameterByName("Surface", 1f);
		}

		if (obj.name == "Platform")
		{
			steps.setParameterByName("Surface", 2f);
		}

	}

	//Prints the name of the current Game Object the player jumps from
	public void PlayerJump(Transform obj)
	{
		FMODUnity.RuntimeManager.PlayOneShotAttached(PlayerJumps, gameObject);

		if (showMovementDebug)
			Debug.Log("PlayerJump on object: " + obj.name);
	}

	//Prints the name of the current Game Object the player lands on
	public void PlayerLand(Transform obj)
	{

		lands = RuntimeManager.CreateInstance(PlayerLands);
		lands.start();

		if (showMovementDebug)
			Debug.Log("PlayerLand on object: " + obj.name);

		if (obj.name == "Water")
		{
			lands.setParameterByName("Surface", 1f);
		}

		if (obj.name == "Platform")
		{
			lands.setParameterByName("Surface", 2f);
		}
	}

	public void PlayerShoot()
	{
		FMODUnity.RuntimeManager.PlayOneShotAttached(PlayerFire, gameObject);

		Debug.Log("PlayerShoot");
	}

	public void PlayerDied()
	{
		FMODUnity.RuntimeManager.PlayOneShotAttached(PlayerDamage, gameObject);

		Debug.Log("PlayerDied");
		//FindObjectOfType<Player>().RespawnRoutine(deathWaitTime / 10);
	}

	public void CheckpointTaken(GameObject instance)
	{
		FMODUnity.RuntimeManager.PlayOneShotAttached(CheckpointSound, gameObject);

		Debug.Log("CheckpointTaken: " + instance.name);
	}

	public void PickupTaken(GameObject instance)
	{
		FMODUnity.RuntimeManager.PlayOneShotAttached(PickupSound, gameObject);

		Debug.Log("PickupTaken: " + instance.name);
	}

	public void MovingNPCSpawned()
	{
		Debug.Log("MovingNPCSpawned");
	}

	//When player hits Moving NPC with a bullet
	public void MovingNPCHit(int hitpoints)
	{
		FMODUnity.RuntimeManager.PlayOneShotAttached(MovingEnemyHit, gameObject);

		Debug.Log("MovingNPC Hitpoints left: " + hitpoints);
	}

	public void MovingNPCKilledplayer()
	{

		Debug.Log("MovingNPCKilledplayer");
	}

	public void MovingNPCDied()
	{
		Debug.Log("MovingNPCDie");

	}

	//Runs when player enters Sentrygun/Sensor's trigger
	public void StationaryNPCActivated(GameObject instance)
	{
		Detect = RuntimeManager.CreateInstance(TurretOn);
		RuntimeManager.AttachInstanceToGameObject(Detect, instance.transform, instance.GetComponent<Rigidbody>());
		Detect.start();

		fire = instance.GetComponentInChildren<StudioEventEmitter>();
		Debug.Log("StationaryNPCActivated: " + instance.name);
	}

	//Runs when player exits Sentrygun/Sensor's trigger
	public void StationaryNPCDeactivated()
	{
		Detect.setParameterByName("Detect", 1f);
		Detect.start();
		Debug.Log("StationaryNPCDeactivated");

	}

	public void StationaryNPCShoot()
	{

		fire.Play();
		Debug.Log("StationaryNPCShoot");

	}

	public void StationaryNPCRotationStarted(GameObject instance)
	{
		Trote = RuntimeManager.CreateInstance(SentryRote);
		RuntimeManager.AttachInstanceToGameObject(Trote, instance.transform, instance.GetComponent<Rigidbody>());
		Trote.start();


		Debug.Log("StationaryNPCRotationStarted");
	}

	public void StationaryNPCRotationStopped()
	{
		Trote.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		Debug.Log("StationaryNPCRotationStopped");
	}

	public void StationaryNPCDied()
	{

		Debug.Log("StationaryNPCDied");
	}

	public void EventATriggered()
	{
		Debug.Log("EventATriggered");


	}

	public void EventBTriggered()
	{
		FMODUnity.RuntimeManager.PlayOneShotAttached(EventB, gameObject);

		Debug.Log("EventBTriggered");

	}

	public void PlayerWon(float waitTime)
	{
		FMODUnity.RuntimeManager.PlayOneShotAttached(Won, gameObject);

		Debug.Log("PlayerWon - waiting: " + waitTime + "seconds");
	}

	//Receives the current value of TimeOfDay once every frame
	public void Timeofday(float timeofday)
	{
		if (showTimeofdayDebug)
			Debug.Log("TimeOfDay is: " + timeofday);
	}

	public void LocationSwitch()
	{
		FMODUnity.RuntimeManager.StudioSystem.setParameterByName(AmbianceGlobal, 1f);
	}
}