using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="pageStore",menuName ="AppStore/Page")]
public class PageStore : ScriptableObject
{
    [Header("Pages")]
    [SerializeField] List<Page> pages = new List<Page>();
    
    public Page GetPage(PageSet page)
    {
        return pages.Find(x => x.pageSet == page);
    }

}

public enum PageSet {Splash,level,Game}


[System.Serializable]
public class Page
{
  public GameObject panel;
  public PageSet pageSet;
}
