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
        // BagEdit
        public const string StageBorders = BagEdit + "/StageBorders";
        public const string BagRoot = BagEdit + "/BagArea/Bag";
        public const string BagItemsRoot = BagRoot + "/Items";
        public const string BagSlotRoot = BagRoot + "/Slots";
        // Battle
        public const string BattleItemList = Battle + "/Items";
    }

    public static class Resources
    {
        private const string Prefabs = "Prefabs";
        // public const string BagSlot = "Prefabs/StageSlot";
        // public const string CellItem = "Prefabs/CellItem";
        // public const string CellBag = "Prefabs/CellBag";
        // public const string CellStageSlot = "Prefabs/CellStageSlot";

        public const string BattleItemListItemPrefab = Prefabs + "/ItemListItem";
        public const string BattleDamagePrefab = Prefabs + "/Damage";

        // アイテム関連
        public static class BagItem
        {
            private const string Items = Prefabs + "/Items";
            private const string Cells = Items + "/Cells";
            // 初期スロット
            public const string StageSlot = Items + "/StageSlot";
            // バッグアイテム
            public const string ItemBag1x1 = Items + "/BagItem_Bag_1x1";
            public const string ItemBag2x1 = Items + "/BagItem_Bag_2x1";
            public const string ItemBag3x1 = Items + "/BagItem_Bag_3x1";
            public const string ItemBag2x2 = Items + "/BagItem_Bag_2x2";
            // 通常アイテム
            public const string ItemApple = Items + "/BagItem_Apple";
            public const string ItemApple4 = Items + "/BagItem_Apple4";
            public const string ItemLong = Items + "/BagItem_Long";
            // セル
            public const string CellItem = Cells + "/CellItem";
            public const string CellBag = Cells + "/CellBag";
            public const string CellStageSlot = Cells + "/CellStageSlot";
        }

        // キャラクター関連
        public static class Character
        {
            private const string Characters = Prefabs + "/Characters";
            // 敵機
            public const string EnemyA = Characters + "/EnemyA";
        }

    }
    
    public static class Sprites
    {
        public const string ItemAppleBattle = "Assets/Images/ItemCardA.png";
        public const string ItemAppleBagEdit = "Assets/Images/ItemCardA.png";
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
    }
}