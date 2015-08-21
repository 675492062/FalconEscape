using UnityEngine;

public class FSM : MonoBehaviour
{
	protected Transform target;
	

	protected virtual void Initialize ()
	{
	}

	protected virtual void FSMUpdate ()
	{
	}

	protected virtual void FSMFixedUpdate ()
	{
	}

	void Start ()
	{
		Initialize ();
	}

	void Update ()
	{
		FSMUpdate ();
	}

	void FixedUpdate ()
	{
		FSMFixedUpdate ();
	}
}
