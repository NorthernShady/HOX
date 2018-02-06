using UnityEngine;
using System.Collections.Generic;

namespace Brainiac
{
	public class Blackboard : MonoBehaviour 
	{
		[SerializeField]
		protected Memory[] m_startMemory;

		protected Dictionary<string, object> m_values;

		protected virtual void Awake()
		{
			m_values = new Dictionary<string, object>();
			if (m_startMemory != null) {
				for (int i = 0; i < m_startMemory.Length; i++) {
					SetItem (m_startMemory [i].Name, m_startMemory [i].GetValue ());
				}
			}
		}

		public virtual void SetItem(string name, object item)
		{
			if(!string.IsNullOrEmpty(name))
			{
				if(m_values.ContainsKey(name))
				{
					m_values[name] = item;
				}
				else
				{
					m_values.Add(name, item);
				}
			}
		}

		public virtual object GetItem(string name, object defValue = null)
		{
			if(!string.IsNullOrEmpty(name))
			{
				object value = null;
				if(m_values.TryGetValue(name, out value))
				{
					return value;
				}
			}
			
			return defValue;
		}

		public virtual T GetItem<T>(string name, T defaultValue)
		{
			object value = GetItem(name);
			return (value != null && value is T) ? (T)value : defaultValue;
		}

		public virtual bool HasItem<T>(string name)
		{
			object value = GetItem(name);
			return (value != null && value is T);
		}
	}
}
