using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Minion {

    public static Rock Create() {

        //TODO: GetFromObjectPool

        Rock rock = new Rock();
        return rock;
    }

}
