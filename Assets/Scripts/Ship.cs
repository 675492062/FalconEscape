using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	
	public GameObject gun;
	public GameObject[] gunBarrels;
	public Transform[] gunBarrelsEnds;
	public Animator[] gunBarrelsAnimators;
	public GameObject laser;
	public Transform[] waypoints;
	public int currentWaypoint;
	public bool travelling;
	public float fireRate;
	public AudioClip shotSound;
	public AudioClip damageSound;
	public Light interiorTurretPointLight;
	private GameObject uiCanvas;
	private AudioSource audioSrc;
	private int currentBarrel;
	private float timeSinceFired;
	private float timeSinceTurretChange;
	private GameObject playerShip;
	private Quaternion otherTurretLastRotation;
	private Color32 turret1Color = new Color32(255, 132, 0, 255);
	private Color32 turret2Color = new Color32(0, 254, 255, 255);
	
	// Use this for initialization
	void Start ()
	{
		currentWaypoint = 0;
		currentBarrel = 0;
		
		gunBarrelsAnimators = new Animator[gunBarrels.Length];
		for (int i = 0; i<gunBarrels.Length; i++) {
			gunBarrelsAnimators [i] = gunBarrels [i].GetComponent<Animator> ();
			
		}
		
		audioSrc = GetComponent<AudioSource> ();
		uiCanvas = GameObject.FindGameObjectWithTag ("UI");
		otherTurretLastRotation = Quaternion.identity;
		interiorTurretPointLight.color = turret1Color;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (travelling) {
	
			if (Vector3.Distance (transform.position, waypoints [currentWaypoint].transform.position) < 0.5f)
				currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
		
			transform.position = Vector3.Lerp (transform.position, waypoints [currentWaypoint].transform.position, Time.deltaTime * 1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, waypoints [currentWaypoint].transform.rotation, Time.deltaTime * .5f);
		}
		
		// If the player press to shoot
		if (Input.GetButton ("Fire1")) {
			if (timeSinceFired >= (1 / fireRate)) {
				timeSinceFired = 0;
				Shoot ();
			}
		}
		
		timeSinceFired += Time.deltaTime;
		
		// If the player press to change turret
		if (Input.GetButton ("Fire2")) {
			if (timeSinceTurretChange > .5f) {
				transform.Rotate (new Vector3 (0f, 180f, 0f));
				uiCanvas.SendMessage ("ChangeTurret");
				Quaternion currentRotation = gun.transform.localRotation;
				gun.transform.localRotation = otherTurretLastRotation;
				otherTurretLastRotation = currentRotation;
				timeSinceTurretChange = 0;
				
				if(interiorTurretPointLight.color == turret1Color)
					interiorTurretPointLight.color = turret2Color;
				else
					interiorTurretPointLight.color = turret1Color;
			}
		}
		timeSinceTurretChange += Time.deltaTime;
		
	}
	
	private void Shoot ()
	{
		gunBarrelsAnimators [currentBarrel].SetTrigger ("Firing");
		GameObject redLaser = (GameObject)Instantiate (laser, gunBarrelsEnds [currentBarrel].transform.position, gun.transform.rotation);
		redLaser.SendMessage ("SetRedLaser");
		
		audioSrc.clip = shotSound;
		audioSrc.Play ();
		
		currentBarrel = (currentBarrel + 1) % gunBarrelsEnds.Length;	
	
	}
	
	private void HitByLaser ()
	{
		print ("Taking damage from Laser");
		
		AudioSource.PlayClipAtPoint (damageSound, transform.position);
	}
}
