using UnityEngine;

public enum CharacterType
{
    None,
    Player,
    EnemyWeak,
    EnemyNormal,
    EnemyMidBoss,
    EnemyFinalBoss,
}

public enum CharacterName
{
    None,
    Player,
    // 一般兵
    EnemyWeakA,
    EnemyWeakB,
    EnemyWeakC,
    EnemyNormalD,
    EnemyNormalE,
    EnemyNormalF,
    // 中ボス
    EnemyStrongA,
    EnemyStrongB,
    EnemyStrongC,
    EnemyStrongD,
    EnemyStrongE,
    EnemyStrongF,
    // 大ボス
    EnemyBossChiruno
}

public interface CharacterDataBase
{
    public CharacterType Type { get { return GetCharacterType(); } }
    public CharacterName Name { get { return GetCharacterName(); } }
    public float MaxLife { get { return GetMaxLife(); } }
    public Vector2 Velocity { get { return GetVelocity(); } }
    public float Cooldown { get { return GetCooldown(); } }
    public float AttackDamage { get { return GetAttackDamage(); } }

    public abstract CharacterType GetCharacterType();
    public abstract CharacterName GetCharacterName();
    public abstract float GetMaxLife();
    public abstract Vector2 GetVelocity();
    public abstract float GetCooldown();
    public abstract float GetAttackDamage();

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
