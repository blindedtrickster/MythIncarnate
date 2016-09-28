using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[System.Serializable]

public class CharacterInfo  {

	//Statistics
	public string Name;
	public string Player;
	public int Level;
	public int Strength;
	public int Dexterity;
	public int Vitality;
	public int Intelligence;
	public int Luck;
	public int MaxHealth;
	public int CurHealth;
	public int nextLevel;
	public int XP;
	[SerializeField] public int charID;
	public int attackPower;

	//Statuses
	public bool Dead;
	public bool Poisoned;
	public bool Frozen;
	public bool Burned;
}
