using UnityEngine;
using System.Collections;

public class ButtonListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick() {
		Debug.LogError("OnClick: " + gameObject.name);
	}
}
