using UnityEngine;
using System.Collections;
using LitJson;
using System;

[Serializable]
public class TestForm {
	public int number;
	public string text;

	public LowerTest[] lowerTest;
}

[Serializable]
public class LowerTest {
	public int lowerLevel;
	public string lowerName;
}

public class TestJson : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T)) {
			Test();
		}
	}

	void Test() {
		TestForm testForm = new TestForm();
		testForm.number = 123;
		testForm.text = "456";

		testForm.lowerTest = new LowerTest[3];

		LowerTest lower = new LowerTest();
		lower.lowerLevel = 21;
		lower.lowerName = "lower";
		testForm.lowerTest[0] = lower;
		testForm.lowerTest[2] = lower;

		string json = JsonMapper.ToJson(testForm);
		Debug.LogError("Json: " + json);

		Debug.LogError("");

		TestForm result = JsonMapper.ToObject<TestForm>(json);
		Debug.LogError("Number = " + result.number + "   text = " + result.text);
		Debug.LogError("Lower[0]= " + JsonMapper.ToJson(result.lowerTest[0]));
		Debug.LogError("Lower[1]= " + result.lowerTest[1]);
		Debug.LogError("Lower[2]= " + JsonMapper.ToJson(result.lowerTest[0]));
	}
}
