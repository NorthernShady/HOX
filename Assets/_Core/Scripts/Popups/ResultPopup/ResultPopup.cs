﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPopup : BasePopup {

    [SerializeField]
    tk2dSprite m_resultSprite = null;

    [SerializeField]
    GameObject m_continueButton = null;

    protected override void Awake()
    {
        bool hasWon = FindObjectOfType<GameDataProxy>().hasWon;
        m_resultSprite.SetSprite(hasWon ? "win" : "lose");
        m_continueButton.transform.localPosition = hasWon ? new Vector3(0.0f, -1.45f, -0.1f) : new Vector3(0.0f, -2.05f, -0.1f);
    }

    public override void onClose()
	{
		SceneManager.LoadScene(k.Scenes.MAIN_MENU);
	}
}