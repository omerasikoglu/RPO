using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : Singleton<MinionManager> {

    public event EventHandler<OnActiveRoadChangedEventArgs> OnActiveRoadChanged;

    public class OnActiveRoadChangedEventArgs : EventArgs {
        public Road road;
    }



}
