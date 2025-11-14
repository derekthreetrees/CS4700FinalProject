using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ActivateTab(int tabNum)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }
        pages[tabNum].SetActive(true);
        tabImages[tabNum].color = Color.white;
    }
}
