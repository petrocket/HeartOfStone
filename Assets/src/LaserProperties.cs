using UnityEngine;
using System.Collections;

public class LaserProperties : MonoBehaviour {
	public float fireTime = -1.0f;
	public float speed = 100.0f;
	public float life = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeSelf) {
			if(Time.time > fireTime + life) {
				gameObject.SetActive(false);
			}

			//transform.position += transform.forward * speed * Time.deltaTime;
			transform.Translate (Vector3.forward * Time.deltaTime * speed);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag != "Player") {
			//Debug.Log ("Laser collision " + other.tag);
			DamageTaker damageTaker = other.GetComponent<DamageTaker> ();
			//Debug.Log ("Hit " + hit[closestIndex].collider.GetType());
			if (damageTaker != null) {
				//Debug.Log ("Taking Damage");
				damageTaker.TakeDamage (1000.0f);
				gameObject.SetActive (false);
			}
		}
	}
}
