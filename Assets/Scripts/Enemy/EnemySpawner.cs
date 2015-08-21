using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	
	public GameObject enemyPrefab;
	public float boundary;
	public GameObject enemyGrouper;
	private GameObject player;
	private int enemyCount;
	private int enemiesKilled;
	
	public Text enemyCountText;
	public Text enemiesKilledText;
		
	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("SpawnRandomEnemy", 0f, 2f);
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void SpawnRandomEnemy ()
	{	if(enemyCount > 20){
			return;
		}
		Vector3 position;
		do {
			position = new Vector3 (Random.Range (-boundary, +boundary), Random.Range (-boundary, +boundary), Random.Range (-boundary, +boundary));
		} while(Vector3.Distance(position, player.transform.position) < 30f);
		
		GameObject enemy = (GameObject)Instantiate (enemyPrefab, position, Quaternion.identity);
		enemy.transform.parent = enemyGrouper.transform;
		enemyCount++;
		enemyCountText.text = enemyCount.ToString();
	}
	
	void EnemyKilled(){
		enemyCount--;
		enemyCountText.text = enemyCount.ToString();
		enemiesKilled++;
		enemiesKilledText.text = enemiesKilled.ToString();
	}
}