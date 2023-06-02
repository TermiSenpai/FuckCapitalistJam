using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreakable
{
    public void breakItem(Vector3 dir);
    public void onBreakItem();
}
