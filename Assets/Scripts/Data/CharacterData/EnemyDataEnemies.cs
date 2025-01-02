using System.Text;
using UnityEngine;


public class CharacterDataEnemyBase : CharacterDataBase
{
    public static class Status
    {
        public const float StrongBuffer = 1.5f;
        public const float WeakBuffer = 1.2f;
        public static class Weak
        {
            public const float BaseLife = 12.0f;
            public const float BaseCoolDown = 1.2f;
            public const float BaseAttack = 9.0f;
            public const float BaseSpeed = -2.0f;
        }
        public static class Normal
        {
            public const float BaseLife = Weak.BaseLife * 1.75f;
            public const float BaseCoolDown = 1.3f;
            public const float BaseAttack = Weak.BaseLife * 1.75f;
            public const float BaseSpeed = -1.75f;
        }
        // 中ボス
        public static class Strong
        {
            public const float BaseLife = Normal.BaseLife * 3.5f;
            public const float BaseCoolDown = 1.75f;
            public const float BaseAttack = Normal.BaseLife * 2.5f;
            public const float BaseSpeed = -1.5f;
        }
        // 大ボス
        public static class Boss
        {
            public const float BaseLife = Strong.BaseLife * 3.5f;
            public const float BaseCoolDown = 2.0f;
            public const float BaseAttack = Strong.BaseLife * 2.5f;
            public const float BaseSpeed = -1.0f;
        }
    }


