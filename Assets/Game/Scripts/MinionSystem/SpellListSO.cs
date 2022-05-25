using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SpellList")]
public class SpellListSO : ScriptableObject {

    public List<SpellTypeSO> spellList;

    private Dictionary<SpellTypeSO, bool> activeSpellsDic;

    private void Awake() {

        activeSpellsDic = new Dictionary<SpellTypeSO, bool>();

        SetDic(); void SetDic() {

            foreach (var st in spellList) {

                activeSpellsDic[st] = st.IsActive;
            }
        }
    }


}


