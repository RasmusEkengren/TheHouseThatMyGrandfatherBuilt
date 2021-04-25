using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoiceButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
	[SerializeField] private Animator animator = null;
	[SerializeField] private EventSystem eventSystem;
	private Button button;
	private string isSelected = "IsSelected";

	void OnEnable()
	{
		button = GetComponent<Button>();
	}
	public void OnPointerEnter(PointerEventData pointerData)
	{
		eventSystem.SetSelectedGameObject(null);
		button.Select();
		button.OnSelect(null);
	}
	public void OnSelect(BaseEventData selectData)
	{
		if (selectData.selectedObject == this.gameObject) animator.SetBool(isSelected, true);
	}
	public void OnDeselect(BaseEventData deselectData)
	{
		if (deselectData.selectedObject == this.gameObject) animator.SetBool(isSelected, false);
	}
}
