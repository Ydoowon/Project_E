using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M_shopslot : MonoBehaviour
{
    public List<GameObject> BT;
    public List<TMPro.TMP_Text> Goldtext;

    int i = 0;

    M_imageset[] MySales;
    // Start is called before the first frame update
    void Start()
    {
        MySales =  this.GetComponentsInChildren<M_imageset>();
        for(int i =0; i<6; i++)
        {
            Goldtext[i].text = 0.ToString();
        }
    }


    public void DestoryBT(int index)
    {
        if (BT.Count - 1 < index || Goldtext.Count - 1 < index) return;
            
        BT[index].SetActive(false);
        Goldtext[index].text = "0";
        MySales[index].myItem.ableDrag = true;
        PlayerstatManagement_L.instance.GetComponent<UIManager_L>().AddItem(MySales[index].myItem,1, MySales[index].myItem.Price);
        Destroy(MySales[index].myItem.gameObject);
    }

    public void ActiveBT(int index)
    {
        if (BT.Count - 1 < index) return;
        BT[index].SetActive(true);
    }

    public void GoldReset(int index)
    {
        GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent.parent) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
        i = index;
    }
    public void Write(int tempGold)
    {
        if (i > 5) return;
        Goldtext[i].text = tempGold.ToString();
    }


    // Update is called once per frame
    void Update()
    {        
    }
}
