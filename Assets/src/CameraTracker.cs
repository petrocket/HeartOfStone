using UnityEngine;
using System.Collections;

public class CameraTracker : MonoBehaviour {

	public Transform target;
	public float elasticity = 0.0f;
	public Vector3 offset = new Vector3(0.0f,2.5f,-9.0f);

	public float shake = 0.0f;
	public float shakeAmount = 0.4f;
	public float shakeDecFactor = 1.0f;

	// Use this for initialization
	void Start () {
		if (target) {
			transform.localPosition = target.localPosition + offset;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (target) {
			if (elasticity > 0.01f) {
				Vector3 delta = (target.localPosition + offset) - transform.localPosition;
				float distance = delta.magnitude;
				delta.Normalize();
				transform.localPosition = transform.localPosition + (delta * distance * elasticity * Time.deltaTime);
			}
			else {
				transform.localPosition = target.localPosition + offset;
			}

			if (shake > 0.0) {
				transform.localPosition = transform.localPosition + Random.insideUnitSphere * shakeAmount;
				shake -= Time.deltaTime * shakeDecFactor;
			}
			else {
				shake = 0.0f;
			}

		}
	}
}
