using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public float Xmin;
	public float Ymin;
	public float Xmax;
	public float Ymax;

	float shakeTimer;
	float shakeAmount;
	bool shaking = false;

	private GameObject stephen;
	void Start()
	{
		stephen = GameObject.Find ("Stephen");
	}
	void Update()
	{
		Shake ();
	}
	void LateUpdate()
	{
		if (stephen != null)
		if (shaking == false)
		this.transform.position = new Vector3 (Mathf.Clamp(stephen.transform.position.x, Xmin,Xmax), Mathf.Clamp(stephen.transform.position.y,Ymin,Ymax), this.transform.position.z);
	}
	void Shake()
	{
		if (shakeTimer > 0) 
		{
			Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
			this.transform.position = new Vector3 (Mathf.Clamp(stephen.transform.position.x + ShakePos.x,Xmin + 0.5f,Xmax + 0.5f), Mathf.Clamp(stephen.transform.position.y + ShakePos.y,Ymin,Ymax), transform.position.z);
			shakeTimer -= Time.deltaTime;
			shaking = true;
		}
		if (shakeTimer < 0)
		shaking = false;
	}
	public void ShakeCamera(float shakePower, float shakeDuration)
	{
		shakeAmount = shakePower;
		shakeTimer = shakeDuration;
	}
}
