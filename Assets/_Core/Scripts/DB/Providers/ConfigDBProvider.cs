using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DBProvider: I_ConfigDBProvider {

	// TODO: Replace with Dictionary. (Seems, I was drunk)
	private List<Config> m_savedData;

	void loadData() {
		if (m_savedData == null)
			m_savedData = new List<Config> (dataService.connection.Table<Config> ());
	}

	public float getFloatValue(string name) {
		loadData ();
		return float.Parse(m_savedData.Find (record => record.Name == name).Data);
	}

	public int getIntValue(string name) {
		loadData ();
		return int.Parse(m_savedData.Find (record => record.Name == name).Data);
	}

	public int[] getIntArrayValue(string name) {
		loadData ();
		var list = m_savedData.Find (record => record.Name == name).Data.Split (',');
		return System.Array.ConvertAll(list, x => int.Parse(x));
	}

	public float[] getFloatArrayValue(string name) {
		loadData ();
		var list = m_savedData.Find (record => record.Name == name).Data.Split (',');
		return System.Array.ConvertAll(list, x => float.Parse(x));
	}
}
