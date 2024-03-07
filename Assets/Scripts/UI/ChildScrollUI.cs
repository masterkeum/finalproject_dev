using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChildScrollUI : ScrollRect
{
    private bool _forParent;
    private ScrollUI _scrollUI;
    private ScrollRect _parentScrollRect;

    protected override void Start()
    {
        _scrollUI = GameObject.FindWithTag("ScrollUI").GetComponent<ScrollUI>();
        _parentScrollRect = GameObject.FindWithTag("ScrollUI").GetComponent<ScrollRect>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _forParent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);

        if( _forParent )
        {
            _scrollUI.OnBeginDrag(eventData);
            _parentScrollRect.OnBeginDrag(eventData);
        }
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (_forParent)
        {
            _scrollUI.OnDrag(eventData);
            _parentScrollRect.OnDrag(eventData);
        }
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (_forParent)
        {
            _scrollUI.OnEndDrag(eventData);
            _parentScrollRect.OnEndDrag(eventData);
        }
        base.OnEndDrag(eventData);
    }
}
