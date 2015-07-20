using UnityEngine;
using System.Collections;

public class ToRandomLocation : TimedBehaviour {

	public float radius = 5;
	public Vector3 origin;
	public Vector3 destination;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		PickRandomDestination ();

		if (origin.sqrMagnitude < 10.0) {
			transform.localPosition = destination;
			PickRandomDestination ();
		}
	}

	void PickRandomDestination() {
		origin = transform.localPosition;
		destination = new Vector3(Random.Range(-1.0f,1.0f),Random.Range(-1.0f, 1.0f),0);
		destination.Normalize();
		destination *= radius;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		float dt = Mathf.Clamp((Time.time - startTime) / duration, 0.0f, 1.0f);
		transform.localPosition = Vector3.Lerp (origin, destination, Mathf.SmoothStep (0.0f, 1.0f, dt));
	}

	public override void OnLoop() {
		base.OnLoop ();
		PickRandomDestination ();
	}
}
