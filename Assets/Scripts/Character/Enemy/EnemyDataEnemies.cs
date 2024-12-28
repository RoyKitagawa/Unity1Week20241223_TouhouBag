using UnityEngine;

/// <summary>
/// モブA
/// </summary>
public class CharacterDataEnemyA : CharacterDataBase
{
    // 識別情報
    CharacterType CharacterDataBase.GetType() { return CharacterType.EnemyNormal; }
    CharacterName CharacterDataBase.GetName() { return CharacterName.EnemyA; }
    // 敵の強さ関連
    float CharacterDataBase.GetMaxLife() { return 50.0f; }
    float CharacterDataBase.GetCooldown() { return 1.0f; }
    float CharacterDataBase.GetAttackDamage() { return 5.0f; }
    // 敵の挙動関連
    Vector2 CharacterDataBase.GetVelocity() { return new Vector2(-2.5f, 0.0f); }
}

/// <summary>
/// ボスチルノ
/// </summary>
public class CharacterDataEnemyBossChiruno : CharacterDataBase
{
    // 識別情報
    CharacterType CharacterDataBase.GetType() { return CharacterType.EnemyBoss; }
    CharacterName CharacterDataBase.GetName() { return CharacterName.EnemyBossChiruno; }
    // 敵の強さ関連
    float CharacterDataBase.GetMaxLife() { return 500.0f; }
    float CharacterDataBase.GetCooldown() { return 3.0f; }
    float CharacterDataBase.GetAttackDamage() { return 99.0f; }
    // 敵の挙動関連
    Vector2 CharacterDataBase.GetVelocity() { return new Vector2(-1.5f, 0.0f); }
}
