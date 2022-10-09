using System;
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

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        if (currentPage)
        {
            SetCurrentPage(currentPage);
        }
    }

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
        animator?.SetTrigger("Flip");
    }
}