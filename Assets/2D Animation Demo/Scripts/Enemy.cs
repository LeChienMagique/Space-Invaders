using UnityEngine;

public class Enemy: MonoBehaviour {
	public GameObject BulletPrefab;
	public Transform  ShootOffsetTransform;
	public int        Value;

	public void Fire() {
		Instantiate(BulletPrefab, ShootOffsetTransform.position, Quaternion.identity);
	}

	public void Kill() {
		ScoreController.Instance.AddScore(Value);
		EnemyManager.Instance.Enemies.Remove(gameObject);
		Destroy(gameObject);
	}
	
	private void OnTriggerEnter(Collider collision) {

	}
}