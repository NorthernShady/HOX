using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataAssetsHolder", menuName = "DataAssetsHolder")]
public class DataAssetsHolder : ScriptableObject {

	[SerializeField] UserRepresentation userRepresentationAsset;
	[SerializeField] GeneralRepresentation generalRepresentationAsset;
	[SerializeField] HeroesRepresentation heroesRepresentationAsset;
	[SerializeField] CreepsRepresentation creepsRepresentationAsset;

	public UserRepresentation getUserRepresentationAsset()
	{
		return userRepresentationAsset;
	}

	public GeneralRepresentation getGeneralRepresentationAsset()
	{
		return generalRepresentationAsset;
	}

	public HeroesRepresentation getHeroesRepresentationAsset()
	{
		return heroesRepresentationAsset;
	}

	public CreepsRepresentation getCreepsRepresentationAsset()
	{
		return creepsRepresentationAsset;
	}
}
