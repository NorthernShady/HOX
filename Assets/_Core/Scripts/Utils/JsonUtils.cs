using System.Collections;
using System.Collections.Generic;
using System;

public class JsonUtils {

	public static class Json {
		public static object Deserialize (string text) {
			var parser = new SharpJson.JsonDecoder();
			parser.parseNumbersAsFloat = true;
			return parser.Decode(text);
		}
	}

	public static float[] GetFloatArray(Dictionary<String, Object> map, String name, float scale) {
		var list = (List<Object>)map[name];
		var values = new float[list.Count];
		if (scale == 1) {
			for (int i = 0, n = list.Count; i < n; i++)
				values[i] = (float)list[i];
		} else {
			for (int i = 0, n = list.Count; i < n; i++)
				values[i] = (float)list[i] * scale;
		}
		return values;
	}

	public static int[] GetIntArray(Dictionary<String, Object> map, String name) {
		var list = (List<Object>)map[name];
		var values = new int[list.Count];
		for (int i = 0, n = list.Count; i < n; i++)
			values[i] = (int)(float)list[i];
		return values;
	}

	public static float GetFloat(Dictionary<String, Object> map, String name, float defaultValue) {
		if (!map.ContainsKey(name))
			return defaultValue;
		return (float)map[name];
	}

	public static int GetInt(Dictionary<String, Object> map, String name, int defaultValue) {
		if (!map.ContainsKey(name))
			return defaultValue;
		return (int)(float)map[name];
	}

	public static bool GetBoolean(Dictionary<String, Object> map, String name, bool defaultValue) {
		if (!map.ContainsKey(name))
			return defaultValue;
		return (bool)map[name];
	}

	public static String GetString(Dictionary<String, Object> map, String name, String defaultValue) {
		if (!map.ContainsKey(name))
			return defaultValue;
		return (String)map[name];
	}

	public static float ToColor(String hexString, int colorIndex) {
		if (hexString.Length != 8)
			throw new ArgumentException("Color hexidecimal length must be 8, recieved: " + hexString, "hexString");
		return Convert.ToInt32(hexString.Substring(colorIndex * 2, 2), 16) / (float)255;
	}
}
