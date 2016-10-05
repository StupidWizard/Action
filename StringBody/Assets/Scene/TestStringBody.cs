using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestStringBody : MonoBehaviour {

	[SerializeField]
	RectTransform bodyPrefab;

	[SerializeField]
	RectTransform bodyRoot;

	[SerializeField]
	RectTransform head;

	[SerializeField]
	List<RectTransform> listBody = new List<RectTransform>();

	[SerializeField]
	int N_CELL = 0;

	[SerializeField]
	float cellSize = 16;

	[SerializeField]
	List<Vector2> listPos = new List<Vector2>();

	[SerializeField]
	Vector2 direct = Vector2.up;

	[SerializeField]
	float velocityTurn = 30;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDirect();

		UpdatePos();
	}

	void UpdatePos() {
		var headPos = head.anchoredPosition;

	}

	public float axisX;
	void UpdateDirect() {
		axisX = Input.GetAxis("Horizontal");

		float deltaAngle = axisX * velocityTurn * Time.deltaTime * Mathf.Deg2Rad;
		float x = direct.x * Mathf.Cos(deltaAngle) + direct.y * Mathf.Sin(deltaAngle);
		float y = direct.y * Mathf.Cos(deltaAngle) - direct.x * Mathf.Sin(deltaAngle);
		direct.Set(x, y);
		direct.Normalize();

		float angle = Mathf.Acos(direct.x) * Mathf.Rad2Deg;
		if (direct.y < 0) {
			angle = -angle;
		}
		head.localRotation = Quaternion.Euler(0, 0, angle - 90);
	}

	[ContextMenu("Create")]
	void Create() {
		Clear();

		// reset posHead
		head.anchoredPosition = Vector2.zero;
		head.localRotation = Quaternion.identity;
		listPos.Add(Vector2.zero);

		// create by N_CELL
		for (int i = N_CELL; i > 0; i--) {
			RectTransform bodyCell = Instantiate(bodyPrefab);
			listBody.Insert(0, bodyCell);
			bodyCell.parent = bodyRoot;
			bodyCell.localScale = Vector3.one;
			bodyCell.rotation = Quaternion.identity;
			bodyCell.anchoredPosition = new Vector2(0, -cellSize * i);
			listPos.Insert(1, bodyCell.anchoredPosition);
		}
	}

	void Clear() {
		listPos.Clear();
		foreach (RectTransform rectTransform in listBody) {
			GameObject.Destroy(rectTransform.gameObject);
		}
		listBody.Clear();
	}
}
