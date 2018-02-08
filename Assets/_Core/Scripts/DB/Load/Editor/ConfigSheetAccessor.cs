using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConfigSheetAccessor {

	DataAssetsHolder m_dataAssetsHolder;

	public ConfigSheetAccessor(DataAssetsHolder dataAssetsHolder) {
		m_dataAssetsHolder = dataAssetsHolder;
	}

	public int[] getIntArrayFromConfigTable(string name) {
		var generalRepresentation = m_dataAssetsHolder.getGeneralRepresentationAsset();
		var row = System.Array.Find (generalRepresentation.dataArray, item => item.Name == name);
		var stringArray = row.Data.Split (',');
		var intArray = System.Array.ConvertAll (stringArray, item => int.Parse (item));
		return intArray;
	}

	public int getIntFromConfigTable(string name) {
		var generalRepresentation = m_dataAssetsHolder.getGeneralRepresentationAsset ();
		var row = System.Array.Find (generalRepresentation.dataArray, item => item.Name == name);
		return int.Parse(row.Data);
	}

	public RangeInt createRange(int min, int max) {
		if (min > max)
			throw new UnityException ("Min is greater then max");
		return new RangeInt (min, max - min);
	}

	public RangeInt getRangeFromConfigTable (string name) {
		var intArray = getIntArrayFromConfigTable (name);
		if (intArray.Length != 2)
			throw new UnityException ("Wrong range value. Should to consist of two values divided by comma");
		return createRange (intArray.First (), intArray.Last () + 1);
	}

}
