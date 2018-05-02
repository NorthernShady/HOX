using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Services : MonoBehaviour {

	Dictionary<string, object> m_services = new Dictionary<string, object>();

	public void addService<T>(T service)
	{
		var serviceName = typeof(T).ToString();
		if (m_services.ContainsKey(serviceName)) {
			Debug.Log("Already added service " + serviceName);
			return;
		}

		m_services.Add(serviceName, (object)service);
	}

	public T getService<T>()
	{
		return (T)m_services[typeof(T).ToString()];
	}
}
