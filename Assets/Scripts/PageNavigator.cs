using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PageNavigator : MonoBehaviour
{
    static PageNavigator instance;
    public static PageNavigator Instance { get { return instance; } }

    [Header("PageStore")]
    [SerializeField] PageStore pageStore;

    public Transform pageParent;

    List<Page> currentPages = new List<Page>();

   
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void NavAction(PageSet page)
    {
        if (pageParent.childCount > 0)
        {
            for (int i = 0; i < pageParent.childCount; i++)
            {
                GameObject child = pageParent.GetChild(i).gameObject;
                child.SetActive(false);
            }
        }

        Page currentPage = pageStore.GetPage(page);

        if (currentPages.Contains(currentPage))
        {
            string name = currentPage.panel.name;

            for (int i = 0; i < pageParent.childCount; i++)
            {
                GameObject child = pageParent.GetChild(i).gameObject;

                if (child.name.Contains(name))
                {
                    child.SetActive(true);
                    break;
                }
            }

            return;
        }

        GameObject go = Instantiate(currentPage.panel, pageParent);

        go.transform.localScale = Vector3.one;
        go.transform.position = Vector3.zero;

        currentPages.Add(currentPage);
    }

}
