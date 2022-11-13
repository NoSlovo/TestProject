using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    private List<Item> _items;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item _tower))
        {
           StartCoroutine( GetItem(_tower));
        }
    }
    
    private IEnumerator GetItem(Item item)
    {
        var waitForSecondsRealtime = new WaitForSecondsRealtime(1f);
        yield return waitForSecondsRealtime;
        item.transform.SetParent(transform);
        item.transform.DOMove(transform.position, 1f);
        
    }
}
