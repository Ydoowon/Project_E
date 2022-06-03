using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapName : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text mapname;

    public void SetMapName(string text) => mapname.text = text;
}
