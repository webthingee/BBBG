using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StoreManager : MonoBehaviour
{
    public bool canSelect;
    public GameObject controls;
    
    public Buff[] buffList;
    
    public List<Buff> storeItemsList = new List<Buff>();
    public List<int> storeCoinList = new List<int>();
    
    public List<Image> storeImageListField = new List<Image>();
    public List<TextMeshProUGUI> storeItemNameListField = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> storeItemCostListField = new List<TextMeshProUGUI>();

    private PlayerMove playerMove;

    public void OnEnable()
    {
        canSelect = false;
        SetUpStore();
        
        playerMove = FindObjectOfType<PlayerMove>();
        playerMove.canMove = false;
        
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        canSelect = false;
        Time.timeScale = 1;
        
        playerMove.nextMoveTime = Time.time + playerMove.moveInterval;
        playerMove.canMove = true;
    }

    private void Update()
    {
        if (!canSelect) return;
        
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
            canSelect = false;
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }

    private void SetUpStore()
    {
        StartCoroutine(StoreSetUp());
    }
    
    IEnumerator StoreSetUp()
    {
        Debug.Log("Store Setup...");
        controls.SetActive(false);
        yield return StartCoroutine(StoreRandomItems());
        yield return StartCoroutine(StoreSetupItems());
        yield return StartCoroutine(SelectionsAvailable());
        Debug.Log("... Store Is Ready");
    }

    IEnumerator StoreRandomItems()
    {
        storeItemsList.Clear();
        
        for (int i = 0; i < 3; i++)
        {
            storeItemsList.Add(buffList[Random.Range(0, buffList.Length)]);
        }

        yield return true;
    }
    
    IEnumerator StoreSetupItems()
    {
        storeCoinList.Clear();
            
        for (int i = 0; i < 3; i++)
        {
            storeImageListField[i].sprite = storeItemsList[i].buffBadgePrefab.GetComponent<Image>().sprite;
            storeImageListField[i].color = storeItemsList[i].buffBadgePrefab.GetComponent<Image>().color;
            
            storeItemNameListField[i].text = storeItemsList[i].title;
            
            storeCoinList.Add(Random.Range(2, LevelMaster.instance.level + 2 * 2));
            storeItemCostListField[i].text = storeCoinList[i].ToString();
        }

        yield return true;
    }

    IEnumerator SelectionsAvailable()
    {
        yield return new WaitForSecondsRealtime(1f);
        controls.SetActive(true);
        canSelect = true;
        //@TODO show arrows at this point, to make clear this is when you can select
    }
}