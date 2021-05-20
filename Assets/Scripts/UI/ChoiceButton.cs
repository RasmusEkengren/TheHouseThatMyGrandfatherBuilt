using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoiceButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
	[SerializeField] private float TimeToSelectable = 0.2f;
	[SerializeField] private bool isSelectedAtStart = false;
	[SerializeField] private Animator animator = null;
	[SerializeField] private EventSystem eventSystem;
	private Button button;
	private bool isSelectable;
	private float timer = 0;
	private string isSelected = "IsSelected";

	void OnEnable()
	{
		button = GetComponent<Button>();
		timer = 0;
		isSelectable = false;
	}
	void Update()
	{
		if (!isSelectable)
		{
			if (timer < TimeToSelectable)
			{
				timer += Time.deltaTime;
			}
			else if (isSelectedAtStart)
			{
				isSelectable = true;
				eventSystem.SetSelectedGameObject(null);
				button.Select();
			}
			else
			{
				isSelectable = true;
			}
		}
	}
	public void OnPointerEnter(PointerEventData pointerData)
	{
		if (isSelectable)
		{
			eventSystem.SetSelectedGameObject(null);
			button.Select();
			button.OnSelect(null);
		}
	}
	public void OnSelect(BaseEventData selectData)
	{
		if (selectData.selectedObject == this.gameObject && isSelectable && animator != null) animator.SetBool(isSelected, true);
	}
	public void OnDeselect(BaseEventData deselectData)
	{
		if (deselectData.selectedObject == this.gameObject && isSelectable && animator != null) animator.SetBool(isSelected, false);
	}
	public void DeselectButton()
	{
		eventSystem.SetSelectedGameObject(null);
	}
}
