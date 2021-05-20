using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SawingCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText = null;

    private Sawing sawingMinigame = null;

    void Start()
    {
        sawingMinigame = GetComponentInParent<Sawing>();
    }

    void Update()
    {
        counterText.text = "Planks left: "+ (sawingMinigame.GetPlanksLeft()).ToString();
    }
}
