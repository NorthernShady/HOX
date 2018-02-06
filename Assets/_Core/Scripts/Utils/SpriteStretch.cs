using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class SpriteStretch : MonoBehaviour {
	public enum Stretch{Horizontal, Vertical, Both};
	public Stretch stretchDirection = Stretch.Horizontal;
	public Vector2 offset = new Vector2(0f,0f);

	SpriteRenderer sprite;
	Transform _thisTransform;


	void Start () 
	{
		_thisTransform = transform;
		sprite = GetComponent<SpriteRenderer>();
		StartCoroutine("stretch");
	}
	#if UNITY_EDITOR
	void Update()
	{
		scale();
	}
	#endif
	IEnumerator stretch()
	{
		yield return new WaitForEndOfFrame();
		scale();
	}
	void scale()
	{

		var camera = Camera.main.gameObject.gameObject.GetComponent<tk2dCamera> ();
		float worldScreenHeight = camera.ScreenExtents.height;
		float worldScreenWidth = camera.ScreenExtents.width;
		float ratioScale = worldScreenWidth / sprite.sprite.bounds.size.x;
		ratioScale += offset.x;
		float h = worldScreenHeight /  sprite.sprite.bounds.size.y;
		h += offset.y;
		switch(stretchDirection)
		{
		case Stretch.Horizontal:
			_thisTransform.localScale = new Vector3(ratioScale,_thisTransform.localScale.y,_thisTransform.localScale.z);
			break;
		case Stretch.Vertical:
			_thisTransform.localScale = new Vector3(_thisTransform.localScale.x, h,_thisTransform.localScale.z);
			break;
		case Stretch.Both:
			_thisTransform.localScale = new Vector3(ratioScale, h,_thisTransform.localScale.z);
			break;
		default:break;
		}
	}
}