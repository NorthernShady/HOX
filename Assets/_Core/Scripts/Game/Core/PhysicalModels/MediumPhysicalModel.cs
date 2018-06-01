using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumPhysicalModel : BasicPhysicalModel {

	public override Vector3 getInfoPosition()
	{
		return new Vector3(0.0f, -2.5f, 0.0f);
	}
}
