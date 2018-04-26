using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaFollow : MonoBehaviour {


    public GameObject player;
    private Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
        cameraOffset = new Vector3(0, 3, -6);

        
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //moving camera
        transform.position = Vector3.Lerp(transform.position, player.transform.position + cameraOffset, Time.deltaTime * 6);
        //rotating camera
        var lookPos = player.transform.position - transform.position;
        lookPos.x = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 6);
	}
}
