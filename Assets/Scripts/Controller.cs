using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Controller : MonoBehaviour
{
	
	public float sensitivity;
	public GameObject gun;
	public float cameraMaxRot;
	public float gunMaxRotVertical;
	public float gunMaxRotHorizontal;
	public float timeToReturnToFront;
	public float timeWaitingToReturnToFront;
	
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 inputValue;
		
		inputValue.x = CrossPlatformInputManager.GetAxis ("Vertical");
		inputValue.y = CrossPlatformInputManager.GetAxis ("Horizontal");
		inputValue.z = 0;
		
		if (inputValue == Vector3.zero && transform.localRotation != Quaternion.identity) { 
			timeWaitingToReturnToFront += Time.deltaTime;
			if(timeWaitingToReturnToFront >= timeToReturnToFront){
				transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * 2f);
				gun.transform.localRotation = Quaternion.Slerp(gun.transform.localRotation, Quaternion.identity, Time.deltaTime * 2f);
			}
		} else {
			timeWaitingToReturnToFront = 0;		
			Vector3 cameraLocalEulerAngles = transform.localEulerAngles + (inputValue * sensitivity * Time.deltaTime);			
			cameraLocalEulerAngles.x = ClampAngle (cameraLocalEulerAngles.x, -cameraMaxRot, cameraMaxRot);
			cameraLocalEulerAngles.y = ClampAngle (cameraLocalEulerAngles.y, -cameraMaxRot, cameraMaxRot);
			transform.localEulerAngles = cameraLocalEulerAngles;
			
			Vector3 gunLocalEulerAngles = gun.transform.localEulerAngles + (inputValue * sensitivity * Time.deltaTime);			
			gunLocalEulerAngles.x = ClampAngle (gunLocalEulerAngles.x, -gunMaxRotVertical, gunMaxRotVertical);
			gunLocalEulerAngles.y = ClampAngle (gunLocalEulerAngles.y, -gunMaxRotHorizontal, gunMaxRotHorizontal);
			gun.transform.localEulerAngles = gunLocalEulerAngles;		
		}		
		
		
	}
	
	private float ClampAngle (float angle, float min, float max)
	{
		
		if (angle < 90 || angle > 270) {       // if angle in the critic region...
			if (angle > 180)
				angle -= 360;  // convert all angles to -180..+180
			if (max > 180)
				max -= 360;
			if (min > 180)
				min -= 360;
		}    
		angle = Mathf.Clamp (angle, min, max);
		if (angle < 0)
			angle += 360;  // if angle negative, convert to 0..360
		return angle;
	}
	
	
}
