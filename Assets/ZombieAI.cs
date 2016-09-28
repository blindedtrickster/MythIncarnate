using UnityEngine;
using System.Collections;

public class ZombieAI : enemyGeneric {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void takeDamage(int incomingDamage) {
		Debug.Log("Zombie is taking " + incomingDamage + " points of damage.");
		CurHealth -= CurHealth;
	}
}
