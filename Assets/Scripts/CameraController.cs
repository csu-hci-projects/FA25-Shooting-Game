using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class CameraController : MonoBehaviour
{
	public GameObject[] cameras = Admin.cameras;
	private int currentCameraIndex = Admin._cam_index;
	private int _cam_ID;

	// Use this for initialization
	void Start () {

		  //Start the engine and register the missile
         
       _cam_ID = Admin.RegisterCAM(this.gameObject);
        Debug.Log($"{_cam_ID} This is cam ID");
		
		// //Turn all cameras off, except the first default one
		// for (int i=1; i<cameras.Length; i++)
		// {

		// 	cameras[i].gameObject.SetActive(false);
		// 	Debug.Log ("Camera with name: " + cameras[i].gameObject.name + ", is now disabled");
			
		// }

		// //If any cameras were added to the controller, enable the first one
		// if (cameras.Length>0)
		// {
		// 	cameras [0].gameObject.SetActive (true);
		// 	Debug.Log ("Camera with name: " + cameras[0].gameObject.name + ", is now enabled");
		// }
	}

	// Update is called once per frame
	void Update () {
		//If the c button is pressed, switch to the next camera
		//Set the camera at the current index to inactive, and set the next one in the array to active
		//When we reach the end of the camera array, move back to the beginning or the array.
		Debug.Log ($"Current Camera Index: {Admin._cam_index} ");
		if (Input.GetKeyDown(KeyCode.C)&&cameras[Admin._cam_index]==this.gameObject)
		{
			

			Debug.Log ("C button has been pressed. Switching to the next camera");
			cameras[Admin._cam_index].gameObject.SetActive(false);
			Admin._cam_index++;
			if (Admin._cam_index >= Admin._cam_total)
				Admin._cam_index = 0;
            cameras[Admin._cam_index].gameObject.SetActive(true);

			Debug.Log ("Camera with name: " + cameras [currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");


		}
	}
}