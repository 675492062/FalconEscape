using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	
	public TextMesh close;
	public TextMesh far;
	public float range;
	private Ray shootRay;                                   // A ray from the gun end forwards.
	private RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	private int shootableMask;
	
	private bool canShoot = true;
	
	// Use this for initialization
	void Start () {
		shootableMask = LayerMask.GetMask ("Shootable");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!canShoot) {
			close.color = Color.gray;
			far.color = Color.gray;
		} else {	
			
			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;
			
			// Perform the raycast against gameobjects on the shootable layer and if it hits something...
			if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
				
				close.color = Color.red;
				far.color = Color.red;
					
			}
			// If the raycast didn't hit anything on the shootable layer...
			else {
				close.color = Color.gray;
				far.color = Color.gray;
			}
		}
	}
}
