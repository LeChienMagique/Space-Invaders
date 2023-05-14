using System;
using UnityEngine;

public class Bullet: MonoBehaviour {
	public float Speed = 5;

	private void Start() {
		Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
		velocity.y                           = Speed;
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	private void Update() {
		// transform.Translate(Vector3.up * (Speed * Time.deltaTime));
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Enemy")) {
			other.GetComponent<Enemy>().Kill();
			Destroy(gameObject);
		}

		if (other.gameObject.CompareTag("Barrier")) {
			other.GetComponent<Barrier>().Damage(10);
			Destroy(gameObject);
		}
	}
}