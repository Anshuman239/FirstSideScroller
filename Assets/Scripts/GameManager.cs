using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private PlayerController m_Player;
    
    private void Awake()
    {
        Application.targetFrameRate = 90;
    }

    private void Start()
    {
        instance = this;
    }
}
