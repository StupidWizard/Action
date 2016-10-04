using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField]
	Vector2 sizeDefault = new Vector2(1280, 720);

	[SerializeField]
	ScrollRect scrollRect;		// scroll screen to update camera position

	[SerializeField]
	Camera mainCamera;

	public float _cameraX = 0;

	float cameraX {
		get {
			return _cameraX;
		}
		set {
			if (_cameraX != value) {
				_cameraX = value;
				var cameraPos = mainCamera.transform.position;
				cameraPos.x = _cameraX;
				mainCamera.transform.position = cameraPos;
			}
		}
	}

	public float cameraWidth = 1280;

	// Use this for initialization
	void Start () {
		cameraWidth = Screen.width * sizeDefault.y / Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		cameraX = -scrollRect.content.anchoredPosition.x + cameraWidth/2;
	}
}
