using UnityEngine;

public class CharacterDataPlayer : SimpleSingleton<CharacterDataPlayer>, CharacterDataBase
{
    // 識別情報
    CharacterType CharacterDataBase.GetType() { return CharacterType.Player; }
    CharacterName CharacterDataBase.GetName() { return CharacterName.Player; }
    // 強さ関連
    float CharacterDataBase.GetMaxLife() { return 1000.0f; }
    float CharacterDataBase.GetCooldown() { return 1.0f; } // 自機の場合使わない
    float CharacterDataBase.GetAttackDamage() { return 1.0f; } // 自機の場合使わない
    // 挙動関連
    Vector2 CharacterDataBase.GetVelocity() { return new Vector2(0.0f, 0.0f); } // 自機の場合使わない
}
