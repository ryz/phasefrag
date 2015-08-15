using UnityEngine;
using System.Collections;

public class SimpleEnemy : MonoBehaviour {

	public int HP = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(HP <= 0)
			Death ();
	}

	public void Hurt()
	{
		// Reduce the number of hit points by one.
		Debug.Log("hurt!");
		HP--;
	}

	public void Death() {
		Destroy(gameObject);
		Debug.Log("DESTROYED!");
	}

}
