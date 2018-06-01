using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventoryObserver : InventoryObserver {

	// Use this for initialization
	void Awake () {
        FindObjectOfType<Services>().addService(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void onItemUsed(Item item)
    {
        
    }
}
