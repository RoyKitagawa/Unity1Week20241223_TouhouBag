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
    }

    public static class Roots
    {
        public const string BagEdit = "BagEdit";
        public const string Battle = "Battle";
        // Area
        public const string BoxRoot = Battle + "/Areas/PlayerHome";
        // BagEdit
        public const string StageBorders = BagEdit + "/StageBorders";
        public const string BagRoot = BagEdit + "/BagArea/Bag";
        public const string BagItemsRoot = BagRoot + "/Items";
        public const string BagSlotRoot = BagRoot + "/Slots";
        // Battle
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
            public const string EnemyA = Characters + "/EnemyA";
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
            }
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
                    public const string CucumberLv1 = _ItemsThumb + "/Apple";
                    public const string CucumberLv2 = _ItemsThumb + "/Apple";
                    public const string CucumberLv3 = _ItemsThumb + "/Apple";
                    // ネジ
                    public const string ScrewLv1 = _ItemsThumb + "/Apple4";
                    public const string ScrewLv2 = _ItemsThumb + "/Apple4";
                    public const string ScrewLv3 = _ItemsThumb + "/Apple4";
                    // スパナ                
                    public const string SpannerLv1 = _ItemsThumb + "/Apple4";
                    public const string SpannerLv2 = _ItemsThumb + "/Apple4";
                    public const string SpannerLv3 = _ItemsThumb + "/Apple4";

                    public const string Apple = _ItemsThumb + "/Apple";
                    public const string Apple4 = _ItemsThumb + "/Apple4";
                    public const string Long = _ItemsThumb + "/Long";
                }

                public const string StageSlot = _Items + "/StageSlot";
                // きゅうり
                public const string CucumberLv1 = _Items + "/kyuri1";
                public const string CucumberLv2 = _Items + "/kyuri2";
                public const string CucumberLv3 = _Items + "/kyuri3";
                // ネジ
                public const string ScrewLv1 = _Items + "/negi1";
                public const string ScrewLv2 = _Items + "/negi2";
                public const string ScrewLv3 = _Items + "/negi3";
                // スパナ                
                public const string SpannerLv1 = _Items + "/supana1";
                public const string SpannerLv2 = _Items + "/supana2";
                public const string SpannerLv3 = _Items + "/supana3";

                public const string Bag2x2 = _Items + "/Bag_2x2";
            }

            public static class Cells
            {
                public const string Overlay = _Items + "/Overlay";
            }

            public const string Box = _Images + "/mikanhako1";
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
    }
}