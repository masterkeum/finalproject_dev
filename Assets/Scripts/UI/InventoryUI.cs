using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("UISlots")]
    public ItemSlotUI weaponSlot = new ItemSlotUI();
    public ItemSlotUI helmetSlot = new ItemSlotUI();
    public ItemSlotUI gloveSlot = new ItemSlotUI();
    public ItemSlotUI bootsSlot = new ItemSlotUI();
    public ItemSlotUI armorSlot = new ItemSlotUI();
    public ItemSlotUI shieldSlot = new ItemSlotUI();


    private void Start()
    {
       
        
    }
    
    private void UpdateUI()
    {
        if (weaponSlot != null)
        {
            string imagePath = AccountInfo.Instance.equipItems["Weapon"].ImageFile;
            Resources.Load(imagePath);
           
            weaponSlot.transform.GetChild(1).gameObject.SetActive(true);
            //weaponSlot.transform.GetChild(1).GetComponent<Image>().sprite = 

        }
    }

    private Sprite LoadSpriteFromAsset(string path)
    {
#if UNITY_EDITOR
        return UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);
#else

#endif
    }



}