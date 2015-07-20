using UnityEngine;
using System.Collections;


//[RequireComponent (typeof (LineRenderer))]
public class PlayerController : MonoBehaviour {

	public GameObject target;
	public GameObject gun;

	private LaserGun laserGun;
	private DamageTaker damageTaker;
	private CameraTracker cameraTracker;
	private GameRules gameRules;

	// these are the max extents the player can drift from 0,0,0
	public Vector3 limits = new Vector3(3.0f,3.0f,3.0f);
	public Vector2 targetLimits = new Vector2 (3.0f, 3.0f);
	public Vector3 lookAt = new Vector3(0.0f, 10.0f, 0.0f);
	public float sensitivity = 2.0f;
	public float targetOffset = 30.0f;

	//public float laserWidth = 1.0f;
	//public Color laserColor = Color.red;
	public float laserMaxLength = 50.0f;
	public int laserNumSegments = 2;

	//private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		//lineRenderer = GetComponent<LineRenderer> ();
		//lineRenderer.SetVertexCount (laserNumSegments);
		//lineRenderer.SetPosition (0, Vector3.zero);
	
		GameObject newGun = (GameObject)Instantiate (gun, Vector3.zero, Quaternion.identity);
		newGun.transform.parent = transform;
		newGun.transform.localPosition = Vector3.zero;
		newGun.transform.localRotation = Quaternion.identity;
		laserGun = newGun.GetComponent<LaserGun> ();

		damageTaker = GetComponent<DamageTaker> ();
		cameraTracker = Camera.main.GetComponent<CameraTracker> ();
		gameRules = Camera.main.GetComponent<GameRules> ();
	}
	
	// Update is called once per frame
	void Update () {
		float dx = Input.GetAxis("Horizontal") * sensitivity * Time.deltaTime;
		float dy = Input.GetAxis("Vertical") * sensitivity * Time.deltaTime;

		transform.localPosition = new Vector3 (
			Mathf.Clamp (transform.localPosition.x + dx, -limits.x, limits.x),
			Mathf.Clamp (transform.localPosition.y + dy, -limits.y, limits.y),
			transform.localPosition.z);

		Vector3 mousepos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetOffset));
		target.transform.position = mousepos;
		transform.LookAt (target.transform, Vector3.up);


		if (Input.GetButton("Fire1") && damageTaker.health > 0.0f) {
			laserGun.Fire();
		}
	}
	/*
	void RenderLaser()	{

		UpdateLaserLength ();

		//lineRenderer.SetColors (laserColor, laserColor);
		Vector3 vertexPos = Vector3.zero;
		float dz = laserMaxLength / (float)laserNumSegments;
		for (int i = 1; i < laserNumSegments; ++i) {
			vertexPos.z = dz + ((float)i * dz);
			lineRenderer.SetPosition(i,vertexPos);
		}
	}
	*/

	void OnTriggerEnter(Collider other) {
		if (other.tag != "Player" && other.tag != "Laser") {
			//Debug.Log ("Collided");
			if (damageTaker.health > 0.0f) {

				DamageTaker otherDamageTaker = other.GetComponent<DamageTaker>();
				if(otherDamageTaker && otherDamageTaker.health > 0.0f) {
					otherDamageTaker.TakeDamage (1000.0f);

					damageTaker.TakeDamage(10.0f);
					cameraTracker.shake = 1.0f;
					
					if (damageTaker.health <= 0.0f) {
						gameRules.OnGameOver();
					}

				}
			}
		}

	}
	/*
	void UpdateLaserLength() {
		RaycastHit[] hit;
		hit = Physics.RaycastAll (transform.position, transform.forward, laserMaxLength );
		int i = 0;
		float closest = -1.0f;
		int closestIndex = -1;
		while (i < hit.Length) {
			// TODO damage!
			if ( (closest < 0.0f || hit[i].distance < closest) && hit[i].collider.tag != "player") {
				closest = hit[i].distance;
				closestIndex = i;
			}
			i++;
		}

		if (closestIndex > -1 && closestIndex < hit.Length) {
			DamageTaker damageTaker = hit[closestIndex].collider.GetComponent<DamageTaker>();
			//Debug.Log ("Hit " + hit[closestIndex].collider.GetType());
			if (damageTaker != null) {
				//Debug.Log ("Taking Damage");
				damageTaker.TakeDamage(1000.0f);
			}
		}
	}
	*/

}
