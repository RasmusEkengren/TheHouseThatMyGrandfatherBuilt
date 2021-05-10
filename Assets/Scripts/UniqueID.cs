using UnityEngine;
using UnityEditor.SceneManagement;

[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
	public string ID;
	void Update()
	{
		CheckID();
	}
	public void CheckID()
	{
		if (ID == null || ID == "")
		{
			Debug.Log("ID: " + ID);
			string sceneName = this.gameObject.scene.name;
			if (sceneName == null) return;
			ID = sceneName + "_" + gameObject.name + "_" + (gameObject.transform.position.x * gameObject.transform.position.y * gameObject.transform.position.z);
		}
	}
}
