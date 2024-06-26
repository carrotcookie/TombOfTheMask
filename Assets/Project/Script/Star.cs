using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ScoreProp {
    UIStar uiStar;

    protected override void Component() {
        uiStar = FindObjectOfType<UIStar>();
    }

    protected override void RenewTopBarUI() {
        uiStar.RenewActiveStar();
        StageManager.Instance.IncreaseStars();
    }

    protected override void UpdateLogic() {
        
    }
}
