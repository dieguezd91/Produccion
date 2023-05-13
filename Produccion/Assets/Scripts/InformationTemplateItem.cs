using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreMenu", menuName = "Templates/DataItemStore")]
public class InformationTemplateItem : ScriptableObject
{
    public string objectName;
    public Sprite image;
    public int price;
}
