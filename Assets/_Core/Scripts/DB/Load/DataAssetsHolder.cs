using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataAssetsHolder", menuName = "DataAssetsHolder")]
public class DataAssetsHolder : ScriptableObject {

	[SerializeField] UserRepresentation userRepresentationAsset;
	[SerializeField] GeneralRepresentation generalRepresentationAsset;
	[SerializeField] HeroConfigRepresentation heroRepresentationAsset;
	[SerializeField] CreepConfigRepresentation creepRepresentationAsset;

	public UserRepresentation getUserRepresentationAsset()
	{
		return userRepresentationAsset;
	}

	public GeneralRepresentation getGeneralRepresentationAsset()
	{
		return generalRepresentationAsset;
	}

	public HeroConfigRepresentation getHeroRepresentationAsset()
	{
		return heroRepresentationAsset;
	}

	public CreepConfigRepresentation getCreepRepresentationAsset()
	{
		return creepRepresentationAsset;
	}
}
