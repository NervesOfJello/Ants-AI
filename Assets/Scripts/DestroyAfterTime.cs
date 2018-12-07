using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {


    [SerializeField]
    private float timeToDestroy;
	// Use this for initialization
	void Start () 
	{
        Destroy(this.gameObject, timeToDestroy);
	}
}
