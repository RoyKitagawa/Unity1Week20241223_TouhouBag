using UnityEngine;

public enum CharacterType
{
    None,
    Player,
    EnemyNormal,
    EnemyBoss,
}

public enum CharacterName
{
    None,
    Player,
    EnemyA,
    EnemyBossChiruno
}

public interface CharacterDataBase
{
    public CharacterType Type { get { return GetType(); } }
    public CharacterName Name { get { return GetName(); } }
    public float MaxLife { get { return GetMaxLife(); } }
    public Vector2 Velocity { get { return GetVelocity(); } }
    public float Cooldown { get { return GetCooldown(); } }
    public float AttackDamage { get { return GetAttackDamage(); } }

    public CharacterType GetType();
    public CharacterName GetName();
    public float GetMaxLife();
    public Vector2 GetVelocity();
    public float GetCooldown();
    public float GetAttackDamage();

    // public CharacterDataBase()
    // {
    //     // キャラクター識別関連
    //     Type = CharacterType.None;
    //     Name = CharacterName.None;
    //     // キャラクターの強さ関連
    //     MaxLife = 10;
    //     Cooldown = 1.0f;
    //     AttackDamage = 1.0f;
    //     // 移動速度関連
    //     Velocity = Vector2.zero;
    // }

    // public static CharacterName GetRandomCharacterName(CharacterType characterType)
    // {
    //     // switch(characterType)
    //     // {
    //     //     case 
    //     // }
    //     return CharacterName.None;

    // }
}
