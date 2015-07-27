using UnityEngine;
using System.Collections;

public class AITurret : MonoBehaviour {

	public GameObject gun;
	
	private LaserGun laserGun;
	private Transform playerTransform;
	private Renderer turretRenderer = null;

	// Use this for initialization
	void Start () {
		GameObject newGun = (GameObject)Instantiate (gun, Vector3.zero, Quaternion.identity);
		newGun.transform.parent = transform;
		newGun.transform.localPosition = Vector3.zero;
		newGun.transform.localRotation = Quaternion.identity;
		laserGun = newGun.GetComponent<LaserGun> ();
		laserGun.fireRate = 1.0f;
		laserGun.laserSpeed = 40.0f;
		laserGun.laserStartColor = new Color (0.0f, 0.0f, 1.0f);
		laserGun.laserEndColor = new Color (0.0f, 0.0f, 0.5f);
		laserGun.laserDamageAmount = 25.0f;

		// this is lame
		playerTransform = Camera.main.GetComponent<CameraTracker> ().target;

		turretRenderer = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (turretRenderer.enabled) {
			// look at the player and fire
			transform.LookAt (playerTransform);
			laserGun.Fire ();
		}
	}
}
