using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class tk2dAnimationAdapter : MonoBehaviour {

	public Color color = Color.white;
	public Vector3 scale = Vector3.one;
	tk2dBaseSprite sprite = null;
	tk2dTextMesh textMesh = null;

	public Color textColor = Color.white;
	public Vector3 textScale = Vector3.one;

	// Use this for initialization
	void Start() {
		sprite = GetComponent<tk2dBaseSprite>();
		textMesh = GetComponent<tk2dTextMesh>();
		if (sprite != null) {
			color = sprite.color;
			scale = sprite.scale;
		}
		if (textMesh != null) {
			textColor = textMesh.color;
			textScale = textMesh.scale;
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		DoUpdate();
	}

	void DoUpdate() {
		if (sprite != null && (sprite.color != color || sprite.scale != scale)) {
			sprite.color = color;
			sprite.scale = scale;
			sprite.Build();
		}
		if (textMesh != null && (textMesh.color != textColor || textMesh.scale != textScale)) {
			textMesh.color = textColor;
			textMesh.scale = textScale;
			textMesh.Commit();
		}
	}
}