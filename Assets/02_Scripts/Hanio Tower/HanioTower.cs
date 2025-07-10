using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HanioTower : MonoBehaviour
{
    public enum Level { Lv1 = 1, Lv2, Lv3, Lv4, Lv5, Lv6, Lv7, Lv8, Lv9, Lv10}
    public Level CurLevel = Level.Lv3;
    
    public UiManager UiManager;
    
    public Bar[] bars;
    public GameObject[] gears;

    public float autoSpeed;
    
    public TMP_InputField levelInput;
    public static bool isStart = false;
    public static Quaternion gearRotation;
    public static int moveCount = 0;
    public static float selectY = 11f;
    public static GameObject selectGear;
    public static bool isSelected = false;

    private void Start()
    {
        Time.timeScale = 10;
        SettingGears();
    }

    IEnumerator SpawnGearRoutine()
    {
        gearRotation = gears[0].transform.rotation;
        for (int i = 0; i < (int)CurLevel; i++)
        {
            SpawnGear(bars[0], gears[(int)CurLevel - 1 - i]);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void SettingGears()
    {
        for (int i = 0; i < 3; i++)
        {
            while (bars[i].stack.Count > 0)
            {
                GameObject g = bars[i].stack.Pop();
                Destroy(g);
            }
        }

        StartCoroutine(SpawnGearRoutine());
    }

    public void LevelChange()
    {
        CurLevel = (Level)int.Parse(levelInput.text);
    }
    public void AutoSolve()
    {
        isStart = true;
        StartCoroutine(SolveHanoi((int)CurLevel,0,2,1));
    }

    public void SpawnGear(Bar bar, GameObject gear)
    {
        GameObject newGear = Instantiate(gear, bar.transform.position + Vector3.up * selectY, gear.transform.rotation);
        bar.stack.Push(newGear);
    }

    public static void MoveGear(Bar bar, GameObject gear)
    {
        isSelected = false;
        selectGear.GetComponent<Rigidbody>().useGravity = true;
        gear.transform.position = bar.transform.position + Vector3.up * selectY;
        gear.transform.rotation = gearRotation;
        bar.stack.Push(gear);
    }

    IEnumerator MoveGear(int start, int to)
    {
        yield return new WaitForSeconds(autoSpeed);
        Debug.Log($"{start+1}==>{to+1}");
        GameObject gear = bars[start].stack.Pop();
        gear.transform.position = bars[to].transform.position + Vector3.up * selectY;
        gear.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        bars[to].stack.Push(gear);
        
        moveCount++;
        UiManager.ScoreUpdate();
    }
    IEnumerator SolveHanoi(int n, int start, int to, int via)
    {
        if (n == 1)
        {
            yield return StartCoroutine(MoveGear(start,to));
        }
        else
        {
            yield return StartCoroutine(SolveHanoi(n - 1, start, via, to));
            yield return StartCoroutine(MoveGear(start, to));
            yield return StartCoroutine(SolveHanoi(n - 1, via, to, start));
        }
        if (bars[2].stack.Count == (int)CurLevel) isStart = false;
    }
}
