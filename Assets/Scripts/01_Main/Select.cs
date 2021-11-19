using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum Type { tower, skill }
public class Select : MonoBehaviour
{

    private Type type = Type.tower;

    public TextMeshProUGUI text_type;
    public GameObject prefabGrid;
    public GameObject selectedTowerGrid;
    public GameObject selectedSkillGrid;
    public GameObject typeBtnGroup;
    public Button selectBtn;
    public Button startBtn;

    public TextMeshProUGUI text_name;
    public TextMeshProUGUI text_price;
    public TextMeshProUGUI text_content;
    public Image bigImage;

    private TextMeshProUGUI text_btnState;
    private Button[] typeBtn;
    private SelectedSlot[][] selectedSlots=new SelectedSlot[2][];
    private Slot[] slots;
    private int currentSlotIndex;
    private int selectedItemCnt;//���õ� ������ ī��Ʈ 8�̸� ���۰��ɻ���

    private GameObject backPanel;
    private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        backPanel = gameObject.transform.GetChild(0).gameObject;
        panel = gameObject.transform.GetChild(1).gameObject;

        text_btnState = selectBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        startBtn.onClick.AddListener(StartGameScene);
        typeBtn = typeBtnGroup.GetComponentsInChildren<Button>();
        typeBtn[0].onClick.AddListener(delegate { ClickTypeButton(Type.tower); });
        typeBtn[1].onClick.AddListener(delegate { ClickTypeButton(Type.skill); });

        selectBtn.onClick.AddListener(ClickSelectButton);
        selectedSlots[0] = selectedTowerGrid.GetComponentsInChildren<SelectedSlot>();
        selectedSlots[1] = selectedSkillGrid.GetComponentsInChildren<SelectedSlot>();
        for (int i=0; i<selectedSlots.Length; i++)
        {
            
            for(int j=0; j<selectedSlots[i].Length; j++)
            {
                selectedSlots[i][j].type = (Type)i;
                selectedSlots[i][j].idx = j;
                selectedSlots[i][j].itemIdx = -1;
                selectedSlots[i][j].OnDelete += OnDeleteSelectedSlot;

            }
        }

        slots=prefabGrid.GetComponentsInChildren<Slot>();
        for(int i=0; i<slots.Length; i++)
        {
            slots[i].idx = i;
        }
    }
    public void InitSelect()
    {
        backPanel.SetActive(true);
        panel.SetActive(true);

        currentSlotIndex = -1;
        selectedItemCnt = 0;
        type = Type.tower;
        LoadSlots();
    }
    private void StartGameScene()
    {
        backPanel.SetActive(false);
        panel.SetActive(false);
        GameData.Instance.selectedTowers = new int[4];
        GameData.Instance.selectedSkills = new int[4];
        for (int i=0; i<4; i++)
        {
            GameData.Instance.selectedTowers[i] = selectedSlots[0][i].itemIdx;
            GameData.Instance.selectedSkills[i] = selectedSlots[1][i].itemIdx;
        }
       
    }

    public void ClickTypeButton(Type tp)
    {
        if (tp == type) return;
        type = tp;
        LoadSlots();


    }
    public void LoadSlots()
    {
        currentSlotIndex = -1;
        text_name.SetText("");
        text_price.SetText("");
        text_content.SetText("");
        bigImage.sprite = null;

        if (selectBtn.gameObject.activeSelf == true)
        {
            selectBtn.gameObject.SetActive(false);
        }

        List<int> idxs = new List<int>();
        for(int i=0; i<selectedSlots[(int)type].Length; i++)
        {
            idxs.Add(selectedSlots[(int)type][i].itemIdx);
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (idxs.Contains(slots[i].idx))
            {
                slots[i].SetSelect(true);
            }
            else
            {
                slots[i].SetSelect(false);
            }
            // slots[i]
            //���� Ÿ������ ���� ������Ʈ
        }
    }
    public void OnDeleteSelectedSlot(Type t, int itIdx)
    {
        selectedItemCnt--;
        if (type == t)
        {
            slots[itIdx].SetSelect(false);
            if(currentSlotIndex==itIdx)
                text_btnState.SetText("����");
        }
        if (selectedItemCnt < 8) { startBtn.gameObject.SetActive(false); }
        
    }
    public void ClickSelectButton()
    {

        for(int i=0; i< selectedSlots[(int)type].Length; i++)
        {
            if (selectedSlots[(int)type][i].itemIdx == currentSlotIndex) return;
        }

        for(int i=0; i< selectedSlots[(int)type].Length; i++)
        {
            if (selectedSlots[(int)type][i].itemIdx == -1)
            {

                selectedItemCnt++;
                selectedSlots[(int)type][i].SetSlot(currentSlotIndex, "Image/" + type.ToString() + "" + currentSlotIndex);
                slots[currentSlotIndex].SetSelect(true);
                text_btnState.SetText(slots[currentSlotIndex].selectText);
                if (selectedItemCnt >= 8) { startBtn.gameObject.SetActive(true); }
                break;
            }
        }
    }
    public void ClickSlot(Slot slot)
    {
        if (currentSlotIndex == slot.idx) return;
        currentSlotIndex = slot.idx;

        //text ������Ʈ
        text_name.text = slot.ItemName;
        text_content.text = slot.content;
        text_price.text = slot.price;
        Debug.Log("this slot image is    Image/" + type + "" + slot.idx);
        bigImage.sprite = Resources.Load<Sprite>("Image/"+ type.ToString() + ""+slot.idx);

        if (selectBtn.gameObject.activeSelf == false)
        {
            selectBtn.gameObject.SetActive(true);
        }
        text_btnState.SetText( slot.selectText);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}