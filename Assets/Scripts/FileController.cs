using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FileController : MonoBehaviour
{
    [SerializeField]
    private PageController[] pages;

    [SerializeField]
    private PageController currentPage;

    [SerializeField]
    private int topSortLayer = 100;

    [ContextMenu("Set pages")]
    public void SetPages()
    {
        pages = GetComponentsInChildren<PageController>();
    }

    public void SetCurrentPage(PageController pageController)
    {
        if (currentPage)
        {
            currentPage.SetActive(false);
        }

        pageController.SetActive(true, topSortLayer);
        currentPage = pageController;
    }
}