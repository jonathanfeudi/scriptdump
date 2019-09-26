using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlueItemSelect : MonoBehaviour
{

    public BlueItemSelectionManager blueItemSelector;

    public int itemSelected;

    public Sprite item_1;
    public Sprite item_2;
    public Sprite item_3;
    public Sprite item_4;
    public Sprite item_5;

    // Start is called before the first frame update
    void Start()
    {
        itemSelected = blueItemSelector.itemIndex;
    }

    // Update is called once per frame
    void Update()
    {
        itemSelected = blueItemSelector.itemIndex;

        if (itemSelected == 1)
        {
            gameObject.GetComponent<Image>().sprite = item_1;
        }

        if (itemSelected == 2)
        {
            gameObject.GetComponent<Image>().sprite = item_2;
        }

        if (itemSelected == 3)
        {
            gameObject.GetComponent<Image>().sprite = item_3;
        }

        if (itemSelected == 4)
        {
            gameObject.GetComponent<Image>().sprite = item_4;
        }

        if (itemSelected == 5)
        {
            gameObject.GetComponent<Image>().sprite = item_5;
        }

    }
}
