using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SawingCounter : MonoBehaviour
{
	[SerializeField] private Image cutsLeft = null;
	[SerializeField] private Image numberToCut = null;
	[SerializeField] private Sprite icon0 = null;
	[SerializeField] private Sprite icon1 = null;
	[SerializeField] private Sprite icon2 = null;
	[SerializeField] private Sprite icon3 = null;
	[SerializeField] private Sprite icon4 = null;
	[SerializeField] private Sprite icon5 = null;
	[SerializeField] private Sprite icon6 = null;
	private Sawing sawingMinigame = null;

	void Start()
	{
		sawingMinigame = GetComponentInParent<Sawing>();
	}

	void Update()
	{
		switch (sawingMinigame.GetPlanksNumberToCut())
		{
			case 0:
				numberToCut.sprite = icon0;
				break;
			case 1:
				numberToCut.sprite = icon1;
				break;
			case 2:
				numberToCut.sprite = icon2;
				break;
			case 3:
				numberToCut.sprite = icon3;
				break;
			case 4:
				numberToCut.sprite = icon4;
				break;
			case 5:
				numberToCut.sprite = icon5;
				break;
			case 6:
				numberToCut.sprite = icon6;
				break;
			default:
				numberToCut.sprite = icon6;
				break;
		}
		switch (sawingMinigame.GetPlankCompletions())
		{
			case 0:
				cutsLeft.sprite = icon0;
				break;
			case 1:
				cutsLeft.sprite = icon1;
				break;
			case 2:
				cutsLeft.sprite = icon2;
				break;
			case 3:
				cutsLeft.sprite = icon3;
				break;
			case 4:
				cutsLeft.sprite = icon4;
				break;
			case 5:
				cutsLeft.sprite = icon5;
				break;
			case 6:
				cutsLeft.sprite = icon6;
				break;
			default:
				cutsLeft.sprite = icon6;
				break;
		}
	}
}
