using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin;
using HTC.UnityPlugin.Vive;

public class CrumbManager : MonoBehaviour
{
    public static CrumbManager instance = null;

    public GameObject crumbPrefab;
    private List<GameObject> crumbs;

    [SerializeField]
    private float launchForceVal;

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

        crumbs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Crumb"));
    }

    private void Update()
    {

    }

    public GameObject[] GetCrumbs()
    {
        return crumbs.ToArray();
    }

    public void RemoveCrumb(GameObject go)
    {
        crumbs.Remove(go);
        Destroy(go);
    }

    public void LaunchCrumb(GameObject controller)
    {
        GameObject newCrumb = GameObject.Instantiate(crumbPrefab, controller.transform.position, controller.transform.rotation);

        Rigidbody launch = newCrumb.GetComponent<Rigidbody>();
        launch.isKinematic = false;
        Vector3 launchForce = new Vector3(0f, 0f, launchForceVal);
        launch.AddRelativeForce(launchForce, ForceMode.Impulse);

        crumbs.Add(newCrumb);

    }
}