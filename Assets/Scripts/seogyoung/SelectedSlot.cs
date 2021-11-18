using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSlot : MonoBehaviour
{

    public int idx;
    public int itemIdx;

    public Image image;
    public Button delBtn;


    // Start is called before the first frame update
    void Start()
    {
        delBtn.onClick.AddListener(ClickDelBtn);
    }
    public void SetSlot(int num, string str)
    {
        itemIdx = num;
        image.sprite = Resources.Load<Sprite>(str);

    }
    void ClickDelBtn()
    {
        itemIdx = -1;
        image.sprite = null;
    }
}
