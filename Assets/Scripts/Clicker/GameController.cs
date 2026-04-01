using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [field: SerializeField] public float ClickRatio { get; set; }
    [field: SerializeField] public PoolSystem Pool { get; set; }

    [SerializeField] private Agent[] agents;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI clicksText;

    [SerializeField] private ParticleSystem particlesRain;
    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SlotButtonUI.OnSlotReward += GetReward;
    }
    private void OnDestroy()
    {
        SlotButtonUI.OnSlotReward -= GetReward;
    }
    public void RainParticles()
    {
        particlesRain.Emit(Mathf.Clamp((int)ClickRatio, 0, 13));
    }

    private void GetReward(Reward reward)
    {
        ShowReward(reward);

        if (reward.RewardType == RewardType.Plus)
        {
            ClickRatio += reward.Value;
            clicksText.text = "x" + ClickRatio;
            return;
        }

        if (reward.RewardType == RewardType.Multi)
        {
            ClickRatio *= reward.Value;
            clicksText.text = "x" + ClickRatio;
            return;
        }

        if (reward.RewardType == RewardType.Agent)
        {
            Agent newAgent = Instantiate(agents[(int)reward.Value], transform.position, Quaternion.identity);
            newAgent.destiny = reward.ObjectReward;

            return;
        }
    }

    private void ShowReward(Reward reward)
    {
        if (!rewardText.gameObject.activeSelf)
        {
            rewardText.gameObject.SetActive(true);
            rewardText.transform.localScale = Vector3.zero;
        }

        rewardText.text = "REWARD\n " + reward.RewardType + reward.Value + " Clicks";

        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(rewardText.transform.DOScale(1, 1));

        mySequence.Append(rewardText.transform.DOShakeRotation(1, new Vector3(0, 0, 30)));

        mySequence.Append(rewardText.transform.DOScale(0, 1));

        mySequence.Play();

    }
}
