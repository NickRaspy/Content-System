using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookListController : MonoBehaviour
{
    [SerializeField] private GameObject book;
    [SerializeField] private Transform bookHolder;

    public ManipulationType ManipulationType {  get; set; }
    public void LoadList()
    {

    }
}
