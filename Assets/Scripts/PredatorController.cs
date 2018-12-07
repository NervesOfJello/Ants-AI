using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorController : MonoBehaviour {

    #region Variables

    [SerializeField]
    private float lastPositionTime; //how long ago it stores the last position
    [SerializeField]
    private float pheremoneDropInterval; //time between pheremone drops
    [SerializeField]
    private float searchRange;
    [SerializeField]
    private float destinationDetectionRange = 0.1f;
    [SerializeField]
    private float foodConsumption;

    //TODO: SET HOME TRANSFORM IN CODE ON CREATION
    [SerializeField]
    public Transform homeTransform; //home position

    [SerializeField]
    private Transform PheremonePrefab;

    //variables
    private Vector3 pastPosition; //stores the objects past position
    private bool hasFood = false; //determines the state of the ant whether searching for food or not searching for food
    private bool canDropPheremone = true; //determines whether the ant can drop a pheremone yet
    private bool isOnPheremoneCooldown; //determines whether the ant has a searching destination

    //component variables
    private UnityEngine.AI.NavMeshAgent agent; //referencing the nav mesh agent component
    [SerializeField]
    private MeshRenderer foodMeshRenderer; //references the mesh renderer of the food
    private TrailRenderer tailComponent; //references the tail renderer component
    #endregion

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); //initialize nave mesh agent component
        GetNewSearchingDestination();
        //set an initial search destination
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LastPositionCoroutine()); //starts the setting of the last position
        CheckState(); //updates the behavior state of the
    }

    void CheckState()
    {
        if (hasFood)
        {
            //Debug.Log("Running Homecoming Behavior");
            //NAVIGATION TO ANTHILL CODE
            agent.SetDestination(homeTransform.position);
            StartCoroutine(PheremoneCooldownCoroutine());
        }
        else
        {
            //Debug.Log("Running Searching Behavior");
            //If it reaches its destination
            if (Mathf.Abs(this.transform.position.x - agent.destination.x) <= destinationDetectionRange &&
                Mathf.Abs(this.transform.position.z - agent.destination.z) <= destinationDetectionRange)
            {
                //hasSearchingDestination = true;
                GetNewSearchingDestination();
                //StartCoroutine(SearchDelayCoroutine());
                //Debug.Log("Set new Destination:" + agent.destination);
            }
        }
    }

    private void GetNewSearchingDestination()
    {
        //Debug.Log("Getting a new Search Destination");
        agent.SetDestination(new Vector3
            (
            Random.Range((this.transform.position.x - searchRange), (this.transform.position.x + searchRange)), //sets random x within range
            this.transform.position.y, //keeps y (we don't want floating)
            Random.Range((this.transform.position.z - searchRange), (this.transform.position.z + searchRange)) //sets random z within range
            ));
    }

    //Collision Functions
    private void OnTriggerEnter(Collider other)
    {
        //when it finds food
        if (!hasFood && other.CompareTag("Food") && other.GetComponent<FoodStorage>() != null)
        {
            other.GetComponent<FoodStorage>().FoodCount -= foodConsumption;
            hasFood = true;
            foodMeshRenderer.enabled = true;
        }

        //when it gets home with food
        if (hasFood && other.CompareTag("Home"))
        {
            SpawnPheremone();
            hasFood = false;
            other.GetComponent<Anthill>().AddFood();
            foodMeshRenderer.enabled = false;
        }

        //when, while searching for food, encounters a pheremone
        if (!hasFood && other.GetComponent<PheremoneInfo>() != null)
        {
            agent.SetDestination(other.GetComponent<PheremoneInfo>().guidePosition);
        }
    }

    void SpawnPheremone() //instantiates pheremone objects
    {
        //Make a pheremone object and also set its guide position to this ant's past position
        Instantiate(PheremonePrefab, this.transform.position, Quaternion.identity).GetComponent<PheremoneInfo>().guidePosition = pastPosition;
    }

    #region Coroutines
    //Coroutines
    //Drops pheremones at a give interval of time
    private IEnumerator PheremoneDropCoroutine()
    {
        while (hasFood && canDropPheremone)
        {
            canDropPheremone = false;
            SpawnPheremone();
            yield return new WaitForSeconds(pheremoneDropInterval);
            canDropPheremone = true;
        }
    }

    //stores the position of this object five seconds ago
    private IEnumerator LastPositionCoroutine()
    {
        Vector3 temp = transform.position;
        yield return new WaitForSeconds(lastPositionTime);
        pastPosition = temp;
    }

    //waits and sets new destination at a given interval
    private IEnumerator PheremoneCooldownCoroutine()
    {
        yield return new WaitForSeconds(lastPositionTime);
        StartCoroutine(PheremoneDropCoroutine());
    }
    #endregion
}
