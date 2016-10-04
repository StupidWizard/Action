using UnityEngine;
using System.Collections;

public class TestTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
//			Debug.LogError("Touch down, pos = " + Input.mousePosition);

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			//check if the ray hits a physic collider
			RaycastHit hit; //a local variable that will receive the hit info from the Raycast call below
			if (Physics.Raycast(ray,out hit)) {
				Debug.LogError("TargetName = " + hit.collider.gameObject.name);
			}

//			transform.localRotation = Quaternion.Euler(ray.direction);
//			transform.localPosition = ray.origin;
			transform.position = ray.origin;
			transform.LookAt(transform.position + ray.direction);
		}
	}
}
