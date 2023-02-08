using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PriceDisplay : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    [SerializeField] private RootsTilemap rootsTilemap;

    // Update is called once per frame
    void Update()
    {
        image.sprite = rootsTilemap.GetRootSpriteByName(GameManager.Instance.nextTileType.sprite.name);
        text.text = GameManager.Instance.GetPrice(GameManager.Instance.GetNumberOfRoots()).ToString();
    }
}
