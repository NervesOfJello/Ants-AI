using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHeightRandomizer : MonoBehaviour {

    [SerializeField]
    private float minHeight;
    [SerializeField]
    private float maxHeight;

	// Use this for initialization
	void Start () 
	{
        this.transform.localScale = new Vector3(transform.localScale.x, Random.Range(minHeight, maxHeight), transform.localScale.z);
	}
}
