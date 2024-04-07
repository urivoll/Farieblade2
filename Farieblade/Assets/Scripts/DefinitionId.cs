using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitionId : MonoBehaviour
{
    private Dictionary<int, int> code = new Dictionary<int, int>
    {
        { 23 , 0 },
        { 24 , 1 },
        { 6 , 2 },
        { 7 , 3 },
        { 9 , 4 },
        { 14 , 5 },
        { 15 , 6 },
        { 17 , 7 },
        { 18 , 8 },
        { 19 , 9 },
        { 20 , 10 },
        { 21 , 11 },
        { 8 , 12 },
        { 16 , 13 },
        { 22 , 14 }
    };
    public int unCode(int obj)
    {
        return code[obj];
    }
}
