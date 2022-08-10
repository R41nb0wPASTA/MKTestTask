using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [Header("User Data")]
    [SerializeField] private UserData userData;
    
    [Header("Scroll")]
    [SerializeField] private Scrollbar scrollBar;

    [Header("UI Elems")]
    [SerializeField] private GameObject starImg;
    [SerializeField] private GameObject starCounter;
    [SerializeField] private List<GameObject> blockCards;
    [SerializeField] private List<GameObject> blockButtons;
    
    private String[] keys = new String[] { "Dino",  "Num", "SolarSys", "Alphabet", "Alice", "Info", "Restore", "Left", "Right" };

    private bool isUpdatingScore = false;
    
    private GameObject leftScrollButt;
    private GameObject rightScrollButt;
    
    private void Awake()
    {
        SetButtonsListeners();
        SetScrollParams();
    }

    void Start()
    {
        scrollBar.value = 0f;
    }

    private void Update()
    {
        UpdateObjectColorAlpha(leftScrollButt, scrollBar.value);
        UpdateObjectColorAlpha(rightScrollButt, 1 - scrollBar.value);
    }

    private void UpdateObjectColorAlpha(GameObject go, float a)
    {
        var goColorTmp = go.GetComponent<Image>().color;
        goColorTmp.a = a;
        go.GetComponent<Image>().color = goColorTmp;
    }
    
    private void OnDinoBlockStart()
    {
        Debug.Log("Dino! + Score!");
        SetUserScore(userData.userScore + 3);
    }

    private void OnNumBlockStart()
    {
        Debug.Log("Num!");
    }
    
    private void OnSolarSysBlockStart()
    {
        Debug.Log("SolarSys!");
    }
    
    private void OnAlphabetBlockStart()
    {
        Debug.Log("Alphabet!");
    }
    
    private void OnAliceBlockStart()
    {
        Debug.Log("Alice!");
    }
    
    private void OnInfoClick()
    {
        Debug.Log("Info!");
    }
    
    private void OnRestoreClick()
    {
        Debug.Log("Restore!");
    }

    private void OnLeftScrollClick()
    {
        
        scrollBar.value = 0f;
    }
    
    private void OnRightScrollClick()
    {
        scrollBar.value = 1f;
    }
    
    private void SetButtonsListeners()
    {
        foreach (GameObject go in blockButtons)
        {
            string sKeyResult = keys.FirstOrDefault<string>(s=>go.name.Contains(s));

            switch (sKeyResult)
            {
                case "Dino":
                    go.GetComponent<Button>().onClick.AddListener(OnDinoBlockStart);
                    break;
                case "Num":
                    go.GetComponent<Button>().onClick.AddListener(OnNumBlockStart);
                    break;
                case "SolarSys":
                    go.GetComponent<Button>().onClick.AddListener(OnSolarSysBlockStart);
                    break;
                case "Alphabet":
                    go.GetComponent<Button>().onClick.AddListener(OnAlphabetBlockStart);
                    break;
                case "Alice":
                    go.GetComponent<Button>().onClick.AddListener(OnAliceBlockStart);
                    break;
                case "Info":
                    go.GetComponent<Button>().onClick.AddListener(OnInfoClick);
                    break;
                case "Restore":
                    go.GetComponent<Button>().onClick.AddListener(OnRestoreClick);
                    break;
                case "Left":
                    go.GetComponent<Button>().onClick.AddListener(OnLeftScrollClick);
                    break;
                case "Right":
                    go.GetComponent<Button>().onClick.AddListener(OnRightScrollClick);
                    break;
            }
        }
    }

    private void SetScrollParams()
    {
        leftScrollButt = blockButtons.Find(x => x.name == "ScrollLeftButton");
        rightScrollButt = blockButtons.Find(x => x.name == "ScrollRightButton");
    }
    
    public void SetUserScore(int score)
    {
        userData.userScore = score;
        UpdateUserScore();
    }
    
    private void UpdateUserScore()
    {
        if (!isUpdatingScore)
            StartCoroutine(UpdateScoreTextWithAnim());
    }

    private IEnumerator UpdateScoreTextWithAnim()
    {
        isUpdatingScore = true;

        TextMeshProUGUI scoreText = starCounter.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI scoreTextOutline = starCounter.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        
        while (int.Parse(scoreText.text) < userData.userScore)
        {
            scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
            scoreTextOutline.text = (int.Parse(scoreTextOutline.text) + 1).ToString();
            
            LeanTween.rotateZ(starImg, 180f, 1f);
            LeanTween.scale(starCounter, new Vector3(1.25f, 1.25f, 1.25f), 1f).setEaseOutBack();
            yield return new WaitForSeconds(1f);
            
            LeanTween.rotateZ(starImg, 360f, 1f);
            LeanTween.scale(starCounter, new Vector3(1f, 1f, 1f), 1f);
            yield return new WaitForSeconds(1f);
        }

        isUpdatingScore = false;
    }
    
    public void UpdateBlockLevelsInfo()
    {
        foreach (GameObject go in blockCards)
        {
            GameObject lvlsDone = go.transform.GetChild(4).GetChild(0).gameObject;
            GameObject lvlsTotal = go.transform.GetChild(4).GetChild(1).gameObject;

            string sKeyResult = keys.FirstOrDefault<string>(s=>go.name.Contains(s));

            switch (sKeyResult)
            {
                case "Dino":
                    lvlsDone.GetComponent<TextMeshProUGUI>().text = userData.completedLvlsDino.ToString();
                    lvlsTotal.GetComponent<TextMeshProUGUI>().text = userData.totalLvlsDino.ToString();
                    break;
                case "Num":
                    lvlsDone.GetComponent<TextMeshProUGUI>().text = userData.completedLvlsNum.ToString();
                    lvlsTotal.GetComponent<TextMeshProUGUI>().text = userData.totalLvlsNum.ToString();
                    break;
                case "SolarSys":
                    lvlsDone.GetComponent<TextMeshProUGUI>().text = userData.completedLvlsSolarSys.ToString();
                    lvlsTotal.GetComponent<TextMeshProUGUI>().text = userData.totalLvlsSolarSys.ToString();
                    break;
                case "Alphabet":
                    lvlsDone.GetComponent<TextMeshProUGUI>().text = userData.completedLvlsAlphabet.ToString();
                    lvlsTotal.GetComponent<TextMeshProUGUI>().text = userData.totalLvlsAlphabet.ToString();
                    break;
                case "Alice":
                    lvlsDone.GetComponent<TextMeshProUGUI>().text = userData.completedLvlsAlice.ToString();
                    lvlsTotal.GetComponent<TextMeshProUGUI>().text = userData.totalLvlsAlice.ToString();
                    break;
            }
        }
    }
}
