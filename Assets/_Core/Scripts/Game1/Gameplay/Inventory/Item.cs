using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

	public Item(GameData.ItemType type, GameData.DomaineType domaineType, CommonTraits data)
	{
		this.type = type;
		this.domaineType = domaineType;
		this.data = data;
	}

	public Item(GameData.ItemType type, GameData.DomaineType domaineType)
	{
		this.type = type;
		this.domaineType = domaineType;
		this.data = new CommonTraits(CharacterConfigDBHelper.getItemConfig(type));
	}

	public Item() {}

	public GameData.ItemType type = GameData.ItemType.NONE;
	public GameData.DomaineType domaineType = GameData.DomaineType.NONE;

	public CommonTraits data = new CommonTraits();
}
