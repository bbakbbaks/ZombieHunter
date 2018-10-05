using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletType
{
    string strName;
    int n_Dam;
    float n_lifeTime;
    int n_bulletSpeed;

    public string Name { get { return strName; } }
    public int Damage { get { return n_Dam; } }
    public float LifeTime { get { return n_lifeTime; } }
    public int BulletSpeed { get { return n_bulletSpeed; } }

    public BulletType(string name, int dam, float lifetime, int bulletspeed)
    {
        strName = name;
        n_Dam = dam;
        n_lifeTime = lifetime;
        n_bulletSpeed = bulletspeed;
    }
}

public class BulletManager : MonoBehaviour {

    public enum eBullet { normalb, powerb, specialb}
    public List<BulletType> m_bulletList = new List<BulletType>();

    public BulletType GetBullet(eBullet bullet)
    {
        return m_bulletList[(int)bullet];
    }
}
