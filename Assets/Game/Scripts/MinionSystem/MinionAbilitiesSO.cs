using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MinionAbilities")]
public class MinionAbilitiesSO : ScriptableObject {

    public List<MinionAbilitySO> minionAbilities;

    private Dictionary<MinionAbilitySO, bool> activeAbilitiesDic;

    private void Awake() {

        activeAbilitiesDic = new Dictionary<MinionAbilitySO, bool>();

        SetDic(); void SetDic() {

            foreach (var st in minionAbilities) {

                activeAbilitiesDic[st] = st.IsActive;
            }
        }
    }


}


