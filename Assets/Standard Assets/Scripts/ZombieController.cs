using UnityEngine;
using System.Collections;

public class ZombieController : enemyGeneric {
	//Public
	public float moveSpeed = 1.5f;
	public GameObject[] targets;
	public GameObject curTarget;
	public Vector2 targetLoc;


	//Private
	private float dirToTargetX;
	private float dirToTargetY;
	private float targetTimer;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.Instance.paused)
		{
			updateTarget();		//Update target every few seconds. Update script to grab list of players after scene has been loaded.
			moveZombie();
		} 
	}

	void updateTarget()
	{
		targetTimer += Time.deltaTime;
		if(targetTimer >= 5.0f || curTarget == null)
		{
			targets = GameObject.FindGameObjectsWithTag("Player");
			curTarget = targets[0];
			orderTargetsByDistance();
			targetTimer = 0.0f;
		}

		targetLoc = curTarget.transform.position;

	}

	void orderTargetsByDistance()
	{
		int closestPlayer = 0;
		float closestDistance = 100;
		float[] targetDistances = new float[targets.Length];

		for(int i = 0; i < targets.Length; i++)		//Gets the distance of all players and stores in a list
		{
			targetDistances[i] = Vector3.Distance(targets[i].transform.position,transform.position);
		}

		for(int i = 0; i < targets.Length; i++)		//Checks who is closest (by distance) and get the closest player to the monster.
		{
			if(targetDistances[i] < closestDistance)
			{
				closestDistance = targetDistances[i];
				closestPlayer = i;
			}
		}

		curTarget = targets[closestPlayer];		//Update later to only select player who is alive.
	}

	void moveZombie()
	{
		clearMovement();										
		setAnimation();
		applyMovement();
	}

	void clearMovement()
	{
		anim.SetBool("moveDown", false);
		anim.SetBool("moveUp", false);
		anim.SetBool("moveLeft", false);
		anim.SetBool("moveRight", false);
	}

	void setAnimation()
	{

	}

	void applyMovement()
	{
		//Get differences between target and enemy.
		dirToTargetX = curTarget.transform.position.x - this.transform.position.x;
		dirToTargetY = curTarget.transform.position.y - this.transform.position.y;

		//Set movement animation based on difference between target and enemy.
		if(dirToTargetX < 0 && Mathf.Abs(dirToTargetX) > Mathf.Abs(dirToTargetY) ) //Oriented left
		{
			anim.SetBool("moveLeft",true);
		} else if(dirToTargetX > 0 && Mathf.Abs(dirToTargetX) > Mathf.Abs(dirToTargetY) ) //Oriented right.
		{
			anim.SetBool("moveRight",true);
		} else if(dirToTargetY < 0 && Mathf.Abs(dirToTargetY) > Mathf.Abs(dirToTargetX) ) //Oriented down.
		{
			anim.SetBool("moveDown",true);
		} else if(dirToTargetY > 0 && Mathf.Abs(dirToTargetY) > Mathf.Abs(dirToTargetX) ) //Oriented up
		{
			anim.SetBool("moveUp",true);
		}

		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x,transform.position.y), targetLoc, moveSpeed * Time.deltaTime);
	}
}
