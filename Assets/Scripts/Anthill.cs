using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anthill : MonoBehaviour {

    [SerializeField]
    private AntController antPrefab;
    [SerializeField]
    private int populationSize;
    [SerializeField]
    private int foodToSpawn;

    private int foodCount;

	// Use this for initialization
	void Start () 
	{
        for (int i = 0; i < populationSize; i++)
        {
            Instantiate(antPrefab, transform.position, Quaternion.identity).GetComponent<AntController>().homeTransform = this.transform;
        }
	}
	
	public void AddFood()
    {
        foodCount++;
        if (foodCount == foodToSpawn)
        {
            foodCount = 0;
            Debug.Log("Swaned New Ant");
            Instantiate(antPrefab, transform.position, Quaternion.identity).GetComponent<AntController>().homeTransform = this.transform;
        }
    }
}
