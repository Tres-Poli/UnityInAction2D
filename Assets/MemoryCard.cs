using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField]
    private SceneController _sc;
    [SerializeField]
    private GameObject _cardBack;

    public int Id { get; private set; }

    void OnMouseDown()
    {
        if (_cardBack.activeSelf && _sc.CanReveal)
        {
            _cardBack.SetActive(false);
            _sc.OnRevealed(this);
        }
    }

    public void Hide()
    {
        _cardBack.SetActive(true);
    }

    public void SetCard(int id, Sprite sprite)
    {
        Id = id;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
