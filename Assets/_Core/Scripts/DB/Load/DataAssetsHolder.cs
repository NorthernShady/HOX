using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataAssetsHolder", menuName = "DataAssetsHolder")]
public class DataAssetsHolder : ScriptableObject {

	[SerializeField] UserRepresentation userRepresentationAsset;
	[SerializeField] GeneralRepresentation generalRepresentationAsset;
	[SerializeField] CharacterNormRepresentation characterNormRepresentation;
	[SerializeField] HeroConfigRepresentation heroRepresentationAsset;
	[SerializeField] CreepConfigRepresentation creepRepresentationAsset;
	[SerializeField] ItemConfigRepresentation itemRepresentationAsset;

	public UserRepresentation getUserRepresentationAsset()
	{
		return userRepresentationAsset;
	}

	public GeneralRepresentation getGeneralRepresentationAsset()
	{
		return generalRepresentationAsset;
	}

	public CharacterNormRepresentation getCharacterNormRepresentationAsset()
	{
		return characterNormRepresentation;
	}

	public HeroConfigRepresentation getHeroRepresentationAsset()
	{
		return heroRepresentationAsset;
	}

	public CreepConfigRepresentation getCreepRepresentationAsset()
	{
		return creepRepresentationAsset;
	}

	public ItemConfigRepresentation getItemRepresentationAsset()
	{
		return itemRepresentationAsset;
	}
}
