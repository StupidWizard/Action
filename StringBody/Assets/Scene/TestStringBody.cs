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
	int N_CELL = 30;

	[SerializeField]
	float cellSize = 16;

	[SerializeField]
	List<Vector2> listPos = new List<Vector2>();

	[SerializeField]
	Vector2 direct = Vector2.up;		// default: move up

	[SerializeField]
	float velocityTurn = 270;

	[SerializeField]
	float velocityRun = 360;

	// Use this for initialization
	void Start () {
		Create();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDirect();

		// debug: only update position for listBody when pressing M
		if (Input.GetKey(KeyCode.M)) {
			UpdateMove();
		}
	}

	void UpdateMove() {
		var headPos = head.anchoredPosition;
		headPos += velocityRun * direct * Time.deltaTime;
		var offsetHead = headPos - listPos[0];
		while (offsetHead.magnitude > cellSize) {		// if device too slow, offset >> cellSize => use while to smooth.
			// update listPos
			Vector2 headNextPos = listPos[0] + offsetHead.normalized * cellSize;
			UpdateListPos(headNextPos);

			offsetHead = headPos - listPos[0];
		}

		ratio = offsetHead.magnitude / cellSize;
		// update body visible
		UpdateBodyVisible(ratio);
		head.anchoredPosition = headPos;
	}

	public float ratio;
	void UpdateBodyVisible(float ratio) {
		for (int i = 0; i < listBody.Count; i++) {
			// savedPos of body[i] is listPos[i+1].  listPos[0] is savedPos of Head.
			listBody[i].anchoredPosition = listPos[i] * ratio + listPos[i+1] * (1 - ratio);
		}
	}

	void UpdateListPos(Vector2 headNextPos) {
		for (int i = listPos.Count-1; i > 0; i--) {
			listPos[i] = listPos[i-1];
		}
		listPos[0] = headNextPos;
	}

	public float axisX;
	void UpdateDirect() {
		// get event of Left/Right key
		axisX = Input.GetAxis("Horizontal");

		// turn left/right. (Rotate vector_Direct with deltaAngle)
		float deltaAngle = axisX * velocityTurn * Time.deltaTime * Mathf.Deg2Rad;
		float x = direct.x * Mathf.Cos(deltaAngle) + direct.y * Mathf.Sin(deltaAngle);
		float y = direct.y * Mathf.Cos(deltaAngle) - direct.x * Mathf.Sin(deltaAngle);
		direct.Set(x, y);
		direct.Normalize();

		UpdateHeadVisibleRot();
	}

	void UpdateHeadVisibleRot() {
		float angle = Mathf.Acos(direct.x) * Mathf.Rad2Deg;
		if (direct.y < 0) {
			angle = -angle;
		}
		head.localRotation = Quaternion.Euler(0, 0, angle - 90);		// default direct of Head_Image is up (upRot = 90).
	}

	[ContextMenu("Create")]
	void Create() {
		Clear();

		// reset posHead
		head.anchoredPosition = Vector2.zero;
		head.localRotation = Quaternion.identity;
		listPos.Add(Vector2.zero);

		// create with number cell on body is N_CELL.
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
