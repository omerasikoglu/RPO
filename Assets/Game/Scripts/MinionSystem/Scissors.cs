using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : Minion {

    public static Scissors Create() {

        //TODO: GetFromObjectPool

        Scissors scissors = new Scissors();
        return scissors;
    }

}
