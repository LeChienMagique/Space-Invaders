using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager: MonoBehaviour {
	public static GameManager     Instance;
	public        TextMeshProUGUI GameOverText;
	public        TextMeshProUGUI VictoryText;
	public        bool            GameEnded;
	public        Transform       SpawnPoint;
	public        GameObject      Player;

	public List<GameObject> Barriers;

	void Start() {
		Instance = this;
	}

	public void Lose() {
		GameEnded = true;
		GameOverText.gameObject.SetActive(true);
	}

	public void Win() {
		GameEnded = true;
		VictoryText.gameObject.SetActive(true);
	}

	private void Update() {
		if (GameEnded && Input.GetKeyDown(KeyCode.R)) {
			VictoryText.gameObject.SetActive(false);
			GameOverText.gameObject.SetActive(false);
			
			EnemyManager.Instance.Enemies.ForEach(e => Destroy(e));
			EnemyManager.Instance.Enemies.Clear();
			EnemyManager.Instance.SpawnEnemies();

			GameEnded = false;

			Player.gameObject.SetActive(true);
			Player.transform.position = SpawnPoint.position;
			foreach (GameObject barrier in Barriers) {
				barrier.GetComponent<Barrier>().Revive();
			}

			ScoreController.Instance.ResetScore();
		}
	}
}