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

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { select.ClickSlot(this); });
    }

    public void SetSlot()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
