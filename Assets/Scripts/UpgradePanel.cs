using System;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private GameObject fireRate;
    [SerializeField] private GameObject bulletSize;
    [SerializeField] private GameObject damage;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject triple;
    [SerializeField] private GameObject single;

    private GameObject[] upgradeList;

    public void Start()
    {
        upgradeList = new GameObject[6];
        upgradeList[0] = fireRate;
        upgradeList[1] = bulletSize;
        upgradeList[2] = damage;
        upgradeList[3] = shotgun;
        upgradeList[4] = triple;
        upgradeList[5] = single;

        GenerateUpgrades();
    }

    public void GenerateUpgrades()
    {
        Transform panel = transform.GetChild(0);
        for (int j = 0; j < panel.childCount; j++)
        {
            GameObject oldUpgrade = panel.GetChild(j).gameObject;
            Destroy(oldUpgrade);
        }


        // Generates empty array and fills it with -1
        int[] arr = new int[3];
        for (int k = 0; k < 3; k++)
        {
            arr[k] = -1;
        }

        int[] picked = GenerateNumbers(arr, 0);

        // Instantiates each upgrade panel
        foreach (int i in picked)
        {
            GameObject upgrade = Instantiate(upgradeList[i], panel);
        }
        

    }
    private int[] GenerateNumbers(int[] picked, int i)
    {
        if (i == 3) return picked;
        int random = Mathf.RoundToInt(Random.Range(0, 6));
        Debug.Log(random);
        if (picked.Contains(random) == false)
        {
            picked[i] = random;
            i++;
        }
        return GenerateNumbers(picked, i);
    }
}
