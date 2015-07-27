using UnityEngine;
using System.Collections;

public class LaserGun : MonoBehaviour {
	public GameObject Laser;
	public float fireRate = 0.5f;
	public float laserSpeed = 80.0f;
	public Color laserStartColor = Color.red;
	public Color laserEndColor = new Color(0.5f,0.0f,0.0f);
	public float laserDamageAmount = 1000.0f;

	private GameObject[] lasers;
	private int maxLasers = 10;
	private float nextFireTime = 0.0f;

	// Use this for initialization
	void Start () {
		nextFireTime = 0.0f;

		lasers = new GameObject[maxLasers];
		for (int i = 0; i < maxLasers; ++i) {
			lasers[i] = (GameObject)Instantiate(Laser,Vector3.zero,Quaternion.identity);
			lasers[i].SetActive(false);
			lasers[i].GetComponent<LaserProperties>().damageAmount = laserDamageAmount;

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire() {
		if (Time.time > nextFireTime) {
			//Debug.Log("Firing laser");
			nextFireTime = Time.time + fireRate;
			GameObject laser = GetLaser();
			if (laser != null) {
				laser.transform.position = transform.position + transform.forward * 4.0f;
				laser.transform.rotation = transform.rotation;
				laser.GetComponent<LaserProperties>().fireTime = Time.time;
				laser.GetComponent<LaserProperties>().speed = laserSpeed;
				laser.GetComponent<LineRenderer>().SetColors(laserStartColor, laserEndColor);
				laser.SetActive(true);
			}
			else {
				//Debug.Log("Laser not available");
			}
		}
	}

	private GameObject GetLaser() {
		GameObject laser = null;

		for (int i = 0; i < maxLasers; ++i) {
			if(lasers[i].activeSelf == false) {
				return lasers[i];
			}

			LaserProperties props = lasers[i].GetComponent<LaserProperties>();
			if(laser == null) {
				laser = lasers[i];
			}
			else if(props.fireTime < laser.GetComponent<LaserProperties>().fireTime) {
				laser = lasers[i];
			}

		}

		return laser;
	}
}
