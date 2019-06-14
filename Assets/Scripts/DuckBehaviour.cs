using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DuckBehaviour : MonoBehaviour {
    private GameObject closestCrumb;
    private NavMeshAgent duckAI;
    private Animator duckController;
    private Rigidbody duckRB;

	// Use this for initialization
	void Start () {
        
        if(GetComponent<NavMeshAgent>() == null)
        {
            gameObject.AddComponent<NavMeshAgent>();
        }
        //GetComponent<Rigidbody>().useGravity = true;
        duckAI = GetComponent<NavMeshAgent>();
        duckController = GetComponentInChildren<Animator>();
        duckAI.speed = DuckManager.instance.duckSpeed;
    }
	
	// Update is called once per frame
	void Update () {

        if(closestCrumb == null)
        {
            duckAI.destination = new Vector3(0, 0, 0); //set up wander behavior here
        }else if(closestCrumb.transform.position != duckAI.destination)
        {
            duckAI.destination = closestCrumb.transform.position;
        }
        Vector3 vel = duckAI.velocity;
        duckController.SetFloat("Speed", vel.magnitude);
        int waterMask = 1 << NavMesh.GetAreaFromName("Water");
        NavMeshHit hit;
        if(NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            if(hit.mask == waterMask) {
                duckController.SetBool("onWater", true);
                duckAI.baseOffset = -0.5f;
            } else {
                duckController.SetBool("onWater", false);
                duckAI.baseOffset = -0.05f;
            }
            
        }

    }

    public void SetClosestCrumb(GameObject go)
    {
        closestCrumb = go;
    } 

}
