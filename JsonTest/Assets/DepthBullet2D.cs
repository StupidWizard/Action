using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Collider2D))]
public class DepthBullet2D : MonoBehaviour {

	[SerializeField]
	float timeLife = 0.1f;

	List<GameObject> listCollider = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timeLife > 0) {
			timeLife -= Time.deltaTime;
			if (timeLife <= 0) {
				AutoRemove();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (timeLife > 0) {
			AddCollider(other.gameObject);
		}
	}

	void AddCollider(GameObject other) {
		foreach (GameObject obj in listCollider) {
			if (obj == other) {
				return;
			}
		}
		if (other.GetComponent<DepthBullet2D>() != null) {
			return;
		}
		listCollider.Add(other);
	}

	void AutoRemove() {
		// show all collider
		string log = "collider with:";
		foreach(GameObject obj in listCollider) {
			log += string.Format(" [{0}]", obj.name);
		}

		Debug.LogError(log);

//		GameObject.Destroy(gameObject);
	}
}
