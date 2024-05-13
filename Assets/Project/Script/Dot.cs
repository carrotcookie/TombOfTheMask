using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : ScoreProp {

    protected override void Component() {
        
    }

    protected override void RenewTopBarUI() {
        StageManager.Instance.IncreaseDots();
    }

    protected override void UpdateLogic() {
        
    }
}
