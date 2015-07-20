using UnityEngine;
using System.Collections;

public class TimedBehaviour : MonoBehaviour {
	public bool complete = false;
	public bool loop = true;
	public float progress = 0.0f;

	protected bool paused = false;
	protected float startTime;
	protected float duration = 2.0f;


	// Use this for initialization
	public virtual void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (!complete) {
			progress = Mathf.Clamp((Time.time - startTime) / duration, 0.0f, 1.0f);
			if ((Time.time - startTime) > duration) {
				if (loop) {
					OnLoop ();
				}
				else {
					complete = true;
				}
			}
		}
	}

	public virtual void OnLoop() {
		startTime = Time.time;
	}
}
