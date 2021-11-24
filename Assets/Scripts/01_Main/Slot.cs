using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{

    public int idx;
    public string ItemName;
    public string price;
    public string content;
    
    public Select select;

    public Image image;
    public string selectText;
    private Button button;
    private bool isEmpty;
    // Start is called before the first frame update
    void Start()
    {
        selectText = "선택";
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { 
            if(!isEmpty) select.ClickSlot(this); });
    }
    public void SetSelect(bool value)
    {
        if (isEmpty)return;
        if (value)
        {
            selectText = "선택 완료";
            image.color = new Color(0.5f, 0.5f, 0.5f,1);

        }
        else
        {
            selectText = "선택";
            image.color = new Color(1, 1, 1,1);
        }

    }
    public void SetSlotImage(Sprite img)
    {
        image.sprite = img;
        image.preserveAspect = true;
        if (img == null)
        {
            isEmpty = true;
            Color color = image.color;
            color.a = 0;
            image.color = color;
        }
        else
        {
            isEmpty = false;
            Color color = image.color;
            color.a = 1;
            image.color = color;
        }
    }
    public void SetSlot()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
