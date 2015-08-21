using UnityEngine;
using System.Collections;

public class EnemyAI : FSM
{

	public enum FSMState
	{
		Spawn,
		Orbiting,
		Chasing,
		Attacking,
		Evading,
		Dying
	}
	
	public FSMState currentState;
	public float currentSpeed;
	public float currentRotSpeed;
	public float orbitSpeed;
	public float chaseSpeed;
	public float attackSpeed;
	public float evadeSpeed;
	public float rotSpeed;
	public float verticalWiggle;
	public float orbitRadius;
	public float distanceToEvade;
	public float timeToStopOrbiting;
	
	private float timer;
	private float orbitDirection;
	private float evadingDirection;
	private Quaternion evadingRotation;
	
	
	protected override void Initialize ()
	{
		
		//StartPosition ?? 
		
		currentState = FSMState.Spawn;
		
		//Casually orbiting the player
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	
	}
	
	protected override void FSMUpdate ()
	{
		switch (currentState) {
		case FSMState.Spawn:
			UpdateSpawnState ();
			break;
		case FSMState.Orbiting:
			UpdateOrbitingState ();
			break;
		case FSMState.Chasing:
			UpdateChasingState ();
			break;
		case FSMState.Attacking:
			UpdateAttackingState ();
			break;
		case FSMState.Evading:
			UpdateEvadingState ();
			break;	
		case FSMState.Dying:
			UpdateDyingState ();
			break;	
		}
	}

	void UpdateSpawnState ()
	{
		orbitRadius = Vector3.Distance (transform.position, target.position);
		orbitDirection = RandomSign();
		currentState = FSMState.Orbiting;
	}

	protected void UpdateOrbitingState ()
	{	
		currentSpeed = orbitSpeed;
		currentRotSpeed = rotSpeed;		
		timer += Time.deltaTime;
		/*	
		Vector3 orbitingPos = new Vector3(
				target.position.x + Mathf.Sin(timer) * orbitRadius,
				transform.position.y + verticalWiggle * Mathf.Sin(timer),
				target.position.z + Mathf.Cos(timer) * orbitRadius
				);
		
		transform.position = orbitingPos * currentSpeed;
		*/
		transform.RotateAround(target.transform.position, transform.up, Time.deltaTime * orbitDirection * currentSpeed);
		
		if (timer > timeToStopOrbiting) {
			timer = 0f;
			currentState = FSMState.Chasing;
		}
	
	}

	protected void UpdateChasingState ()
	{
		currentSpeed = chaseSpeed;
		currentRotSpeed = rotSpeed;
		
		Vector3 lookAtPosition = Vector3.Slerp (transform.position, target.position, Time.deltaTime * rotSpeed);
		
		transform.LookAt (lookAtPosition);
		
		transform.position += transform.forward * currentSpeed;
		
		if(Vector3.Distance(transform.position, target.transform.position) < distanceToEvade){
			timer = 0f;
			currentState = FSMState.Attacking;
		 } 
	}

	protected void UpdateAttackingState ()
	{
		SendMessage("Shoot");
		
		currentState = FSMState.Evading;
		evadingRotation = Random.rotation;
	}

	protected void UpdateEvadingState ()
	{
		currentSpeed = evadeSpeed;
		currentRotSpeed = rotSpeed;		
		timer += Time.deltaTime;
		
		transform.rotation = Quaternion.Slerp(transform.rotation, evadingRotation, Time.deltaTime * currentRotSpeed);
		transform.position += transform.forward * currentSpeed;
		
		if (timer > 3f) {
			timer = 0f;
			currentState = FSMState.Orbiting;
		}
			
			
		
	}

	protected void UpdateDyingState ()
	{
		throw new System.NotImplementedException ();
	}
	
	
	

	private float RandomSign(){
		float value = 1;
		if (Random.value > 0.5)
			value = -1;
		return value;
	}
	
}
