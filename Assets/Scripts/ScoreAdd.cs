using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreAdd : MonoBehaviour
{
    private void Start()
    {
        var tm = GetComponent<TextMeshProUGUI>();

        tm.text = "+  " + MainClass.instance.LatestScore;

        transform.DOLocalMoveY(5, 0.5f, false);

        tm.DOColor(Color.clear, 0.5f).SetEase(Ease.InQuart);

    }
}