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
    // Start is called before the first frame update
    void Start()
    {
        selectText = "선택";
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { select.ClickSlot(this); });
    }
    public void SetSelect(bool value)
    {
       
        if (value)
        {
            selectText = "선택 완료";
            image.color = new Color(0.5f, 0.5f, 0.5f);

        }
        else
        {
            selectText = "선택";
            image.color = new Color(1, 1, 1);
        }

    }
    public void SetSlotImage(Sprite img)
    {
        image.sprite = img;
        image.preserveAspect = true;
    }
    public void SetSlot()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
