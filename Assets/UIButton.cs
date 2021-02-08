using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetObject;
    [SerializeField]
    private string _targetMessage;
    private SpriteRenderer _sr;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        _sr.color = Color.cyan;
    }

    void OnMouseExit()
    {
        _sr.color = Color.white;
    }

    void OnMouseDown()
    {
        _sr.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    void OnMouseUp()
    {
        _sr.transform.localScale = new Vector3(2f, 2f, 2f);
        if (_targetObject != null)
        {
            _targetObject.SendMessage(_targetMessage);
        }
    }
}
