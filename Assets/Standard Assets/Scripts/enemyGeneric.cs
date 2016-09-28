using UnityEngine;
using System.Collections;

public class enemyGeneric : MonoBehaviour {

	//Public
	protected int Level;
	protected int Strength;
	protected int Dexterity;
	protected int Vitality;
	protected int Intelligence;
	protected int Luck;
	protected int MaxHealth;
	protected int CurHealth;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void takeDamage(int incomingDamage)
	{
		CurHealth -= incomingDamage;
	}
	
}