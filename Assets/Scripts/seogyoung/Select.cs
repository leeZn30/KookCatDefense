using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Select : MonoBehaviour
{
    private string type = "tower";
    public TextMeshProUGUI text_type;
    public GameObject prefabGrid;
    public GameObject selectedGrid;
    public Button selectBtn;

    public TextMeshProUGUI text_name;
    public TextMeshProUGUI text_price;
    public TextMeshProUGUI text_content;
    public Image bigImage;


    private SelectedSlot[] selectedSlots;
    private Slot[] slots;
    private int currentSlotIndex;
    // Start is called before the first frame update
    void Start()
    {
        selectBtn.onClick.AddListener(ClickSelectButton);
        selectedSlots = selectedGrid.GetComponentsInChildren<SelectedSlot>();
        for(int i=0; i<selectedSlots.Length; i++)
        {
            selectedSlots[i].idx = i;
            selectedSlots[i].itemIdx = -1;
        }
        slots=prefabGrid.GetComponentsInChildren<Slot>();
        for(int i=0; i<slots.Length; i++)
        {
            slots[i].idx = i;
        }
    }
    public void LoadType()
    {

    }

    public void ClickSelectButton()
    {
        for(int i=0; i<selectedSlots.Length; i++)
        {
            if (selectedSlots[i].itemIdx == currentSlotIndex) return;
        }

        for(int i=0; i<4; i++)
        {
            if (selectedSlots[i].itemIdx == -1)
            {
                selectedSlots[i].SetSlot(currentSlotIndex, "Image/" + type + "" + currentSlotIndex);
                break;
            }
        }
    }
    public void ClickSlot(Slot slot)
    {
        Debug.Log("click" + slot.idx);
        if (currentSlotIndex == slot.idx) return;
        currentSlotIndex = slot.idx;

        //text 업데이트
        text_name.text = slot.ItemName;
        text_content.text = slot.content;
        text_price.text = slot.price;
        Debug.Log("Image/" + type + "" + slot.idx);
        bigImage.sprite = Resources.Load<Sprite>("Image/"+type+""+slot.idx);
  
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
