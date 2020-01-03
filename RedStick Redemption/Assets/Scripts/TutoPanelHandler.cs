using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutoPanelHandler : MonoBehaviour
{
    private int counter;
    private int numberOfPanel;
    public Button rightArrow;
    public Button leftArrow;
    private Object[] panelList;
    private GameObject currentPanel;

    public static bool isInTutorial;

    // Start is called before the first frame update
    void Start()
    {
        isInTutorial = true;

        panelList = Resources.LoadAll("TutoPanel", typeof(GameObject));
        numberOfPanel = panelList.Length - 1;
        counter = 0;

        leftArrow.gameObject.SetActive(false);

        currentPanel = Instantiate((GameObject)panelList[0]);
        currentPanel.transform.SetParent(transform);
        currentPanel.transform.SetSiblingIndex(1);
        currentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LeftArrowClicked()
    {
        if (counter == numberOfPanel)
            rightArrow.gameObject.SetActive(true);

        if (counter != 0)
            counter--;

        if (counter == 0)
            leftArrow.gameObject.SetActive(false);

        Destroy(currentPanel);
        currentPanel = Instantiate((GameObject)panelList[counter]);
        currentPanel.transform.SetParent(transform);
        currentPanel.transform.SetSiblingIndex(1);
        currentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void RightArrowClicked()
    {
        if (counter == 0)
            leftArrow.gameObject.SetActive(true);

        if (counter != numberOfPanel)
            counter++;

        if (counter == numberOfPanel)
            rightArrow.gameObject.SetActive(false);

        Debug.Log(counter);
        Destroy(currentPanel);
        currentPanel = Instantiate((GameObject)panelList[counter]);
        currentPanel.transform.SetParent(transform);
        currentPanel.transform.SetSiblingIndex(1);
        currentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void CloseClicked()
    {
        GameObject.Find("Main Camera").AddComponent<Camera_Follow>().target = GameObject.Find("Stickman").transform;
        isInTutorial = false;

        Destroy(gameObject);
    }
}
