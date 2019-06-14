using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckManager : MonoBehaviour {
    public static DuckManager instance = null;
    private List<GameObject> ducks;
    public float duckSpeed;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        ducks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Duck"));

    }

    GameObject FindClosestCrumb(GameObject duck)
    {
        if(CrumbManager.instance.GetCrumbs().Length == 0)
        {
            return null;
        }
        Vector3 duckPos = duck.transform.position;
        GameObject min = CrumbManager.instance.GetCrumbs()[0];
        float minDistance = 1000;
        foreach (GameObject c in CrumbManager.instance.GetCrumbs())
        {
            Vector3 cPos = c.transform.position;
            float distance = Vector3.Distance(duckPos, cPos);
            if (distance < minDistance){
                minDistance = distance;
                min = c;
            }
        }
        return min;
    }
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject d in ducks)
        {
            GameObject c = FindClosestCrumb(d);
            d.GetComponent<DuckBehaviour>().SetClosestCrumb(c);

        }
	}
}
