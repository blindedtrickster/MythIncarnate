using UnityEngine;
using System.Collections;

public class PlayerStatistics : MonoBehaviour {
	
	//Public
	public CharacterInfo Stats = new CharacterInfo();

	//Private
	//public int characterID = 0;	//Each character gets their own number. it's effectively an ID.

	// Use this for initialization
	void Start () {

		if(GameManager.Instance.Players[Stats.charID].Level != 0)
		{
			Stats = GameManager.Instance.Players[Stats.charID];
		} else {
			//Debug.Log("Didn't pull from the game manager.");
			GameManager.Instance.savePlayer(Stats,Stats.charID);
		}


		if(Stats.Level == 0)
		{
			setNewCharacterStats();
		}

		attackPower();
	}
	
	// Update is called once per frame
	void Update () {
		if(Stats.CurHealth > Stats.MaxHealth)	//Eventually move this to when you GET health.
		{
			trimHealth ();
		}
	}

	void trimHealth() {
		Stats.CurHealth = Stats.MaxHealth;
	}

	void setNewCharacterStats() {
		Stats.Level = 1;
		Stats.Strength = 10;
		Stats.Dexterity = 10;
		Stats.Vitality = 10;
		Stats.Intelligence = 10;
		Stats.Luck = 10;
		Stats.MaxHealth = 15;
		Stats.CurHealth = 15;
		Stats.nextLevel = 50;
		Stats.XP = 0;
	}

	void OnCollisionEnter2D (Collision2D col) {
		//Debug.Log("Hit something");
		if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			//CurHealth -= 1;
		}

		if(col.gameObject.tag == "Door")
		{
			//GameManager.Instance.savePlayer(this, newLoc);
			GameManager.Instance.savePlayer(Stats, Stats.charID);
		}
	}

	public void savePlayer(CharacterInfo player) {
		//GameManager.Instance.savePlayer(player, characterID, newLoc);
	}

	public void takeDamage(int incomingDamage) {
		Stats.CurHealth -= incomingDamage;
		Debug.Log("Took " + incomingDamage + " damage!");
		if(Stats.CurHealth <= 0) {
			Stats.CurHealth = 0;
			Stats.Dead = true;
		}
	}

	void attackPower() {
		Stats.attackPower = (Stats.Strength / 2) + 3; //3 will be replaced with weapon damage.
	}
}
