using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ScoreProp {

    protected override void Component() {

    }

    protected override void RenewScore() {
        GameManager.Instance.IncreaseStar();
        UIManager.Instance.IncreaseStar();
    }

    protected override void UpdateLogic() {
        
    }
}
