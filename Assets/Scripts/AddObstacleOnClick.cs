using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AddObstacleOnClick : MonoBehaviour {

    //TODO: Handle Movement Differently than Point-And-Click
    [SerializeField]
    private Camera cam; //reference the camera component

    [SerializeField]
    private NavMeshObstacle obstaclePrefab;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(obstaclePrefab, hit.transform);
            }
        }
    }
}
