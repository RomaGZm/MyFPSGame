using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject selectedIcon;
    public InventoryItem currentItem;

    public event Action<InventoryCell> selectedCellEvent;
    private bool _isSelected = false;
    
    public void SetItem(InventoryItem item)
    {
        currentItem = item;
        item.transform.parent = transform;
    }
    public InventoryItem SetItemPref(GameObject itemPref)
    {

        GameObject item = Instantiate(itemPref, transform);
        currentItem = item.GetComponent<InventoryItem>();
        return currentItem;
        
    }
    public void RemoveItem()
    {
        Destroy(currentItem.gameObject);
    }
    public bool isSelected
    {
        get { return _isSelected; }
        set 
        {
            _isSelected = value;
            selectedIcon.SetActive(_isSelected);
        }
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        selectedCellEvent?.Invoke(this);
      
    }
}
