using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class uiManager : MonoBehaviour {

	//Public
	public GameObject[] Players;
	public Text P1Health;
	public Text P2Health;
	public Text P3Health;

	public Image P1Portrait;
	public Image P2Portrait;
	public Image P3Portrait;
	public Image portraitSwap;

	private string PlayerDisplay;

	// Use this for initialization
	void Start () {
		//Players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(Players.Length == 0)
		{
			Players = GameObject.FindGameObjectsWithTag("Player");
		}
		showHealth ();

	}

	void FixedUpdate() {

	}

	void showHealth() {

		for(int i = 0; i < Players.Length; i++)
		{
			if(Players[i].GetComponent<PlayerStatistics>().Stats.Player == "P1")
			{
				PlayerDisplay = Players[i].GetComponent<PlayerStatistics>().Stats.CurHealth.ToString() + "/" + Players[i].GetComponent<PlayerStatistics>().Stats.MaxHealth.ToString();
				P1Health.text = PlayerDisplay;
			}

			else if(Players[i].GetComponent<PlayerStatistics>().Stats.Player == "P2")
			{
				PlayerDisplay = Players[i].GetComponent<PlayerStatistics>().Stats.CurHealth.ToString() + "/" + Players[i].GetComponent<PlayerStatistics>().Stats.MaxHealth.ToString();
				P2Health.text = PlayerDisplay;
			}

			else if(Players[i].GetComponent<PlayerStatistics>().Stats.Player == "P3")
			{
				PlayerDisplay = Players[i].GetComponent<PlayerStatistics>().Stats.CurHealth.ToString() + "/" + Players[i].GetComponent<PlayerStatistics>().Stats.MaxHealth.ToString();
				P3Health.text = PlayerDisplay;
			}
		}

	}
}