    // 識別情報
    public virtual CharacterType GetCharacterType() { return CharacterType.EnemyWeak; }
    public virtual CharacterName GetCharacterName() { return CharacterName.EnemyWeakA; }
    // 敵の強さ関連
    public virtual float GetMaxLife() { return Status.Weak.BaseLife * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    public virtual float GetCooldown() { return Status.Weak.BaseCoolDown; }
    public virtual float GetAttackDamage() { return Status.Weak.BaseAttack * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    // 敵の挙動関連
    public virtual Vector2 GetVelocity() { return new Vector2(Status.Weak.BaseSpeed, 0.0f); }

}

/// <summary>
/// 通常タイプ
/// </summary>
public class CharacterDataEnemyWeakA : CharacterDataEnemyBase {}

/// <summary>
/// パワータイプ
/// </summary>
public class CharacterDataEnemyWeakB : CharacterDataEnemyWeakA
{
    // 識別情報
    public override CharacterName GetCharacterName() { return CharacterName.EnemyWeakB; }
    // 敵の強さ関連
    // public override float GetMaxLife() { return 10f; }
    // public override float GetCooldown() { return 1.0f; }
    public override float GetAttackDamage() { return Status.Weak.BaseAttack * Status.StrongBuffer * ManagerGame.Instance.GetEnemyBuffRateWave(); }
}

/// <summary>
/// 体力タイプ
/// </summary>
public class CharacterDataEnemyWeakC : CharacterDataEnemyWeakA
{
    // 識別情報
    public override CharacterName GetCharacterName() { return CharacterName.EnemyWeakC; }
    // 敵の強さ関連
    public override float GetMaxLife() { return Status.Weak.BaseLife * Status.StrongBuffer * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    // public override float GetCooldown() { return 1.0f; }
    // public override float GetAttackDamage() { return 5.0f; }
}

// ====================================================================
// ちょいつよ一般兵
// ====================================================================

/// <summary>
/// 通常タイプ
/// </summary>
public class CharacterDataEnemyNormalD : CharacterDataEnemyWeakA
{
    // 識別情報
    public override CharacterType GetCharacterType() { return CharacterType.EnemyNormal; }
    public override CharacterName GetCharacterName() { return CharacterName.EnemyNormalD; }
    // 敵の強さ関連
    public override float GetMaxLife() { return Status.Normal.BaseLife * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    public override float GetCooldown() { return Status.Normal.BaseCoolDown; }
    public override float GetAttackDamage() { return Status.Normal.BaseAttack * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    // 敵の挙動関連
    public override Vector2 GetVelocity() { return new Vector2(Status.Normal.BaseSpeed, 0.0f); }
}

/// <summary>
/// パワータイプ
/// </summary>
public class CharacterDataEnemyNormalE : CharacterDataEnemyNormalD
{
    // 識別情報
    public override CharacterName GetCharacterName() { return CharacterName.EnemyNormalE; }
    // 敵の強さ関連
    // public override float GetMaxLife() { return 10f; }
    // public override float GetCooldown() { return 1.0f; }
    public override float GetAttackDamage() { return Status.Normal.BaseAttack * Status.StrongBuffer * ManagerGame.Instance.GetEnemyBuffRateWave(); }
}

/// <summary>
/// 体力タイプ
/// </summary>
public class CharacterDataEnemyNormalF : CharacterDataEnemyNormalD
{
    // 識別情報
    public override CharacterName GetCharacterName() { return CharacterName.EnemyNormalF; }
    // 敵の強さ関連
    public override float GetMaxLife() { return Status.Normal.BaseLife * Status.StrongBuffer * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    // public override float GetCooldown() { return 1.0f; }
    // public override float GetAttackDamage() { return 5.0f; }
}

// ====================================================================
// 中ボス系
// ====================================================================
public class CharacterDataEnemyStrongA : CharacterDataEnemyNormalD
{
    // 識別情報
    public override CharacterType GetCharacterType() { return CharacterType.EnemyMidBoss; }
    public override CharacterName GetCharacterName() { return CharacterName.EnemyStrongA; }
    // 敵の強さ関連
    public override float GetMaxLife() { return Status.Strong.BaseLife * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    public override float GetCooldown() { return Status.Strong.BaseCoolDown; }
    public override float GetAttackDamage() { return Status.Strong.BaseAttack * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    // 敵の挙動関連
    public override Vector2 GetVelocity() { return new Vector2(Status.Strong.BaseSpeed, 0.0f); }
}

/// <summary>
/// パワータイプ
/// </summary>
public class CharacterDataEnemyStrongB : CharacterDataEnemyStrongA
{
    // 識別情報
    public override CharacterName GetCharacterName() { return CharacterName.EnemyStrongB; }
    // 敵の強さ関連
    // public override float GetMaxLife() { return 10f; }
    // public override float GetCooldown() { return 1.0f; }
    public override float GetAttackDamage() { return Status.Strong.BaseAttack * Status.StrongBuffer * ManagerGame.Instance.GetEnemyBuffRateWave(); }
}

/// <summary>
/// 体力タイプ
/// </summary>
public class CharacterDataEnemyStrongC : CharacterDataEnemyStrongA
{
    // 識別情報
    public override CharacterName GetCharacterName() { return CharacterName.EnemyStrongC; }
    // 敵の強さ関連
    public override float GetMaxLife() { return Status.Strong.BaseLife * Status.StrongBuffer * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    // public override float GetCooldown() { return 1.0f; }
    // public override float GetAttackDamage() { return 5.0f; }
}

/// <summary>
/// 攻撃速度タイプ
/// </summary>
public class CharacterDataEnemyStrongD : CharacterDataEnemyStrongA
{
    // 識別情報
    public override CharacterName GetCharacterName() { return CharacterName.EnemyStrongD; }
    // 敵の強さ関連
    // public override float GetMaxLife() { return 10f; }
    public override float GetCooldown() { return Status.Strong.BaseCoolDown / Status.WeakBuffer; }
    // public override float GetAttackDamage() { return 5.0f; }
}

/// <summary>
/// 移動速度タイプ
/// </summary>
public class CharacterDataEnemyStrongE : CharacterDataEnemyStrongA
{
    // 識別情報
    public override CharacterName GetCharacterName() { return CharacterName.EnemyStrongE; }
    // 敵の強さ関連
    // public override float GetMaxLife() { return 10f; }
    // public override float GetCooldown() { return 1.0f; }
    // public override float GetAttackDamage() { return 5.0f; }
    // 敵の挙動関連
    public override Vector2 GetVelocity() { return new Vector2(Status.Strong.BaseSpeed * Status.WeakBuffer, 0.0f); }
}

/// <summary>
/// 全体的に少し強いタイプ
/// </summary>
public class CharacterDataEnemyStrongF : CharacterDataEnemyStrongA
{
    // 識別情報
    public override CharacterName GetCharacterName() { return CharacterName.EnemyStrongF; }
    // 敵の強さ関連
    public override float GetMaxLife() { return Status.Strong.BaseLife * Status.WeakBuffer * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    public override float GetCooldown() { return Status.Strong.BaseCoolDown / Status.WeakBuffer * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    public override float GetAttackDamage() { return Status.Strong.BaseAttack * Status.WeakBuffer * ManagerGame.Instance.GetEnemyBuffRateWave(); }
}


// ====================================================================
// 大ボス系
// ====================================================================

/// <summary>
/// ボスチルノ
/// </summary>
public class CharacterDataEnemyBossChiruno : CharacterDataEnemyBase
{
    // 識別情報
    public override CharacterType GetCharacterType() { return CharacterType.EnemyFinalBoss; }
    public override CharacterName GetCharacterName() { return CharacterName.EnemyStrongF; }
    // 敵の強さ関連
    public override float GetMaxLife() { return Status.Boss.BaseLife * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    public override float GetCooldown() { return Status.Boss.BaseCoolDown; }
    public override float GetAttackDamage() { return Status.Boss.BaseAttack * ManagerGame.Instance.GetEnemyBuffRateWave(); }
    // 敵の挙動関連
    public override Vector2 GetVelocity() { return new Vector2(Status.Boss.BaseSpeed, 0.0f); }
}
