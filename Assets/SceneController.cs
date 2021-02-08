using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int _gridCols = 4;
    private int _gridRows = 2;
    private float _offsetX = 2f;
    private float _offsetY = 2.5f;

    [SerializeField]
    private MemoryCard _originalCard;
    [SerializeField]
    private Sprite[] _sprites;

    [SerializeField]
    private TextMesh _textLable;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    private int _scores = 0;

    public bool CanReveal { get { return _secondRevealed == null; } }

    void Start()
    {
        var startPos = _originalCard.transform.position;

        int[] numOrder = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numOrder = ShuffleArray(numOrder);

        for (int i = 0; i < _gridCols; i++)
        {
            for (int j = 0; j < _gridRows; j++)
            {
                MemoryCard currCard;
                if (i == 0 && j == 0)
                {
                    currCard = _originalCard;
                }
                else
                {
                    currCard = Instantiate(_originalCard);
                }

                int id = numOrder[j * _gridCols + i];
                currCard.SetCard(id, _sprites[id]);

                float currPosX = _offsetX * i + startPos.x;
                float currPosY = -_offsetY * j + startPos.y;

                currCard.transform.position = new Vector3(currPosX, currPosY, startPos.z);
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private int[] ShuffleArray(int[] arr)
    {
        int[] newArr = arr.Clone() as int[];

        for (int i = 0; i < newArr.Length; i++)
        {
            int tmp = newArr[i];
            int r = Random.Range(i, newArr.Length);

            newArr[i] = newArr[r];
            newArr[r] = tmp;
        }

        return newArr;
    }

    public void OnRevealed(MemoryCard revealedCard)
    {
        if (_firstRevealed)
        {
            _secondRevealed = revealedCard;

            StartCoroutine(CheckMatch());
        }
        else
        {
            _firstRevealed = revealedCard;
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed == null || _secondRevealed == null)
        {
            yield return null;
        }

        if (_firstRevealed.Id == _secondRevealed.Id)
        {
            _scores++;
            _textLable.text = $"Score: {_scores}";
            Debug.Log("Match!");

            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _firstRevealed.Hide();
            _secondRevealed.Hide();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }
}
