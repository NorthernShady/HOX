using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumPhysicalModel : BasicPhysicalModel {

	public override Vector3 getHealthPosition()
	{
		return new Vector3(-1.1f, 4.5f, 0.0f);
	}
}
