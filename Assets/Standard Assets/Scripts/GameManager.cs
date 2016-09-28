using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//Public
	public static GameManager Instance;

	//[PlayerInfo]
	public CharacterInfo[] Players = new CharacterInfo[3];
	public GameObject scene;
	public bool paused = false;
	public string prevScene;
	public string activeScene;

	// Use this for initialization
	void Start () {
		scene = GameObject.FindGameObjectWithTag("SceneManager");
	}

	void Awake ()   
	{

		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
			activeScene = SceneManager.GetActiveScene().name;

		} else if (Instance != this) //This section kicks off when the scene's game manager is overwritten by the persisting game manager. 
		{
			Destroy (gameObject);

			prevScene = activeScene;
			activeScene = SceneManager.GetActiveScene().name;
		}
	}

	void OnLevelWasLoaded() {					//Use this to spawn players...?
		prevScene = activeScene;
		activeScene = SceneManager.GetActiveScene().name;
	}

	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp (KeyCode.Escape) && !paused)
		{
			paused = true;
		} else if (Input.GetKeyUp (KeyCode.Escape) && paused)
		{
			paused = false;
		}
	}
	
	public void savePlayer(CharacterInfo playerInfo, int playerID)
	{
		GameManager.Instance.Players[playerID] = playerInfo;
	}
}
