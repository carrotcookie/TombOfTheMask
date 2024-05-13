using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ScoreProp {
    UICoin uiCoin;

    protected override void Component() {
        uiCoin = UIManager.Instance.uiCoin;
    }

    protected override void RenewTopBarUI() {
        uiCoin.RenewCoinCount();
        StageManager.Instance.IncreaseCoins();
    }

    protected override void UpdateLogic() {

    }
}
