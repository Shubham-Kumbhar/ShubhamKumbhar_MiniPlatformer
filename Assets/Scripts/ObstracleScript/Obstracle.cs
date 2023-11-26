using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstracle : MonoBehaviour
{

    //Base class for Obstracles
    public virtual void OnUsed()
    {
        SoundManager.instance.PlaySFX(GameManager.instance.dataManager.SFXBulletUsed);
    }
}
