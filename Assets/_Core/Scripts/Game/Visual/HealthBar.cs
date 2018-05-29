using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	[SerializeField]
	SpriteRenderer m_fill = null;

	[SerializeField]
	Texture2D m_texture = null;

	[SerializeField]
	Character m_character = null;

	Vector3 m_cameraVector = Vector3.up;

	void OnEnable()
	{
		m_character.OnHealthChanged += onHealthChanged;
		m_character.OnPhysicsInitialized += onPhysicsInitialized;
	}

	void OnDisable()
	{
		m_character.OnHealthChanged -= onHealthChanged;
		m_character.OnPhysicsInitialized -= onPhysicsInitialized;
	}

	void Start () {
		onHealthChanged(1.0f);
		m_cameraVector = FindObjectOfType<CameraController>().position;
	}

	void Update () {
		transform.rotation = Quaternion.LookRotation(m_cameraVector);
	}

	void onHealthChanged(float percent)
	{
		var rect = new Rect(0.0f, 0.0f, m_texture.width * percent, m_texture.height);
		m_fill.sprite = Sprite.Create(m_texture, rect, new Vector2(0.0f, 0.0f));
	}

	void onPhysicsInitialized(BasicPhysicalModel physicalModel)
	{
		transform.localPosition = physicalModel.getHealthPosition();
	}
}
