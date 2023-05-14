using UnityEngine;

public class Player: MonoBehaviour {
	public GameObject BulletPrefab;
	public Transform  ShootOffsetTransform;
	public float      FireCooldown;

	public Transform LeftBound;
	public Transform RightBound;
	
	[Range(1, 10)] public float Speed;

	private Animator playerAnimator;

	private float fireCooldown;

	//-----------------------------------------------------------------------------
	void Start() {
		fireCooldown   = FireCooldown;
		playerAnimator = GetComponent<Animator>();
	}

	public void Kill() {
		gameObject.SetActive(false);
		GameManager.Instance.Lose();
	}

	//-----------------------------------------------------------------------------
	void Update() {
		if (GameManager.Instance.GameEnded) {
			return;
		}
		
		float axis = Input.GetAxisRaw("Horizontal");

		transform.Translate(Vector3.right * (axis * Time.deltaTime * Speed));
		if (transform.position.x > RightBound.position.x) {
			transform.position = new Vector3(RightBound.position.x, transform.position.y);
		}

		if (transform.position.x < LeftBound.position.x) {
			transform.position = new Vector3(LeftBound.position.x, transform.position.y);
		}

		fireCooldown -= Time.deltaTime;
		if (fireCooldown <= 0 && Input.GetKeyDown(KeyCode.Space)) {
			fireCooldown = FireCooldown;
			// todo - trigger a "shoot" on the animator
			GameObject shot = Instantiate(BulletPrefab, ShootOffsetTransform.position, Quaternion.identity);

			Destroy(shot, 3f);
		}
	}
}