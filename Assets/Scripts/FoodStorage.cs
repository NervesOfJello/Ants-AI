using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorage : MonoBehaviour {

    [SerializeField]
    private float foodCount; //how much food the object holds
    private float sizeChangeRatio = 0.5f; //how much the scale changes by

    //get set for foodCount
    public float FoodCount
    {
        get { return foodCount; }

        set
        {
            foodCount = value;
            if (foodCount <= 0)
            {
                Destroy(this.gameObject);
            }
            updateFoodSize();
        }

    }
    
    private void updateFoodSize()
    {
        this.transform.localScale = new Vector3(transform.localScale.x, foodCount * sizeChangeRatio, transform.localScale.z);
    }

    private void Start()
    {
        updateFoodSize();
    }
}
