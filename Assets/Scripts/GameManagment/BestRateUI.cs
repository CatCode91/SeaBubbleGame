using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestRateUI : MonoBehaviour
{
    private Text label;

    // Start is called before the first frame update
    void Start()
    {
       label = GetComponent<Text>();
       label.text = FindObjectOfType<Player>().GetMaxResult().ToString();
    }
}
