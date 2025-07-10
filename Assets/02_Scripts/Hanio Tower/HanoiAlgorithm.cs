using System;
using System.Collections.Generic;
using UnityEngine;

public class HanoiAlgorithm : MonoBehaviour
{
    Stack<int>[] stack = new Stack<int>[3];
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            stack[i] = new Stack<int>();
        }
        for (int i = 5; i > 0; i--) stack[0].Push(i);
        Hanio(5,1,3,2);
    }

    void ShowHanoi()
    {
        string print = "";
        Stack<int>[] tmp = new Stack<int>[3];
        for (int i = 0; i < 3; i++) tmp[i] = new Stack<int>();
        for (int j = 5; j > 0; j--)
        {
            for (int i = 0; i < 3; i++)
            {
                if (j <= stack[i].Count)
                {
                    tmp[i].Push(stack[i].Pop());
                    print += tmp[i].Peek() + " ";
                }
                else
                {
                    print += "| ";
                }
            }
            print += "\n";
        }
        print += "-------";
        for (int i = 0; i < 3; i++)
        {
            while (tmp[i].Count > 0)
            {
                stack[i].Push(tmp[i].Pop());
            }
        }
        Debug.Log(print);
    }
    void Move(int n, int start, int to)
    {
        // Debug.Log($"[{n}] {start} => {to}");
        stack[to-1].Push(stack[start-1].Pop());
        ShowHanoi();
    }

    public void Hanio(int n, int start, int to, int via)
    {
        if (n == 1)
        {
            Move(n, start, to);
        }
        else
        {
            Hanio(n-1, start, via, to);
            Move(n, start, to);
            Hanio(n-1, via, to, start);
        }
    }
}
