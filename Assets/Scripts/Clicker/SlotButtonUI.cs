using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class SlotButtonUI : MonoBehaviour
{
    [field: SerializeField] public Reward Reward;
    public long ClicksLeft 
    {
        get 
        {
            return clicksLeft;
        }
        set 
        { 
            clicksLeft = value;
            if (clicksLeft <= 0) 
            {
                OnSlotReward?.Invoke(Reward);
                stock--;
                if (stock > 0)
                {
                    iteration++;
                    clicksLeft = initialClics + (long)(initialClics * (0.5*iteration));
                }
                else
                {
                    GetComponent<Image>().enabled = false;
                    clickButton.interactable = false;
                    clicksText.enabled = false;
                }
                RefreshClicksText();
            }
        } 
    }
    public static event Action<Reward> OnSlotReward;
    public static event Action<SlotButtonUI> OnSlotClicked;

    [Header("GameController")]

    #region Fields
    [Header("clicks")]
    [SerializeField] private long initialClics = 10;
    [Header("UI")]
    [SerializeField] private Button clickButton;
    [SerializeField] private TextMeshProUGUI clicksText;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private int materialParticleIndex;
    [Header("Preafb points")]
    [SerializeField] private PointsElementUI pointsPrefab;

    private GameController game;
    private int iteration = 0;
    private int stock = 5;
    private long clicksLeft = 0;

 
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        game = FindFirstObjectByType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        clickButton.onClick.AddListener(Click);

        RefreshClicksText();
    }

    private void Initialize()
    {
        ClicksLeft = initialClics;

        //Setear que la particula sea la adecuada
        float segment = 1f / 28f;
        float frame = segment * materialParticleIndex;
        var tex = particles.textureSheetAnimation;
        tex.startFrame = frame;
    }

    #endregion

    #region Public Methods
    public void Click(int clickCount, bool agent = false)
    {
        particles.startSpeed = Mathf.Clamp(clickCount / 2, 1, 30);
        particles.Emit(Mathf.Clamp(clickCount,1, 15));
        ClicksLeft -= clickCount;
        RefreshClicksText();

        Camera.main.DOShakePosition(Mathf.Clamp(0.01f * clickCount, 0, 2));
        if (!agent)
        {
            PointsElementUI newPoints = game.Pool.GetPoints();
            newPoints.Initialize(transform);
            game.RainParticles();
        }
    }

    private void RefreshClicksText()
    {
       clicksText.text = ClicksLeft.ToString(); 
    }

    #endregion

    #region Private Methods
    private void Click()
    {
        OnSlotClicked?.Invoke(this);
        int clickRatio = Mathf.RoundToInt(game.ClickRatio);
        Click(clickRatio);
    }
    #endregion
}