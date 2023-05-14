using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager: MonoBehaviour {
	public GameObject Enemy1Prefab;
	public GameObject Enemy2Prefab;
	public GameObject Enemy3Prefab;
	public GameObject Enemy4Prefab;

	[Range(1, 50)] public int   EnemiesPerRow;
	[Range(0, 10)] public float EnemyHorizontalSpacing;
	[Range(0, 10)] public float EnemyVerticalSpacing;

	public Transform CenterPoint;

	public  float BaseMoveTime;
	private float moveTimePerEnemy;
	private float moveTime;
	private int   direction = 1;
	public  float MoveDistance;

	public  int   MinShootInterval;
	public  int   MaxShootInterval;
	private float shootTime;

	public Transform LeftBound;
	public Transform RightBound;

	public List<GameObject> Enemies = new();

	public static EnemyManager Instance;

	private void Start() {
		Instance = this;
		SpawnEnemies();
		moveTimePerEnemy = BaseMoveTime / Enemies.Count;
		shootTime        = Random.Range(MinShootInterval, MaxShootInterval);
	}

	public void SpawnEnemies() {
		GameObject[] enemiesPrefab = {Enemy1Prefab, Enemy2Prefab, Enemy3Prefab, Enemy4Prefab};
		float        minY          = CenterPoint.position.y - ((float) enemiesPrefab.Length / 2) * EnemyVerticalSpacing;
		for (int i = 0 ; i < enemiesPrefab.Length ; i++) {
			float y = minY + (i * EnemyVerticalSpacing);
			SpawnRow(enemiesPrefab[i], y);
		}
	}

	private void SpawnRow(GameObject enemyPrefab, float y) {
		float minX = CenterPoint.position.x - ((float) EnemiesPerRow / 2) * EnemyHorizontalSpacing;
		for (int i = 0 ; i < EnemiesPerRow ; i++) {
			float x = minX + (i * EnemyHorizontalSpacing);
			Enemies.Add(Instantiate(enemyPrefab, new Vector3(x, y), Quaternion.identity));
		}
	}

	private float GetMaxRightEnemy() {
		float maxX = float.MinValue;
		foreach (GameObject enemy in Enemies) {
			if (enemy.transform.position.x > maxX) {
				maxX = enemy.transform.position.x;
			}
		}

		return maxX;
	}

	private float GetMaxLeftEnemy() {
		float minX = float.MaxValue;
		foreach (GameObject enemy in Enemies) {
			if (enemy.transform.position.x < minX) {
				minX = enemy.transform.position.x;
			}
		}

		return minX;
	}

	private void MoveDown() {
		foreach (GameObject enemy in Enemies) {
			enemy.transform.Translate(Vector3.down * MoveDistance);
		}
	}

	private void MoveRight() {
		float maxEnemyPos = GetMaxRightEnemy();
		if (maxEnemyPos + MoveDistance > RightBound.position.x) {
			direction *= -1;
			MoveLeft();
			MoveDown();
			return;
		}

		foreach (GameObject enemy in Enemies) {
			enemy.transform.Translate(Vector3.right * MoveDistance);
		}
	}

	private void MoveLeft() {
		float maxEnemyPos = GetMaxLeftEnemy();
		if (maxEnemyPos - MoveDistance < LeftBound.position.x) {
			direction *= -1;
			MoveRight();
			MoveDown();
			return;
		}

		foreach (GameObject enemy in Enemies) {
			enemy.transform.Translate(Vector3.left * MoveDistance);
		}
	}

	private void Fire() {
		float minY = float.MaxValue;
		foreach (GameObject enemy in Enemies) {
			if (enemy.transform.position.y < minY) {
				minY = enemy.transform.position.y;
			}
		}

		List<GameObject> possibleShooter = new();
		foreach (GameObject enemy in Enemies) {
			if (Math.Abs(enemy.transform.position.y - minY) < 0.1f) {
				possibleShooter.Add(enemy);
			}
		}

		GameObject shooter = possibleShooter[Random.Range(0, possibleShooter.Count)];
		shooter.GetComponent<Enemy>().Fire();
		shootTime = Random.Range(MinShootInterval, MaxShootInterval);
	}

	private void Update() {
		if (GameManager.Instance.GameEnded) {
			return;
		}

		if (Enemies.Count == 0) {
			GameManager.Instance.Win();
		}
		
		shootTime -= Time.deltaTime;
		moveTime  -= Time.deltaTime;

		if (shootTime <= 0) {
			Fire();
		}

		if (moveTime <= 0) {
			if (direction == 1) {
				MoveRight();
			}
			else {
				MoveLeft();
			}
			moveTime = moveTimePerEnemy * Enemies.Count;
		}


		if (Input.GetKeyDown(KeyCode.F)) {
			Fire();
		}
	}
}