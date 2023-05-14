using UnityEngine;

public class Barrier: MonoBehaviour {
	public  int MaxHealth;
	private int health;

	void Start() {
		health = MaxHealth;
	}

	public void Revive() {
		health = MaxHealth;
		Color color = GetComponent<SpriteRenderer>().color;
		float alpha = (float) health / MaxHealth;
		color.a                              = alpha;
		GetComponent<SpriteRenderer>().color = color;
	}

	public void Damage(int amount) {
		health -= amount;
		if (health <= 0) {
			gameObject.SetActive(false);
			return;
		}

		Color color = GetComponent<SpriteRenderer>().color;
		float alpha = (float) health / MaxHealth;
		color.a                              = alpha;
		GetComponent<SpriteRenderer>().color = color;
	}
}