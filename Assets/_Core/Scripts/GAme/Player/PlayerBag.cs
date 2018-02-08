using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour {


	int m_playerItemPos;
	int m_chestItemPos;

	List<GameItem> m_playerItems;
	List<GameItem> m_chestItems;

	List<SlotBag> m_playerSlots;
	List<SlotBag> m_chestSlots;


	// Use this for initialization
	void OnEnable () {
		m_playerItemPos = -1;
		m_chestItemPos = -1;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void switchItem()
	{
		if (m_chestItemPos < 0 || m_playerItemPos < 0) {
			return;
		}
		GameItem playerItem = null, chestItem = null;
		foreach (var item in m_playerItems) {
			if (item.pos == m_playerItemPos) {
				playerItem = item;
				break;
			}
		}
		foreach (var item in m_chestItems) {
			if (item.pos == m_chestItemPos) {
				chestItem = item;
				break;
			}
		}
		if (playerItem != null) {
//			m_
		}
//		m_playerItems.re
	}

	public void updateItem()
	{
		foreach (var slot in m_playerSlots) {
			//slot.setDefaultSprite
			slot.enableBorder (false);
			foreach (var item in m_playerItems) {
				if (item.pos == slot.pos) {
//					slot.setSprite (normal);
					if (slot.pos == m_playerItemPos) {
						slot.enableBorder (true);
					}
					break;
				}
			}
		}

		foreach (var slot in m_chestSlots) {
			//slot.setDefaultSprite
			slot.enableBorder (false);
			foreach (var item in m_chestItems) {
				if (item.pos == slot.pos) {
					//					slot.setSprite (normal);
					if (slot.pos == m_chestItemPos) {
						slot.enableBorder (true);
					}
					break;
				}
			}
		}

	}

	public void onItemTap(int slotPos)
	{
		foreach (var slot in m_playerSlots) {
			if (slot.pos == slotPos) {
				if (slot.isBorderEnable) {
					m_playerItemPos = -1;
					slot.enableBorder (false);
				} else {
					m_playerItemPos = slotPos;
					slot.enableBorder (true);
				}
			} else {
				slot.enableBorder (false);
			}
		}

		foreach (var slot in m_chestSlots) {
			if (slot.pos == slotPos) {
				if (slot.isBorderEnable) {
					m_chestItemPos = -1;
					slot.enableBorder (false);
				} else {
					m_chestItemPos = slotPos;
					slot.enableBorder (true);
				}
			} else {
				slot.enableBorder (false);
			}
		}
	}


}
