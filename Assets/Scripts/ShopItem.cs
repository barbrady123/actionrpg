using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ShopItem : MonoBehaviour
{
    public int Cost;

    private bool _itemActive;

    public int MaxPurchases;

    protected virtual SFX PurchaseSFX { get; private set; } = SFX.HealthPickup;

    #region Description Text
    private string _description = $@"{{0}}

Cost: {{1}}

Left-Click To Purchase";
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_itemActive) return;        

        if (Input.GetMouseButtonDown(Global.Inputs.LeftButton))
        {
            if (PlayerController.Instance.RemoveCoins(this.Cost))
            {
                AudioManager.Instance.PlaySFX(this.PurchaseSFX);
                OnPurchase();

                if (this.MaxPurchases > 0)
                {
                    this.MaxPurchases--;
                    if (this.MaxPurchases == 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                AudioManager.Instance.PlaySFX(SFX.Beep);
                ShowDialog($"{Global.Labels.Shop}|NOT ENOUGH COINS!");
            }
        }
    }

    protected abstract void OnPurchase();

    protected void ShowDialog(string text) =>
        DialogManager.Instance.ShowDialog(
            new[] { text },
            clickToExit: false,
            blockMovement: false,
            blockFirstClick: false);

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (!obj.tag.IsPlayerTag())
            return;

        _itemActive = true;
        ShowDialog(String.Format($"{Global.Labels.Shop}|{_description}", this.gameObject.name, this.Cost));
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (!obj.tag.IsPlayerTag())
            return;

        _itemActive = false;
        DialogManager.Instance.HideDialog();
    }
}
