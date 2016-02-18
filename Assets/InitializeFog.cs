using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class InitializeFog : MonoBehaviour {
	public string Camera = "Camera0";
	// Use this for initialization
	void Start () {
		GameObject camera = GameObject.Find (Camera);
		//camera.AddComponent<GlobalFog>();
		//UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(camera, "Assets/InitializeFog.cs (10,3)", "GlobalFog");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
