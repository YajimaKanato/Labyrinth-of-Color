using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AreaData", menuName = "Scriptable Objects/AreaData")]
public class AreaDataList : ScriptableObject
{
    [SerializeField] List<GameObject> _areaDatas;
    public List<GameObject> AreaDatas { get { return _areaDatas; } }
}
