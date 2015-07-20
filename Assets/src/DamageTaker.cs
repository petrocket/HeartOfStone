using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ParticleSystem))]

public class DamageTaker : MonoBehaviour {

	public float health = 100.0f;
	public float initialHealth = 100.0f;
	public float maxHealth = 200.0f;

	private ParticleSystem explosion;

	// Use this for initialization
	void Start () {
		explosion = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(float amount) {
		if (health > 0.0f) {
			if (health - amount <= 0.0f) {
				Die ();
			} else {
				health -= amount;
				IndicateDamage ();
			}
		}
	}

	public void IndicateDamage() {
		// @TODO
	}

	public void Die() {
		health = 0.0f;
		GetComponent<Renderer> ().enabled = false;
		ColorChanger colorChanger = GetComponent<ColorChanger> ();
		if (colorChanger) {
			explosion.startColor = colorChanger.color;
		}
		explosion.Play();
	}

	public void Reset() {
		GetComponent<Renderer> ().enabled = true;
		health = initialHealth;
	}
}