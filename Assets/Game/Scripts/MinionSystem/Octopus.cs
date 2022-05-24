using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : Minion {
    public static Octopus Create() {

        //TODO: GetFromObjectPool

        Octopus octopus = new Octopus();
        return octopus;
    }

}
