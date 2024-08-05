using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public int currentSkin;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Skin[] skin;
    [SerializeField] private TMP_Text[] skinNames;
    [SerializeField] private TMP_Text[] skinPrices;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private Image[] icons;
    [SerializeField] private Image[] active;
    [SerializeField] private Button[] buttons;
    public int coins;



    public void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
        Refresh();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ActiveShop(bool state)
    {
        shopPanel.SetActive(state);

        if (state == true)
        {
            Load();
        }
        else
        {
            Save();
        }

       
        Refresh();
    }

    public void Refresh()
    {
       

        for (int i = 0; i < buttons.Length; i++)
        {
            skinNames[i].text = skin[i].nameSkin;
            skinPrices[i].text = skin[i].price.ToString() + "$";
            icons[i].sprite = skin[i].icon;

            if (skin[i].have == 1 || coins > skin[i].price)
            {
                active[i].enabled = true;
                buttons[i].interactable = true;

                if (skin[i].have ==1 && i != currentSkin)
                {
                    active[i].color = Color.yellow;
                }
                else if (skin[i].have == 1 && i == currentSkin)
                {
                    active[i].color = Color.green;
                }
                else if (skin[i].have == 0 && coins > skin[i].price)
                {
                    active[i].color = Color.white;
                }


            }
            else if (skin[i].have == 0 && coins < skin[i].price)
            {
                buttons[i].interactable = false;
                active[i].enabled = false;
            }

        }


       

        coinsText.text = coins.ToString() + "$";
        
    }

    public void SelectSkin(int index)
    {
        if (skin[index].have == 1)
        {
            currentSkin = index;
        }
        else
        {
            coins -= skin[index].price;
            skin[index].have = 1;
            currentSkin = index;
        }

        PlayerPrefs.SetInt("CurrentSkin", currentSkin); 

        Refresh();  
    } 

    private void Load()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            skin[i].have = PlayerPrefs.GetInt("SkinHave" + i);
        }

        currentSkin = PlayerPrefs.GetInt("CurrentSkin");
    }

    private void Save()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            PlayerPrefs.SetInt("SkinHave" + i, skin[i].have);
        }
        PlayerPrefs.SetInt("Coins", coins);
    }
}


[System.Serializable]

public class Skin
{
    public string nameSkin;
    public Sprite icon;
    public int price;
    public int have;

}
