using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffectScript : MonoBehaviour
{
    // 내 오브젝트가 사라질 때 나타날 효과이다.
    public GameObject HitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit()
    {
        // 내 오브젝트가 있는 곳에 HitEffect 게임 오브젝트를 스폰한다.
        GameObject.Instantiate(HitEffect, this.transform.position, Quaternion.identity);
        // 내 오브젝트를 삭제한다.
        GameObject.Destroy(this.gameObject);
    }
}
