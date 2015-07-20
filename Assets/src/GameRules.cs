using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameRules : MonoBehaviour {

	public float trackRotateSpeed = 30.0f;
	public GameObject ring = null;
	public GameObject shape1 = null;
	public GameObject ship;

	public Canvas gameOverCanvas;
	public Canvas introCanvas;

	public bool paused = true;

	public float trackRadius = 50.0f;
	public uint numRings = 8;
	private GameObject[] rings;

	private GameObject trackOrigin;
	private GameObject track;

	// Use this for initialization
	public void Start () {

		if (gameOverCanvas) {
			//gameOverCanvas.enabled = false;
		}
		trackOrigin = new GameObject ();

		track = new GameObject ();
		track.transform.parent = trackOrigin.transform;
		track.transform.localPosition = new Vector3(trackRadius, 0, 0);

		rings = new GameObject[numRings];
		for (int i = 0; i < numRings; ++i) {
			// track center
			GameObject ringRoot = new GameObject();
			ringRoot.transform.parent = track.transform;
			ringRoot.transform.localPosition = Vector3.zero;

			// offset by radius
			GameObject ringOffsetRoot = new GameObject();
			ringOffsetRoot.transform.parent = ringRoot.transform;
			ringOffsetRoot.transform.localPosition = new Vector3(-trackRadius,0,0);

			GameObject newRing = (GameObject)Instantiate (ring, new Vector3 (0, 0, 0), Quaternion.identity);
			newRing.transform.parent = ringOffsetRoot.transform;
			newRing.transform.localPosition = Vector3.zero;

			//Debug.Log ("Adding shape1 to shapefactory");
			newRing.GetComponent<ShapeFactory>().shape1 = shape1;

			ringRoot.transform.Rotate(0,(360 / numRings) * i,0);
			rings[i] = newRing;
		}

		Time.timeScale = 0;

		OnPlay ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused) {
			//trackOrigin.transform.Rotate (Vector3.forward * (Time.deltaTime * trackRotateSpeed));
			//track.transform.Rotate (Vector3.down * (Time.deltaTime * trackRotateSpeed));
		}

		if (Input.GetKey ("escape")) {
			Application.Quit ();
		}
	}

	public void Reset() {
		for (int i = 0; i < numRings; ++i) {
			rings[i].GetComponent<ShapeFactory>().Reset();
		}

		if (ship != null) {
			ship.GetComponent<DamageTaker> ().Reset ();
		}
		trackOrigin.transform.rotation = Quaternion.identity;
		track.transform.rotation = Quaternion.identity;

	}

	public void OnGameOver() {
		GetComponent<BlurOptimized>().enabled = true;
		gameOverCanvas.enabled = true;
		Cursor.visible = true;
	}

	public void OnPlay() {
		Reset ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;

		paused = false;
		Time.timeScale = 1;

		GetComponent<BlurOptimized>().enabled = false;
		gameOverCanvas.enabled = false;
		introCanvas.enabled = false;
	}

	public void OnQuit() {
		Application.Quit ();
	}
}
