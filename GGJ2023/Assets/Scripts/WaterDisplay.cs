using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaterDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = GameManager.Instance.water.ToString();
    }
}
