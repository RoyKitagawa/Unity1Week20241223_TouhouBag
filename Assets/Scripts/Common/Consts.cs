using UnityEngine;

public static class Consts
{
    public static class Tags
    {
        // バッグ編集画面
        public const string StageSlot = "StageSlot"; // ステージ生成時に配置されるスロット用。バッグ配置時に用いられる管理用タグ
        public const string StageSlotCell = "StageSlotCell"; // StageSlot用のセル。バッグの衝突にのみ対応する
        public const string Bag = "Bag"; // 「バッグ」のアイテム用
        public const string BagCell = "BagCell"; // バッグ用のセル。バッグの配置はStageSlotCellとの衝突で管理する
        public const string Item = "Item"; // バッグ以外のアイテム用
        public const string ItemCell = "ItemCell"; // バッグ以外のアイテム用のセル。アイテムの設置はBagCellとの衝突で管理する

        // バトル画面
        public const string BattlePlayerHome = "Battle_PlayerHome";
        public const string Bullet = "PCBullet"; // 範囲攻撃用
    }

    public static class Roots
    {
        public const string BagEdit = "Bag";
        public const string Battle = "Battle";
        // Area
        public const string BoxRoot = Battle + "/Areas/PlayerHome";
        // BagEdit
        public const string StageBorders = "StageBorders";
        public const string BagRoot = BagEdit;
        public const string BagItemsRoot = BagRoot + "/Items";
        public const string BagSlotRoot = BagRoot + "/Slots";
        // Battle
        public const string BattleWeapons = Battle + "/Weapons";
        public const string BattleItemList = Battle + "/Items";
        // Particle
        public const string ParticlesBagEdit = BagEdit + "/Particles";
        public const string ParticlesBattle = Battle + "/Particles";
    }

    public static class Resources
    {
        private const string _Prefabs = "Prefabs";

        public const string BattleItemListItemPrefab = _Prefabs + "/Items/ItemListItem";
        public const string BattleDamagePrefab = _Prefabs + "/Damage";

        // キャラクター関連
        public static class Character
        {
            private const string Characters = _Prefabs + "/Characters";
            // 敵機
            public const string EnemyWeakA = Characters + "/EnemyWeakA";
            public const string EnemyWeakB = Characters + "/EnemyWeakB";
            public const string EnemyWeakC = Characters + "/EnemyWeakC";
            public const string EnemyNormalD = Characters + "/EnemyNormalD";
            public const string EnemyNormalE = Characters + "/EnemyNormalE";
            public const string EnemyNormalF = Characters + "/EnemyNormalF";
            public const string EnemyStrongA = Characters + "/EnemyStrongA";
            public const string EnemyStrongB = Characters + "/EnemyStrongB";
            public const string EnemyStrongC = Characters + "/EnemyStrongC";
            public const string EnemyStrongD = Characters + "/EnemyStrongD";
            public const string EnemyStrongE = Characters + "/EnemyStrongE";
            public const string EnemyStrongF = Characters + "/EnemyStrongF";
            public const string EnemyBossChiruno = Characters + "/EnemyBossChiruno";
        }

        public static class Prefabs
        {
            // バッグ編集画面のアイテム
            public static class BagItems
            {
                // フォルダ
                private const string _Items = _Prefabs + "/Items"; //アイテム系共通
                private const string _BagItems = _Items + "/BagItems"; // バッグ編集画面アイテムフォルダ
                private const string _Cells = _Items + "/Cells"; // Cellフォルダ

                // 初期スロット
                public const string StageSlot = _BagItems + "/StageSlot";

                // セル
                public const string CellItem = _Cells + "/CellItem";
                public const string CellBag = _Cells + "/CellBag";
                public const string CellStageSlot = _Cells + "/CellStageSlot";

                // バッグアイテム
                public const string ItemBag1x1 = _BagItems + "/BagItem_Bag_1x1";
                public const string ItemBag2x1 = _BagItems + "/BagItem_Bag_2x1";
                public const string ItemBag3x1 = _BagItems + "/BagItem_Bag_3x1";
                public const string ItemBag2x2 = _BagItems + "/BagItem_Bag_2x2";

                // 通常アイテム
                public const string ItemApple = _BagItems + "/BagItem_Apple";
                public const string ItemApple4 = _BagItems + "/BagItem_Apple4";
                public const string ItemLong = _BagItems + "/BagItem_Long";
            }

