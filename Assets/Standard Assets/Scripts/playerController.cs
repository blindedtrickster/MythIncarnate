using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	//Public
	[SerializeField] public BoxCollider2D attackCollider;
	public Animator anim;
	public float moveSpeed = 5.0f;
	public float runMultiplier = 1.5f;
	public float attackTimer = 0.0f;
	public string Player;

	//Private
	[SerializeField] private CharacterInfo CharStats;
	private bool isJumping = false;
	private bool attacking = false;
	private bool isRunning = false;
	private bool xAxisInUse;
	private bool yAxisInUse;
	private float lastTapTime = -1.0f;
	private float jumpTimer = 0.0f;
	private float lastMoveX;
	private float lastMoveY;
	private int attackPower;
	private string runKey = "";
	private Vector3 zAxis;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator>();
		CharStats = this.gameObject.GetComponent<PlayerStatistics>().Stats;
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.Instance.paused)
		{
			movePlayer();
		} 
	}

	void updateZAxis() {
		//+9Z at the top of the screen and -9Z at the bottom.
		zAxis = transform.position;
		zAxis.z = Mathf.Round (transform.position.y);
		transform.position = zAxis;
	}

	void movePlayer() {
		clearMovement();
		characterInput();
		applyMovement();
	}

	void clearMovement() {
		//Clear movement
		anim.SetBool("moveDown", false);
		anim.SetBool("moveUp", false);
		anim.SetBool("moveLeft", false);
		anim.SetBool("moveRight", false);

		//Clear attacks
		anim.SetBool("isPunching", false);
	}

	void characterInput() {

		//Jumping
		if (Input.GetKeyDown(KeyCode.Space)) {
			anim.SetTrigger("isJumping");
			isJumping = true;
			this.gameObject.layer = LayerMask.NameToLayer("inAir");
		}
		
		if(isJumping) {
			jumpTimer += Time.deltaTime;
			
			if(jumpTimer > 0.5f) {
				isJumping = false;
				this.gameObject.layer = LayerMask.NameToLayer("Default");	//Return to normal layer
				jumpTimer = 0.0f;
				
				lastMoveX = 0.0f;
				lastMoveY = 0.0f;
			}
		}

		if(Input.GetAxis (Player + "Horizontal") == 0 && !isJumping)
		{
			lastMoveX = 0.0f;
		}

		if(Input.GetAxis (Player + "Vertical") == 0 && !isJumping)
		{
			lastMoveY = 0.0f;
		}
			

		
		//Vertical Movement
		//-----------------
		if (Input.GetAxis (Player + "Vertical") > 0)						//Moving up
		{					
			
			//First 'frame' of movement
			if(!yAxisInUse)
			{
				if(!isRunning && Time.time < (lastTapTime+0.5f))
				{
					anim.SetTrigger("isRunning");
					isRunning = true;
					runKey = Player + "Vertical";
				} else {
					lastTapTime = Time.time;
					yAxisInUse = true;
				}
			}
			
			//Continual movement up
			if(yAxisInUse)													
			{
				anim.SetBool("moveUp", true);
			}
		} 
		else if(Input.GetAxis(Player + "Vertical") < 0)					//Moving down
		{
			//First 'frame' of movement.
			if(!yAxisInUse)													
			{
				if(!isRunning && Time.time < (lastTapTime+0.5f))
				{
					anim.SetTrigger("isRunning");
					isRunning = true;
					runKey = Player + "Vertical";
				} else {
					lastTapTime = Time.time;
					yAxisInUse = true;
				}
			}
			
			//Continual movement down
			if(yAxisInUse)													
			{
				anim.SetBool ("moveDown", true);
			}
		}
		else if (Input.GetAxis(Player + "Vertical") == 0)					//No vertical movement
		{
			if(yAxisInUse)
			{
				yAxisInUse = false;
			}
			
		}

		//Horizontal movement
		//-------------------
		if (Input.GetAxis (Player + "Horizontal") > 0)						//Moving to the right
		{					

			//First 'frame' of movement
			if(!xAxisInUse)
			{
				if(!isRunning && Time.time < (lastTapTime+0.5f))
				{
					anim.SetTrigger("isRunning");
					isRunning = true;
					runKey = Player + "Horizontal";
				} else {
					lastTapTime = Time.time;
					xAxisInUse = true;
				}
			}

			//Continual movement to the right
			if(xAxisInUse)													
			{
				anim.SetBool("moveRight", true);
			}
		} 
		else if(Input.GetAxis(Player + "Horizontal") < 0)					//Moving to the left
		{
			//First 'frame' of movement.
			if(!xAxisInUse)													
			{
				if(!isRunning && Time.time < (lastTapTime+0.5f))
				{
					anim.SetTrigger("isRunning");
					isRunning = true;
					runKey = Player + "Horizontal";
				} else {
					lastTapTime = Time.time;
					xAxisInUse = true;
				}
			}

			//Continual movement to the left
			if(xAxisInUse)													
			{
				anim.SetBool ("moveLeft", true);
			}
		}
		else if (Input.GetAxis(Player + "Horizontal") == 0)					//No horizontal movement
		{
			if(xAxisInUse)
			{
				xAxisInUse = false;
			}

		}
		
		if(runKey != "" && Input.GetAxis(runKey) == 0)	//Stop running when you let go of the appropriate key.
		{
			isRunning = false;
			runKey = "";
		}
		
		//Attacks
		if (Input.GetKey (KeyCode.T)) 					//While you hold the attack button down, you 'charge' an attack.
		{
			attackTimer += Time.deltaTime;
			attack ();
		}
		
		if (Input.GetKeyUp (KeyCode.T))						//When you let go of the attack button, it does an attack appropriate for how long you were charging.
		{
			if(attackTimer <= 1.0f)			//Basic attack has a window of 1 second.
			{
				attackTimer = 0.0f;
				anim.SetTrigger("isPunching");
			} else {
				attackTimer = 0.0f;							//End case. Reset attack timer.
			}
		}
	}

	void applyMovement() {

		updateZAxis();

		//Apply run speed if applicable
		if(isRunning)
		{
			moveSpeed *= runMultiplier;
		}

		//Normal movement
		if(!isJumping)										
		{
			if (anim.GetBool("moveDown")) {
				transform.Translate(0, -1 * moveSpeed * Time.deltaTime, 0);
				lastMoveY = -1 * moveSpeed;
			}
			
			if (anim.GetBool("moveUp")) {
				transform.Translate(0, moveSpeed * Time.deltaTime, 0);
				lastMoveY = moveSpeed;
			}
			
			if (anim.GetBool("moveLeft")) {
				transform.Translate(-1 * moveSpeed * Time.deltaTime, 0, 0);
				lastMoveX = -1 * moveSpeed;
			}
			
			if (anim.GetBool("moveRight")) {
				transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
				lastMoveX = moveSpeed;
			}
		}

		//Jumping movement
		else {
			transform.Translate (lastMoveX * Time.deltaTime,lastMoveY * Time.deltaTime, 0);
		}

		//Remove run speed if applicable
		if(isRunning)
		{
			moveSpeed /= runMultiplier;
		}

	}

	void attack() {
		attackCollider.enabled = true;
		attacking = true;
	}

	void stopAttacking() {
		attacking = false;
		attackCollider.enabled = false;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			if(attacking)
			{		
				Debug.Log("Attacked an enemy!");
				col.gameObject.SendMessage("takeDamage",CharStats.attackPower);
			}
		}
	}
}