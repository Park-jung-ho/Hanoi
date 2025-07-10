using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TMP_Text hanoiLv;
    public TMP_Text score;
    public TMP_Text timeUI;
    
    private float timer;

    public void init()
    {
        timer = 0;
        HanioTower.moveCount = 0;
    }

    public void isStarted()
    {
    }

    public void ScoreUpdate()
    {
        score.text = $"Count\n{HanioTower.moveCount}";
    }
    
    private void Update()
    {
        if (!HanioTower.isStart) return;
        
        timer += Time.deltaTime;
        timeUI.text = $"Time\n{timer:0.00}";
    }
}