            // 投てきする武器
            public static class ProjectileItems
            {
                private const string _Weapon = _Prefabs + "/Items/ProjectileWeapons";
                public const string Apple = _Weapon + "/ProjectileWeaponApple";
                public const string Apple4 = _Weapon + "/ProjectileWeaponApple4";
                public const string Long = _Weapon + "/ProjectileWeaponLong";
            }

            public static class Particles
            {
                private const string _Particles = "Particles";
                public const string Evolve = _Particles + "/ParticleEvolve";
                public const string Damage = _Particles + "/ParticleDamage";
                public const string Destroy = _Particles + "/ParticleBomb";
                public const string Heal = _Particles + "/ParticleHeal";
                public const string Shield = _Particles + "/ParticleShield";
                public const string BombExplode = _Particles + "/ParticleBombExplode";
                public const string FireFlowerExplode = _Particles + "/ParticleFireFlowerExplode";
            }

            public const string ItemDescription = _Prefabs + "/ItemDescription";
        }

        public static class Sprites
        {
            private const string _Images = "Images";
            private const string _Items = _Images + "/Items";

            // バトル中のクールダウン表記用
            public class BattleItem
            {
                private const string _ItemsThumb = _Images + "/ItemsThumb";
                public class Thumb
                {
                    // きゅうり
                    public const string CucumberLv1 = _ItemsThumb + "/ic_cucumber_lv1";
                    public const string CucumberLv2 = _ItemsThumb + "/ic_cucumber_lv2";
                    public const string CucumberLv3 = _ItemsThumb + "/ic_cucumber_lv3";
                    public const string CucumberLv4 = _ItemsThumb + "/ic_cucumber_lv4";
                    // ネジ
                    public const string ScrewLv1 = _ItemsThumb + "/ic_screw_lv1";
                    public const string ScrewLv2 = _ItemsThumb + "/ic_screw_lv2";
                    public const string ScrewLv3 = _ItemsThumb + "/ic_screw_lv3";
                    public const string ScrewLv4 = _ItemsThumb + "/ic_screw_lv4";
                    // スパナ                
                    public const string SpannerLv1 = _ItemsThumb + "/ic_spanner_lv1";
                    public const string SpannerLv2 = _ItemsThumb + "/ic_spanner_lv2";
                    public const string SpannerLv3 = _ItemsThumb + "/ic_spanner_lv3";
                    public const string SpannerLv4 = _ItemsThumb + "/ic_spanner_lv4";
                    // 爆弾
                    public const string BombLv1 = _ItemsThumb + "/ic_bomb_lv1";
                    public const string BombLv2 = _ItemsThumb + "/ic_bomb_lv2";
                    public const string BombLv3 = _ItemsThumb + "/ic_bomb_lv3";
                    public const string BombLv4 = _ItemsThumb + "/ic_bomb_lv4";
                    // ドライバー
                    public const string DriverLv1 = _ItemsThumb + "/ic_driver_lv1";
                    public const string DriverLv2 = _ItemsThumb + "/ic_driver_lv2";
                    public const string DriverLv3 = _ItemsThumb + "/ic_driver_lv3";
                    public const string DriverLv4 = _ItemsThumb + "/ic_driver_lv4";
                    // 河童キャノン
                    public const string CanonLv1 = _ItemsThumb + "/ic_canon_lv1";
                    public const string CanonLv2 = _ItemsThumb + "/ic_canon_lv2";
                    public const string CanonLv3 = _ItemsThumb + "/ic_canon_lv3";
                    public const string CanonLv4 = _ItemsThumb + "/ic_canon_lv4";
                    // 手袋
                    public const string GloveLv1 = _ItemsThumb + "/ic_glove_lv1";
                    public const string GloveLv2 = _ItemsThumb + "/ic_glove_lv2";
                    public const string GloveLv3 = _ItemsThumb + "/ic_glove_lv3";
                    public const string GloveLv4 = _ItemsThumb + "/ic_glove_lv4";
                }

