using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSlot : MonoBehaviour
{
    public Type type;
    public int idx;
    public int itemIdx;

    public Image image;
    public Button delBtn;

    

    public delegate void DeleteHandler(Type type,int num);
    public event DeleteHandler OnDelete;
    

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
        if (itemIdx != -1)
        {
            
            OnDelete(type, itemIdx);
            itemIdx = -1;
            image.sprite = null;

        }
        
    }
}
