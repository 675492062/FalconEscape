using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
	
	private string turret1 = "Turret 1";
	private string turret2 = "Turret 2";
	public Text turretName;
	
	void ChangeTurret ()
	{
		if (turretName.text.Equals (turret1))
			turretName.text = turret2;
		else
			turretName.text = turret1;
	
	}
}
