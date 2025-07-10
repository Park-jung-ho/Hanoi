using System.Collections;
using UnityEngine;

public class HanioTower : MonoBehaviour
{
    public enum Level { Lv1 = 3, Lv2, Lv3 }
    public Level CurLevel = Level.Lv1;

    public Bar[] bars;
    public GameObject[] gears;

    public float autoSpeed;
    
    public static Quaternion gearRotation;
    public static float selectY = 11f;
    public static GameObject selectGear;
    public static bool isSelected = false;


    IEnumerator Start()
    {
        gearRotation = gears[0].transform.rotation;
        for (int i = 0; i < (int)CurLevel; i++)
        {
            SpawnGear(bars[0], gears[(int)CurLevel - 1 - i]);
            yield return new WaitForSeconds(0.5f);
        }
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
        gear.transform.rotation = gearRotation;
        bars[to].stack.Push(gear);
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
    }
}
