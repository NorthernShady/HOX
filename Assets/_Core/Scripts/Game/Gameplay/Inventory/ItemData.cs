﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Game/Create ItemData")]
public class ItemData : ScriptableObject {

	[System.Serializable] public class DomaineColor : TypedMap<GameData.DomaineType, Color> { }
	[System.Serializable] public class DomaineMaterial : TypedMap<GameData.DomaineType, Material> { }
	[System.Serializable] public class ItemImage : TypedMap<GameData.ItemType, Sprite> { }

	[SerializeField]
	public DomaineColor domaineColor = null;

	[SerializeField]
	public DomaineColor ringColor = null;

	[SerializeField]
	public DomaineMaterial domaineMaterial = null;

	[SerializeField]
	public ItemImage itemImage = null;
}
