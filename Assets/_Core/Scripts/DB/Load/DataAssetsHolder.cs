using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataAssetsHolder", menuName = "DataAssetsHolder")]
public class DataAssetsHolder : ScriptableObject {
	[SerializeField] UserRepresentation userRepresentationAsset;
	[SerializeField] GeneralConfigRepresentation generalRepresentationAsset;

	[SerializeField] XPLevelRepresentation xpLevelRepresentationAsset;

	[SerializeField] InAppItemRepresentation inAppItemRepresentation;

	[SerializeField] NotificationsRepresentation notificationsRepresentationAsset;

	[SerializeField] LevelListRepresentation levelListRepresentationAsset;

	[SerializeField] BoosterConfigRepresentation boosterConfigRepresentationAsset;

	public UserRepresentation getUserRepresentationAsset ()
	{
		return userRepresentationAsset;
	}

	public GeneralConfigRepresentation getGeneralConfigRepresentationAsset ()
	{
		return generalRepresentationAsset;
	}

	public XPLevelRepresentation getXPLevelRepresentationAsset ()
	{
		return xpLevelRepresentationAsset;
	}

	public InAppItemRepresentation getInAppItemRepresentation() {
		return inAppItemRepresentation;
	}

	public NotificationsRepresentation getNotificationsRepresentationAsset() {
		return notificationsRepresentationAsset;
	}

	public LevelListRepresentation getLevelListRepresentationAsset ()
	{
		return levelListRepresentationAsset;
	}

	public BoosterConfigRepresentation getBoosterConfigRepresentationAsset()
	{
		return boosterConfigRepresentationAsset;
	}
}
