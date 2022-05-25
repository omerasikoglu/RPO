using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpellContainerUI : MonoBehaviour {

    [SerializeField] private SpellListSO spellList;
    [SerializeField] private Transform spellTemplate;
    [SerializeField] private List<Transform> spellCoordList;

    private Dictionary<SpellTypeSO, Transform> spellTypeTransformDic;

    public void Awake() {

        spellTypeTransformDic = new Dictionary<SpellTypeSO, Transform>();

        spellTemplate.gameObject.SetActive(false);

        foreach (var spellType in spellList.List) {
            Transform spellTransform = Instantiate(spellTemplate, transform);

            spellTransform.gameObject.SetActive(true);
            spellTransform.GetComponent<Image>().sprite = spellType.Sprite;

            spellTypeTransformDic[spellType] = spellTransform;
        }

        int i = 0;
        foreach (var transform in spellTypeTransformDic.Values) {
            transform.position = spellCoordList[i].position;
            i++;
        }


    }
}
