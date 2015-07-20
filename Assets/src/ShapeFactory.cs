using UnityEngine;
using System.Collections;

public class ShapeFactory : MonoBehaviour {

	public GameObject shape1 = null;
	public uint maxNumShape1 = 10;

	public GameObject shape2 = null;
	public uint maxNumShape2 = 10;

	public GameObject shape3 = null;
	public uint maxNumShape3 = 10;

	private GameObject[][] shapes = new GameObject[3][];

	// Use this for initialization
	void Start () {
		CreateShapes ();
	}

	public void CreateShapes() {
		if (shape1) {
			CreateShapes(0,maxNumShape1,shape1);
		}
		if (shape2) {
			CreateShapes(1,maxNumShape2,shape2);
		}
		if (shape3) {
			CreateShapes(2,maxNumShape3,shape3);
		}
	}

	public void Reset() {
		if (shape1) {
			ResetShapes(0,maxNumShape1);
		}
		if (shape2) {
			ResetShapes(1,maxNumShape2);
		}
		if (shape3) {
			ResetShapes(2,maxNumShape3);
		}
	}

	private void ResetShapes(uint shapeIndex, uint numShapes) {
		for (uint i = 0; i < numShapes; ++i) {
			shapes[shapeIndex][i].GetComponent<DamageTaker> ().Reset();
		}
	}

	private void CreateShapes(uint shapeIndex, uint numShapes, GameObject shape) {
		shapes[shapeIndex] = new GameObject[numShapes];
		for (uint i = 0; i < numShapes; ++i) {
			shapes[shapeIndex][i] = CreateShape (shape,i);
		}
	}

	private GameObject CreateShape(GameObject shape, uint index) {
		GameObject newShape =(GameObject)Instantiate (shape, new Vector3 (0, 0, 0), Quaternion.identity);
		newShape.transform.parent = transform;
		newShape.transform.localPosition = Vector3.zero;
		newShape.transform.localRotation = Quaternion.identity;
		newShape.GetComponent<ShapeProperties> ().index = index;

		ColorChanger colorChanger = newShape.AddComponent<ColorChanger> ();
		Gradient gradient = new Gradient ();
		GradientColorKey[] colorKeys = new GradientColorKey[3];
		colorKeys[0].color = Color.red;
		colorKeys[0].time = 0.0f;
		colorKeys[1].color = Color.green;
		colorKeys[1].time = 0.5f;
		colorKeys[2].color = Color.red;
		colorKeys[2].time = 1.0f;
		gradient.colorKeys = colorKeys;
		colorChanger.colors = gradient;

		newShape.AddComponent<ToRandomLocation> ();

		return newShape;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
