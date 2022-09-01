using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	Debug.Log("Press 'W' to move foreward and 'S' to move back!");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.W)){
	transform.position += transform.forward * 10 * Time.deltaTime;	
		}
		
		if(Input.GetKey(KeyCode.S)){
	transform.position += transform.forward * -10 * Time.deltaTime;	
		}
		
	}
}
