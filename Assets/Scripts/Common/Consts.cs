public static class Consts
{
    public static class Tags
    {
        public const string StageSlot = "StageSlot"; // ステージ生成時に配置されるスロット用。バッグ配置時に用いられる管理用タグ
        public const string StageSlotCell = "StageSlotCell"; // StageSlot用のセル。バッグの衝突にのみ対応する
        public const string Bag = "Bag"; // 「バッグ」のアイテム用
        public const string BagCell = "BagCell"; // バッグ用のセル。バッグの配置はStageSlotCellとの衝突で管理する
        public const string Item = "Item"; // バッグ以外のアイテム用
        public const string ItemCell = "ItemCell"; // バッグ以外のアイテム用のセル。アイテムの設置はBagCellとの衝突で管理する
    }

    public static class Roots
    {
        public const string StageBorders = "StageBorders";
        public const string BagRoot = "BagArea/Bag";
        public const string BagItemsRoot = BagRoot + "/Items";
        public const string BagSlotRoot = BagRoot + "/Slots";
    }

    public static class Resources
    {
        public const string BagSlot = "Prefabs/StageSlot";
        public const string CellItem = "Prefabs/CellItem";
        public const string CellBag = "Prefabs/CellBag";
        public const string CellStageSlot = "Prefabs/CellStageSlot";
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