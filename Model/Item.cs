namespace Team_Project.Model
{
    public enum ItemType
    {
        HealthPotion,
        ManaPotion,
        BuffPotion
    }

    // ============================================================
    // Item (아이템)
    // ------------------------------------------------------------
    //  - 회복량 또는 버프량(Amount)과 보유 개수(Count)를 가짐
    // ============================================================
    public class Item
    {
        public string Name { get; }
        public ItemType Type { get; }
        public int Amount { get; }    // 회복량, 버프량(%)
        public string Description { get; }
        public int Count { get; set; } = 1;

        public Item(string name, ItemType type, int amount, string description)
        {
            Name = name;
            Type = type;
            Amount = amount;
            Description = description;
        }

        // 같은 정보를 가진 새 아이템을 만든다 (원본 데이터는 건드리지 않기 위해)
        public Item Clone() => new Item(Name, Type, Amount, Description);
    }
}
