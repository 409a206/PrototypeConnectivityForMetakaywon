using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInsideSquareMinimap : MonoBehaviour {

	public Transform MinimapCam;
	public Camera renderingCamera;
	private float MinimapSize;
	Vector3 TempV3;

    void Start() {
        
		//set this value relevant to orthographic camera(minimap camera) size.
		MinimapSize = renderingCamera.orthographicSize;
       //Debug.Log(MinimapSize);
    }

	void Update () {
		MinimapSize = renderingCamera.orthographicSize;
		TempV3 = transform.parent.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;




		transform.position = new Vector3 (
			Mathf.Clamp(transform.position.x, 
			MinimapCam.position.x-MinimapSize,
			MinimapSize+MinimapCam.position.x),
			transform.position.y,
			Mathf.Clamp(transform.position.z, 
			MinimapCam.position.z-MinimapSize, 
			MinimapSize+MinimapCam.position.z)
		);
		//Debug.Log(transform.position);
	}

	void LateUpdate () {
		

       
	}
}