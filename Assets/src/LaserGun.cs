using UnityEngine;
using System.Collections;

public class LaserGun : MonoBehaviour {
	public GameObject Laser;
	public float fireRate = 0.5f;

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
				laser.transform.position = transform.position + transform.forward * 2.0f;
				laser.transform.rotation = transform.rotation;
				laser.GetComponent<LaserProperties>().fireTime = Time.time;
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
