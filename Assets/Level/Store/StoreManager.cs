using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public List<Buff> storeItemsList = new List<Buff>();
    public List<int> storeCoinList = new List<int>();
    
    public List<Image> storeImageListField = new List<Image>();
    public List<TextMeshProUGUI> storeItemNameListField = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> storeItemCostListField = new List<TextMeshProUGUI>();

    private PlayerMove playerMove;
    
    public void OnEnable()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        playerMove.canMove = false;
        
        Time.timeScale = 0;

        SetUpStore();
    }

    private void OnDisable()
    {
        playerMove.canMove = true;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (storeCoinList[0] > PointsManager.instance.Coins) return;
            PointsManager.instance.Coins -= storeCoinList[0];
            Instantiate(storeItemsList[0], playerMove.transform.position, Quaternion.identity,
                FindObjectOfType<PlayerMove>().transform);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (storeCoinList[1] > PointsManager.instance.Coins) return;
            PointsManager.instance.Coins -= storeCoinList[1];
            Instantiate(storeItemsList[1], playerMove.transform.position, Quaternion.identity,
                FindObjectOfType<PlayerMove>().transform);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (storeCoinList[2] > PointsManager.instance.Coins) return;
            PointsManager.instance.Coins -= storeCoinList[2];
            Instantiate(storeItemsList[2], playerMove.transform.position, Quaternion.identity,
                FindObjectOfType<PlayerMove>().transform);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }

    public void SetUpStore()
    {
        for (int i = 0; i < 3; i++)
        {
            storeImageListField[i].sprite = storeItemsList[i].buffBadgePrefab.GetComponent<Image>().sprite;
            storeImageListField[i].color = storeItemsList[i].buffBadgePrefab.GetComponent<Image>().color;
            storeItemNameListField[i].text = storeItemsList[i].name;
            storeItemCostListField[i].text = storeCoinList[i].ToString();
        }
    }
}