using UnityEngine;

public class CharacterDataEnemyA : SimpleSingleton<CharacterDataEnemyA>, CharacterDataBase
{
    // 識別情報
    CharacterType CharacterDataBase.GetType() { return CharacterType.EnemyNormal; }
    CharacterName CharacterDataBase.GetName() { return CharacterName.EnemyA; }
    // 敵の強さ関連
    float CharacterDataBase.GetMaxLife() { return 10.0f; }
    float CharacterDataBase.GetCooldown() { return 1.0f; }
    float CharacterDataBase.GetAttackDamage() { return 1.0f; }
    // 敵の挙動関連
    Vector2 CharacterDataBase.GetVelocity() { return new Vector2(-2.5f, 0.0f); }
}
