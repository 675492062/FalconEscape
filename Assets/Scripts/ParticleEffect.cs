using UnityEngine;
using System.Collections;

public class ParticleEffect : MonoBehaviour {
	
	private ParticleSystem partSys;
	private Light effectLight;
	private float lightIntensity = 1.20f;
	private float timeElapsed = 0f;
	private float totalDuration = 0f;
	// Use this for initialization
	void Start () {
		partSys = GetComponent<ParticleSystem>();
		totalDuration = (partSys.duration + partSys.startLifetime);
		Invoke("DestroyEffect", totalDuration);
		effectLight = GetComponent<Light>();
		lightIntensity *= lightIntensity;
	}
	
	void Update(){
		timeElapsed += Time.deltaTime;
		float t = (timeElapsed / totalDuration);
		
		effectLight.intensity =  lightIntensity * Mathf.Sin(Mathf.PI * t);
	}
	
	void DestroyEffect(){
		Destroy(gameObject);
	}
}
