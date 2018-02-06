using UnityEngine;
using System.Collections;

public class SharedCounter
{
	public int size = 0;
	public int index = 0;
	public bool isDoneSomething = false;

	public SharedCounter()
	{
	}

	public SharedCounter(int size)
	{
		this.size = size;
	}
}

