using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;
    public Transform contentContainer;

    public Slider tabSlider;
    public RectTransform[] buttonRect;

    private const int _size = 3;
    private float[] _position = new float[_size];
    private float _distance;
    private float _curPositon;
    private float _targetPosition;
    private int _targetIndex;
    private bool _isDrag;

    private void Start()
    {
        _distance = 1f / (_size - 1);
        for (int i = 0; i < _size; i++)
        {
            _position[i] = _distance * i;
        }
        GetComponent<ScrollRect>().horizontalScrollbar.value = 0.5f;
        scrollbar.value = 0.5f;
        tabSlider.value = 0.5f;
        TabClick(1);
    }
    private float SetPosition()
    {
        for (int i = 0; i < _size; ++i)
        {
            if (scrollbar.value < _position[i] + _distance * .5f && scrollbar.value > _position[i] - _distance * .5f)
            {
                _targetIndex = i;
                return _position[i];
            }
        }
        return 0f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _curPositon = SetPosition();
    }
    public void OnDrag(PointerEventData eventData)
    {
        _isDrag = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;
        _targetPosition = SetPosition();
        
        if(_curPositon == _targetPosition)
        {
            if (eventData.delta.x > 18 && _curPositon - _distance >= 0)
            {
                --_targetIndex;
                _targetPosition = _curPositon - _distance;
            }
            else if (eventData.delta.x < -18 && _curPositon + _distance <= 1.01f)
            {
                ++_targetIndex;
                _targetPosition = _curPositon + _distance;
            }
        }
        
        for(int i = 0; i < _size; i++)
        {
            if (contentContainer.GetChild(i).GetComponent<ChildScrollUI>() && _curPositon != _position[i] && _targetPosition == _position[i])
            {
                contentContainer.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 1;
            }
        }
    }
    private void Update()
    {
        tabSlider.value = scrollbar.value;

        if (!_isDrag)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, _targetPosition, 0.1f);
            for(int i = 0; i < _size; i++)
            {
                buttonRect[i].sizeDelta = new Vector2(i == _targetIndex? 450 : 300, buttonRect[i].sizeDelta.y);
                if (buttonRect[2].sizeDelta.x == 450)
                {
                    buttonRect[1].pivot = new Vector2(1.5f,0.5f) ;
                }
                else
                {
                    buttonRect[1].pivot = new Vector2(1, 0.5f);
                }
            }
        }

    }

    public void TabClick(int n)
    {
        _targetIndex = n;
        _targetPosition = _position[n];
    }

}
