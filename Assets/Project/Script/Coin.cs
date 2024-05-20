using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ScoreProp {

    protected override void Component() {

    }

    protected override void RenewScore() {
        GameManager.Instance.IncreaseCoin();
        UIManager.Instance.IncreaseCoin();
    }

    protected override void UpdateLogic() {

    }
}
