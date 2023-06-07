using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    public enum ItemType
    {
        PistolBullets, RifleBullets
    }
    public ItemType itemType;

    public int counter = 0;
    
    [SerializeField]
    private TMP_Text text;

    public void SetDrop(int count)
    {
        counter = count;
        text.text = count.ToString();
    }
  
    public void ClearDrop()
    {
        counter = 0;
        text.text = "0";
    }
}
