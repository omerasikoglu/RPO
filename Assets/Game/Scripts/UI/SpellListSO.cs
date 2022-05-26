using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SpellList")]
public class SpellListSO : ScriptableObject {

    [SerializeField] private List<SpellTypeSO> list; public List<SpellTypeSO> List => list;

    private Dictionary<SpellTypeSO, bool> activeSpellsDic;

    private void Awake() {

        activeSpellsDic = new Dictionary<SpellTypeSO, bool>();

        SetDic(); void SetDic() {

            foreach (var st in list) {

                activeSpellsDic[st] = st.IsActive;
            }
        }
    }


}


