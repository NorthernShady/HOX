using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	[SerializeField]
	SpriteRenderer m_fill = null;

	[SerializeField]
	Texture2D m_texture = null;

	Vector3 m_cameraVector = Vector3.up;

	// Use this for initialization
	void Start () {
		m_cameraVector = FindObjectOfType<CameraController>().position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation(m_cameraVector);
	}

	public void initialize()
	{
		var percent = 0.8f;
		var rect = new Rect(0.0f, 0.0f, m_texture.width * percent, m_texture.height);
		m_fill.sprite = Sprite.Create(m_texture, rect, new Vector2(0.5f, 0.5f));
	}
}
