using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject deathParticleEffects;
	public GameObject laser;
	
	public AudioClip deathSound;
	public AudioClip shotSound;
		
	private GameObject particleGrouper;
	// Use this for initialization
	void Start () {
		particleGrouper = GameObject.FindGameObjectWithTag("ParticleGrouper");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void Death(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		GameObject particles = (GameObject) Instantiate(deathParticleEffects, transform.position, transform.rotation);
		particles.transform.parent = particleGrouper.transform;
		Destroy(gameObject);
		GameObject gameLogic = GameObject.FindGameObjectWithTag("GameLogic");
		gameLogic.SendMessage("EnemyKilled");
	}
	
	void Shoot(){
		GameObject greenLaser = (GameObject) Instantiate (laser, transform.position, transform.rotation);
		greenLaser.SendMessage("SetGreenLaser");
		
		AudioSource.PlayClipAtPoint(shotSound, transform.position);
		
	}
	
}
