using UnityEngine;
using System;
using DG.Tweening;
using TMPro;

public class PointsElementUI : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private TextMeshProUGUI clicksText;
    [SerializeField] private float duration = 3;
    private GameController game;

    #endregion

    #region Unity Callbacks
    void Awake()
    {
        game = FindFirstObjectByType<GameController>();
    }
  
    void Start()
    {
        DoEffect();
    }

    private void DoEffect()
    {
        //clicksText.transform.localScale = new Vector3(100, 100, 100);
        clicksText.transform.Translate(Vector3.back);
      
        clicksText.text = "+" + game.ClickRatio.ToString();

      
        transform.DOMoveY(transform.position.y + UnityEngine.Random.Range(10, 50), duration);

        clicksText.DOColor(new Color(0, 0, 0, 0), duration);

       
        game.Pool.AddToPool(this, duration);
    }


    public void Initialize(Transform transformClick)
    {
        clicksText.color = Color.white;
        gameObject.SetActive(true);
        transform.parent = transformClick;

        transform.localPosition = Vector3.zero;
        DoEffect();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}