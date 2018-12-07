using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AddObstacleOnClick : MonoBehaviour {

    private Camera cam; //reference the camera component

    [SerializeField]
    private NavMeshObstacle rockPrefab;
    [SerializeField]
    private FoodStorage foodPrefab;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Physics.Raycast(ray, out hit, 500);

            if (Physics.Raycast(ray, out hit, 500))
            {
                Instantiate(rockPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
