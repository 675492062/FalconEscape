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
	private AudioSource audioSrc;
	private int currentBarrel;
	private float timeSinceFired;
	
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
		
		if (Input.GetButton ("Fire1")) {
			if (timeSinceFired >= (1 / fireRate)) {
				timeSinceFired = 0;
				Shoot();
			} else {
				timeSinceFired += Time.deltaTime;
			}
		}
		
		
	}
	
	private void Shoot ()
	{
		gunBarrelsAnimators [currentBarrel].SetTrigger ("Firing");
		GameObject redLaser = (GameObject) Instantiate (laser, gunBarrelsEnds [currentBarrel].transform.position, gun.transform.rotation);
		redLaser.SendMessage("SetRedLaser");
		
		audioSrc.clip = shotSound;
		audioSrc.Play ();
		
		currentBarrel = (currentBarrel + 1) % gunBarrelsEnds.Length;	
	
	}
	
	private void HitByLaser(){
		print ("Taking damage from laser");
		
		AudioSource.PlayClipAtPoint(damageSound, transform.position);
	}
}