                public const string StageSlot = _Items + "/StageSlot";
                // きゅうり
                public const string CucumberLv1 = _Items + "/kyuri1";
                public const string CucumberLv2 = _Items + "/kyuri2";
                public const string CucumberLv3 = _Items + "/kyuri3";
                public const string CucumberLv4 = _Items + "/kyuri4";
                // ネジ
                public const string ScrewLv1 = _Items + "/negi1";
                public const string ScrewLv2 = _Items + "/negi2";
                public const string ScrewLv3 = _Items + "/negi3";
                public const string ScrewLv4 = _Items + "/negi4";
                // スパナ                
                public const string SpannerLv1 = _Items + "/supana1";
                public const string SpannerLv2 = _Items + "/supana2";
                public const string SpannerLv3 = _Items + "/supana3";
                public const string SpannerLv4 = _Items + "/supana4";
                // 爆弾
                public const string BombLv1 = _Items + "/bakudan1";
                public const string BombLv2 = _Items + "/bakudan2";
                public const string BombLv3 = _Items + "/bakudan3";
                public const string BombLv4 = _Items + "/bakudan4";
                // ドライバー
                public const string DriverLv1 = _Items + "/doraibar1";
                public const string DriverLv2 = _Items + "/doraibar2";
                public const string DriverLv3 = _Items + "/doraibar3";
                public const string DriverLv4 = _Items + "/doraibar4";
                // 河童キャノン
                public const string CanonLv1 = _Items + "/kappakyanon1";
                public const string CanonLv2 = _Items + "/kappakyanon2";
                public const string CanonLv3 = _Items + "/kappakyanon3";
                public const string CanonLv4 = _Items + "/kappakyanon4";
                public const string CanonBullet = _Items + "/kappatama1";
                // 手袋
                public const string GloveLv1 = _Items + "/tebukuro1";
                public const string GloveLv2 = _Items + "/tebukuro2";
                public const string GloveLv3 = _Items + "/tebukuro3";
                public const string GloveLv4 = _Items + "/tebukuro4";

                public const string Bag2x2 = _Items + "/Bag_2x2";
            }

            public static class Cells
            {
                public const string Overlay = _Items + "/Overlay";
            }

            public static class Prices
            {
                private const string _Prices = _Images + "/Prices";
                public const string Price0 = _Prices + "/Price0";
                public const string Price1 = _Prices + "/Price1";
                public const string Price2 = _Prices + "/Price2";
                public const string Price3 = _Prices + "/Price3";
                public const string Price4 = _Prices + "/Price4";
                public const string Price5 = _Prices + "/Price5";
                public const string Price6 = _Prices + "/Price6";
                public const string Price7 = _Prices + "/Price7";
                public const string Price8 = _Prices + "/Price8";
                public const string Price9 = _Prices + "/Price9";
                public static string Price(int price)
                {
                    switch(price)
                    {
                        case 0:
                            return Price0;
                        case 1:
                            return Price1;
                        case 2:
                            return Price2;
                        case 3:
                            return Price3;
                        case 4:
                            return Price4;
                        case 5:
                            return Price5;
                        case 6:
                            return Price6;
                        case 7:
                            return Price7;
                        case 8:
                            return Price8;
                        case 9:
                            return Price9;
                        default:
                            Debug.LogError("非対応の金額です: " + price);
                            return Price9;
                    }
                }
            }

            public const string Box = _Images + "/mikanhako1";
            public const string FireFlowerParticle = "Images/FireFlowerParticle";
        }

        public static class Materials
        {
            public const string CoolDownWithGlow = "Materials/CooldownWithGlowMaterial";
        }

        public const string LevelSuffix1 = "_lv1";
        public const string LevelSuffix2 = "_lv2";
        public const string LevelSuffix3 = "_lv3";
        public const string LevelSuffix4 = "_lv4";
        public const string LevelSuffix5 = "_lv5";
    }

    public static class Names
    {
        public const string Overlay = "Overlay";
        public const string Pivot = "Pivot";
    }

    public static class SortingLayer
    {
        public const string BagSlot = "BagSlot";
        public const string SlotOverlay = "SlotOverlay";
        public const string Bag = "Bag";
        public const string BagOverlay = "BagOverlay";
        public const string Item = "Item";
        public const string ItemOverlay = "ItemOverlay";
        public const string DragItem = "DragItem";
        public const string BattleWeapon = "BattleWeapon";
        public const string UI = "UI";
        public const string UIOverlay = "OverlayUI";
    }

    public static class PlayerPrefs
    {
        public static class Keys
        {
            public const string ProgressData = "ProgressData";
            public const string VolumeMaster = "VolumeMaster";
            public const string VolumeSE = "VolumeSE";
            public const string VolumeBGM = "VolumeBGM";
            public const string GameSpeed = "GameSpeed";
        }
    }
}