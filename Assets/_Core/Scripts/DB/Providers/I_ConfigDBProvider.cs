using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_ConfigDBProvider {

	float getFloatValue(string name);
	int getIntValue(string name);
	int[] getIntArrayValue(string name);
	float[] getFloatArrayValue(string name);
}
