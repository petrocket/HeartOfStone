using UnityEngine;
using System.Collections;

public class ColorChanger : TimedBehaviour {

	public Renderer gameObjectRenderer;
	public DamageTaker damageTaker;
	public Gradient colors = null;
	public Color color;
	public bool timeBased = true;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		gameObjectRenderer = GetComponent<Renderer> ();
		damageTaker = GetComponent<DamageTaker> ();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();

		if (colors != null) {
			if (timeBased) {
				color = colors.Evaluate (progress);
			}
			else {
				color = colors.Evaluate (Mathf.Clamp (damageTaker.health / damageTaker.maxHealth, 0.0f, 1.0f));
			}

			gameObjectRenderer.material.SetColor ("_Color",color);
			gameObjectRenderer.material.SetColor("_EmissionColor", color);
		}
	}
}
