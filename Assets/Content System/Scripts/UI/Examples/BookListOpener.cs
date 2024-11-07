using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookListOpener : MonoBehaviour
{
    public ManipulationType manipulationType;
    public void SetList(BookListController bookListController) => bookListController.ManipulationType = manipulationType;
}
