using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Road : int {
    road1 = 1,
    road2 = 2,
};
public class RoadManager : Singleton<RoadManager> {

    public event EventHandler<OnActiveRoadChangedEventArgs> OnActiveRoadChanged;

    public class OnActiveRoadChangedEventArgs : EventArgs {
        public Road activeRoad;
    }

    private Road activeRoad = (Road)1;

    public void SetActiveRoadType(Road road) {
        activeRoad = road;
        OnActiveRoadChanged?.Invoke(this, new OnActiveRoadChangedEventArgs { activeRoad = activeRoad });
    }

    public Road GetActiveRoadType => activeRoad;

    public bool CanSpawnMinion() { //place is occupied with mate
        return true;
    }

}
