using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lib;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBuilder : MonoBehaviour 
{
    private float _itemHeight = 0.20f;
    private Stack<Item> _colection = new ();
    private bool _isRefreshExecuted = false;
    private float _spead = 0.5f;
    private static int _maxLengthColetion = 20;
    protected int _currentCount = 0;
    public int CurrentCount => _currentCount;
    public bool TowerFull => CurrentCount == _maxLengthColetion;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out  Item item))
        {
            PutElement(item);
        }
    }


    public void PutElement(Item collectable)
    {
        if (TowerFull)
            return;

        var targetPosition = new Vector3(0, _currentCount * _itemHeight, 0);
        _currentCount++;
        collectable.transform.SetParent(transform);
        collectable.transform.DOLocalJump(targetPosition, 1f, 1, 0.3f)
            .OnComplete(() =>
            {
                _colection.Push(collectable);
                RefreshPosition();
            });

        collectable.transform.DOLocalRotate(new Vector3(90f, 100f, 0f), 0.1f).OnComplete(() =>
        {
            var targetRotation = new Vector3(90f, 100f, 0f);
            collectable.transform.DOLocalRotate(targetRotation, 0);
        });
    }

    private void RefreshPosition()
    {
        if (!_isRefreshExecuted)
            StartCoroutine(UpdatePositions());
    }


    private IEnumerator UpdatePositions()
    {
        _isRefreshExecuted = true;
        bool needUpdate = false;

        do
        {
            needUpdate = false;

            _colection.ForEachIndexed((log, i) =>
            {
                int index = _colection.Count - i - 1;
                var targetPosition = new Vector3(0, index * _itemHeight, 0);
                log.transform.localPosition = Vector3.MoveTowards(
                    log.transform.localPosition,
                    targetPosition,
                    _itemHeight / _spead * Time.deltaTime);

                needUpdate = needUpdate || log.transform.localPosition != targetPosition;
            });

            yield return null;
        } while (needUpdate);
        
        _isRefreshExecuted = false;
    }
    
    public bool TryElementColection(out Item itemColection)
    {
        if (_colection.Count > 0)
        {
            _currentCount--;
            var elemntColection = _colection.Pop();
            elemntColection.transform.SetParent(null);
            itemColection = elemntColection;
            return true;
        }

        itemColection = null;
        return false;
    }
}