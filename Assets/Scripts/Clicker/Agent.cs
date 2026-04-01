using UnityEngine;
using System;
using DG.Tweening;

public class Agent : MonoBehaviour
{
    #region Properties
    public SlotButtonUI destiny { get; set; }
    [field: SerializeField] public float RepeatRate { get; set; }
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    void Start()
    {
        //Movement();
        InvokeRepeating(nameof(Click), 1, RepeatRate);
        SlotButtonUI.OnSlotClicked += SetDestiny;
    }

    private void SetDestiny(SlotButtonUI newDestiny)
    {
        destiny = newDestiny;
        Movement();
    }

    private void Click()
    {
        if (destiny != null && destiny.ClicksLeft>0)
        {
            destiny.Click(1, true);
            //Only Angel
            //if (destiny.ClicksLeft < 0)
                //Destroy(gameObject);
        }
    }

    void Update()
    {

    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    protected void Movement()
    {
        transform.DOMove(destiny.transform.position, 1);
    }
    #endregion
}