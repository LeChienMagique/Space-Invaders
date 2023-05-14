using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet: MonoBehaviour {
	public float Speed = -5;

	private void Start() {
		Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
		velocity.y                           = Speed;
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			other.GetComponent<Player>().Kill();
			Destroy(gameObject);
		}

		if (other.gameObject.CompareTag("Barrier")) {
			other.GetComponent<Barrier>().Damage(10);
			Destroy(gameObject);
		}
	}
}