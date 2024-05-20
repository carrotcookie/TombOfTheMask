using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : ScoreProp {

    protected override void Component() {
        
    }

    protected override void RenewScore() {
        GameManager.Instance.IncreaseDot();

    }

    protected override void UpdateLogic() {
        
    }
}
