using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {
	[System.Serializable]
	public class Car {
		[SerializeField, Range(0, 10)] float speed;
		[SerializeField] int height;

		public float Speed {
			get { return speed; }
		}
	}
		
	enum CarType {
		BUS = 0,
		TRAIN = 1,
		CAR = 2
	}

	enum Kind {
		ENEMY = 0,
		FRIEND = 1,
		NEUTRAL = 2,
		PLAYER = 3
	}

	[System.Serializable] class Cars : TypedMap<CarType, Car>{}
	[System.Serializable] class Health : TypedMap<Kind, float>{}

	[SerializeField] Cars cars;
	[SerializeField] Health maxHealth;

	void Start() {
		Debug.Log("max health of FRIEND: " + maxHealth[Kind.FRIEND]);
		Debug.Log("max speed of TRAIN: " + cars[CarType.TRAIN].Speed);
	}
}
