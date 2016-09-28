using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	//public GameObject[] Players = GameManager.Instance.Players;
	public string prevScene;
	public GameObject[] SpawnPoints;
	public GameObject charSpawnPoint;

	// Use this for initialization
	void Start () {
		//-----------------------------------------------------------------------
		prevScene = GameManager.Instance.prevScene;

		SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		
		for(int i = 0; i < SpawnPoints.Length; i++)
		{
			foreach(CharacterInfo character in GameManager.Instance.Players)
			{

				if(SpawnPoints[i].name == ("from" + prevScene + character.Player))
				{
					charSpawnPoint = SpawnPoints[i];
					Object newCharacter = Instantiate(Resources.Load(character.Name),charSpawnPoint.transform.position,Quaternion.identity);
					newCharacter.name = character.Name;
					continue;	//Continue with next character because we found the right one.
				}
				
			}
		}
		//-----------------------------------------------------------------------
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnEnable() {
		prevScene = GameManager.Instance.prevScene;
	}

	void Awake() {
		prevScene = GameManager.Instance.prevScene;
	}

}
