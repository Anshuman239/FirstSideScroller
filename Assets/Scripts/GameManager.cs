using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //this is a static copy of game manager. so we can call it from other game objects.
    public static GameManager instance;
    
    [HideInInspector]
    public float inputDir;
    [HideInInspector]
    public int coinCount;

    [Header("Player Settings")]
    public float speed;
    public float jumpHeight;
    [SerializeField]
    private int playerHealth;
    [SerializeField]
    private PlayerController m_Player;

    [Header("UI Settings")]
    public TextMeshProUGUI coinCountDisplay;
    [SerializeField]
    private SpriteRenderer backGround;
    [SerializeField]
    private Animator HealthBar;
    [SerializeField]
    private Sprite FullHeart, HalfHeart, EmptyHeart;
    private Image[] heartSprites;

    public int m_playerHealth
    {
        get
        {
            return playerHealth;
        }
        set
        {
            playerHealth = value;

            if (playerHealth == 5)
            {
                heartSprites[0].sprite = FullHeart;
                heartSprites[1].sprite = FullHeart;
                heartSprites[2].sprite = HalfHeart;
            }
            else if (playerHealth == 4)
            {
                heartSprites[0].sprite = FullHeart;
                heartSprites[1].sprite = FullHeart;
                heartSprites[2].sprite = EmptyHeart;
            }
            else if (playerHealth == 3)
            {
                heartSprites[0].sprite = FullHeart;
                heartSprites[1].sprite = HalfHeart;
                heartSprites[2].sprite = EmptyHeart;
            }
            else if (playerHealth == 2)
            {
                heartSprites[0].sprite = FullHeart;
                heartSprites[1].sprite = EmptyHeart;
                heartSprites[2].sprite = EmptyHeart;
            }
            else if (playerHealth == 1)
            {
                heartSprites[0].sprite = HalfHeart;
                heartSprites[1].sprite = EmptyHeart;
                heartSprites[2].sprite = EmptyHeart;
            }
            else if (playerHealth <= 0)
            {
                heartSprites[0].sprite = EmptyHeart;
                heartSprites[1].sprite = EmptyHeart;
                heartSprites[2].sprite = EmptyHeart;
            }
            else
            {
                foreach (Image i in heartSprites)
                {
                    i.sprite = FullHeart;
                }
            }

        }
    }

    private void Awake()
    {
        Application.targetFrameRate = 90;
    }

    private void Start()
    {
        coinCountDisplay.text = "00";
        instance = this;
        heartSprites =  HealthBar.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        PlayerMoveInput();
        MoveBG();
        updateUI();
    }

    //raw output of direction key pressed by the player (-1, 0 & 1);
    private void PlayerMoveInput()
    {
        inputDir = Input.GetAxisRaw("Horizontal");
    }


    //here we define movement of background based on player movement.
    private void MoveBG()
    {
        backGround.GetComponent<Renderer>().sharedMaterial.SetVector("_Offset",
            new Vector2(backGround.GetComponent<Renderer>().sharedMaterial.GetVector("_Offset").x + (speed * Time.deltaTime * inputDir * 0.02f * m_Player.rawVelocityX), 0));
    }

    private void updateUI()
    {
        coinCountDisplay.text = coinCount.ToString();
        HealthBar.SetInteger("health", m_playerHealth);
    }
}
