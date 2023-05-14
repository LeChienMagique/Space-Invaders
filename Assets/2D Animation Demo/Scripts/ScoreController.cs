using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController: MonoBehaviour {
	public TextMeshProUGUI ScoreText;
	public TextMeshProUGUI HiScoreText;

	private int score;
	private int hiScore;
	
	public static ScoreController Instance;

	void Start() {
		Instance = this;
	}

	public void ResetScore() {
		if (score > hiScore) {
			hiScore          = score;
			HiScoreText.text = $"HI-SCORE\n{hiScore:0000}";
		}
		
		score          = 0;
		ScoreText.text = $"Score <1>\n{score:0000}";
	}
	
	public void AddScore(int amount) {
		score          += amount;
		ScoreText.text =  $"Score <1>\n{score:0000}";
	}
	
}