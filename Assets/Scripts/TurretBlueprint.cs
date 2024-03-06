
using UnityEngine;
using System.Collections;

[System.Serializable] //inspector에서 이 요소들을 설정하고 싶을때 써줘야함. 
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;
    public GameObject upgradedPrefab;
    public int upgradedCost;

    public int GetSellAmount()
    {

        return cost / 2;
    }

}
