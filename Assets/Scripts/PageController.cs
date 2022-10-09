using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [Header("Sorting Layer")]
    [SerializeField]
    private int originalSortID = 0;

    [SerializeField]
    private SpriteRenderer[] spriteRenderers;

    [SerializeField]
    private TextMeshPro[] textMeshPros;

    [SerializeField]
    private GameObject[] contents;

    [Header("Colour")]
    [SerializeField]
    private Color color = Color.white;

    [Header("File Controller")]
    [SerializeField]
    private FileController fileController;

    private void Awake()
    {
        SetSortLayer();
        SetColour();
        fileController = GetComponentInParent<FileController>();
    }

    [ContextMenu("Find all texts")]
    public void FindAllTMPro()
    {
        List<TextMeshPro> temp = new List<TextMeshPro>(textMeshPros);
        temp.RemoveAll(item => item == null);
        foreach (TextMeshPro t in GetComponentsInChildren<TextMeshPro>())
        {
            if (!temp.Contains(t))
            {
                temp.Add(t);
            }
        }

        textMeshPros = temp.ToArray();

    }



    [ContextMenu("Set colour to renders")]
    public void SetColour()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = color;
        }
    }
    [ContextMenu("Set sorting Layer to renders")]
    public void SetSortLayer()
    {
        SetSortLayer(originalSortID);
    }
    
    public void SetSortLayer(int i)
    {
        int x = 0;
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            
            spriteRenderer.sortingOrder = i+x;
            x++;
        }

        foreach (TextMeshPro textMeshPro in textMeshPros)
        {
            textMeshPro.sortingOrder = i+1+x;
        }
    }

    public void SetActive(bool b, int i = 0)
    {
        foreach (GameObject g in contents)
        {
            g.SetActive(b);
        }

        if (b)
        {
            SetSortLayer(i);
        }
        else
        {
            SetSortLayer(originalSortID);
        }
    }

    public void SetAsCurrentPage()
    {
        if (fileController)
        {
            fileController.SetCurrentPage(this);
        }
        else
        {
            Debug.LogError("Missing File Controller");
        }
    }
}