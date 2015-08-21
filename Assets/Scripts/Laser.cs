using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
	
	public float laserSpeed;
	public GameObject collisionEffect;
	private GameObject particleGrouper;
	private Camera playerCamera;
	public Color redLaserTint = new Color (255, 62, 0);
	public Color redLaserLight = new Color (255, 104, 0);
	public Color greenLaserTint = new Color (100, 255, 0);
	public Color greenLaserLight = new Color (72, 255, 0);
	private bool isEnemy = false;
	
	
	// Use this for initialization
	void Start ()
	{
		playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		particleGrouper = GameObject.FindGameObjectWithTag ("ParticleGrouper");
		transform.parent = particleGrouper.transform;
	}
	
	public void SetRedLaser ()
	{
		GetComponent<Renderer>().material.SetColor ("_TintColor", redLaserTint);
		GetComponent<Light> ().color = redLaserLight;
	}
	
	public void SetGreenLaser ()
	{
		GetComponent<Renderer>().material.SetColor ("_TintColor", greenLaserTint);
		GetComponent<Light> ().color = greenLaserLight;
		isEnemy = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += transform.forward * laserSpeed;
		if (Vector3.Distance (transform.position, playerCamera.transform.position) > playerCamera.farClipPlane) {
			Destroy (gameObject);
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		if(other.CompareTag("Laser")){
			return;
		}
		//If it was shot from the player's cannon
		if (!isEnemy && !other.CompareTag ("Gun")) {
			other.gameObject.SendMessage ("Death");
			GameObject particles = (GameObject)Instantiate (collisionEffect, transform.position, transform.rotation);
			particles.transform.parent = particleGrouper.transform;
			Destroy (gameObject);
			return;
		}
		//IF it was an enemy shot
			else if (isEnemy && !other.CompareTag("Enemy")) {
			other.SendMessageUpwards("HitByLaser");
			Destroy (gameObject);
			return;
		}
		
		
	}
	
}
