using System;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public enum  BarType {L, C, R}
    public BarType barType;
    
    public Stack<GameObject> stack = new Stack<GameObject>();

    private void OnMouseEnter()
    {
        if (!HanioTower.isSelected) return;
        HanioTower.selectGear.GetComponent<Rigidbody>().useGravity = false;
        var selectPos = transform.position;
        selectPos.y += HanioTower.selectY;
        HanioTower.selectGear.transform.position = selectPos;
        HanioTower.selectGear.transform.rotation = HanioTower.gearRotation;
    }

    private void OnMouseDown()
    {
        if (HanioTower.isSelected)
        {
            DropGear();
        }
        else
        {
            SelectGear();
        }
    }

    void SelectGear()
    {
        if (stack.Count == 0)
        {
            return;
        }
        HanioTower.isSelected = true;
        HanioTower.selectGear = stack.Pop();
        HanioTower.selectGear.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        OnMouseEnter();
    }

    void DropGear()
    {
        if (stack.Count > 0 && stack.Peek().GetComponent<Gear>().Num < HanioTower.selectGear.GetComponent<Gear>().Num) return;
        HanioTower.MoveGear(this, HanioTower.selectGear);
    }
}
