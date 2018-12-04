using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntController : MonoBehaviour {

    //TODO: Handle Movement Differently than Point-And-Click
    [SerializeField]
    private Camera cam; //reference the camera component
    
    private NavMeshAgent agent; //referencing the nav mesh agent component

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //initialize nave mesh agent component
    }

    // Update is called once per frame
    void Update () 
	{
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
	}
}
