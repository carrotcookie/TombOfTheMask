using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : ScoreProp {
    UICoin uiCoin;

    protected override void Component() {
        uiCoin = UIManager.Instance.uiCoin;
    }

    protected override void RenewTopBarUI() {
        uiCoin.RenewCoinCount();
    }

    protected override void UpdateLogic() {
        
    }
}
