using UnityEngine;
using System.Collections;

public class RabiteController : enemyGeneric {

	//Public
	public Animator anim;

	//Private
	private int attackPower;

	// Use this for initialization
	void Start () {
		attackPower = 3;
		anim = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.Instance.paused) {
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName ("Base.RabiteSpin"));	//If rabite animation state is not currently 'RabiteSpin', then set the parameter.
			{
				anim.SetBool ("Spin",true);
			}
		}
	}



	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Player")
		{
			Debug.Log("Hit player!");
			col.gameObject.SendMessage("takeDamage", attackPower);
		}
	}

	void OnCollision2D(Collider2D col) {
		Debug.Log("Collision happened!");
	}
}
