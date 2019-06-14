using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        GameObject other = col.gameObject;
        if (other.gameObject.CompareTag("Duck"))
        {
            Animator duckController = other.GetComponentInChildren<Animator>();
            duckController.SetTrigger("Eating");
            AnimatorStateInfo eatState = duckController.GetCurrentAnimatorStateInfo(0);
            StartCoroutine(DestroyCrumb(eatState.length, duckController));
        }
    }

    private IEnumerator DestroyCrumb(float duration, Animator anim)
    {
        yield return new WaitForSeconds(duration);
        CrumbManager.instance.RemoveCrumb(this.gameObject);
        anim.SetTrigger("DoneEating");
    }
}
