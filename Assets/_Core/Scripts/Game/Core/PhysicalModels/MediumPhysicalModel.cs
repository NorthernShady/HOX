using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumPhysicalModel : BasicPhysicalModel {

	public override Vector3 getHealthPosition()
	{
		return new Vector3(0.0f, 6.0f, -0.2f);
	}
}
