using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletCountUI;

    public void SetBulletCountUI(int currentBulletCount, int maxBulletCount, bool isReloading)
    {
        bulletCountUI.text = currentBulletCount.ToString("00") + "/" + maxBulletCount.ToString("00");
        if (isReloading == true) bulletCountUI.color = new Color(1, 1, 1, .5f);

        else bulletCountUI.color = new Color(1, 1, 1, 1);
    }
}
