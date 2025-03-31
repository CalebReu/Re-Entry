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
    [SerializeField] private GameObject bulletSpeed;
    [SerializeField] private GameObject bulletSize;
    [SerializeField] private GameObject damage;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject triple;
    [SerializeField] private GameObject single;

    private GameObject[] upgradeList;

    public void Start()
    {
        upgradeList = new GameObject[7];
        upgradeList[0] = fireRate;
        upgradeList[1] = bulletSpeed;
        upgradeList[2] = bulletSize;
        upgradeList[3] = damage;
        upgradeList[4] = shotgun;
        upgradeList[5] = triple;
        upgradeList[6] = single;

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

        int[] arr = new int[3];
        int[] picked = GenerateNumbers(arr, 0);

        foreach (int i in picked)
        {
            GameObject upgrade = Instantiate(upgradeList[i], panel);
        }
        

    }
    private int[] GenerateNumbers(int[] picked, int i)
    {
        if (i == 3) return picked;
        int random = Mathf.RoundToInt(Random.Range(0, 7));
        if (picked.Contains(random) == false)
        {
            picked[i] = random;
            i++;
        }
        return GenerateNumbers(picked, i);
    }
}
