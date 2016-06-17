using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DoJumpExample : MonoBehaviour {

	[SerializeField]
	Transform target;

	[SerializeField]
	float power = 1.0f;

	[SerializeField]
	int numberJump = 1;

	[SerializeField]
	float duration = 1.0f;

	[SerializeField]
	bool snapping = false;


	[SerializeField]
	Ease type = Ease.Linear;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) {
			transform.DOJump(target.position, power, numberJump, duration, snapping).SetEase(type);
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			transform.DOKill();
			transform.position = Vector3.zero;
		}

	}


}
