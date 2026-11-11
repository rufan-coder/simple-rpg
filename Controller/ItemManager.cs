using Team_Project.Model;

namespace Team_Project.Controller
{
    // ============================================================
    // ItemManager
    // ------------------------------------------------------------
    //  - 게임에 등장하는 아이템 원본 데이터(도감)를 보관
    //  - 아이템을 줄 때는 항상 복사본을 만들어 원본이 바뀌지 않게 함
    // ============================================================
    public static class ItemManager
    {
        public const int DropChancePercent = 70;   // 몬스터 처치 시 아이템 드롭 확률(%)

        // 모든 아이템을 모아놓은 리스트 (원본 데이터)
        private static readonly List<Item> allItems = new List<Item>()
        {
            new Item("빨간 포션", ItemType.HealthPotion, 50, "체력을 50 회복합니다."),
            new Item("주황 포션", ItemType.HealthPotion, 100, "체력을 100 회복합니다."),
            new Item("힘 포션", ItemType.BuffPotion, 10, "공격력이 10% 증가합니다."),
            new Item("파란 포션", ItemType.ManaPotion, 20, "마나를 20 회복합니다."),
            new Item("엘릭서", ItemType.ManaPotion, 50, "마나를 50 회복합니다.")
        };

        // 몬스터에게서 아이템 드롭용 메서드 (무작위 아이템의 복사본 반환)
        public static Item GetRandomDrop()
        {
            int index = Random.Shared.Next(allItems.Count);
            return allItems[index].Clone();
        }

        // 도감에서 이름으로 아이템을 찾아 복사본을 반환 (없으면 null)
        public static Item? GetItem(string name)
        {
            return allItems.Find(i => i.Name == name)?.Clone();
        }

        // 초반 아이템 지급 후, 지급한 아이템 목록을 문자열로 반환 (로그 출력용)
        public static string GiveStarterItems(Player player)
        {
            var starterItems = new (string Name, int Count)[]
            {
                ("빨간 포션", 3),
                ("파란 포션", 2)
            };

            var given = new List<string>();
            foreach (var (name, count) in starterItems)
            {
                Item? item = GetItem(name);
                if (item == null) continue;

                player.AddItem(item, count);
                given.Add($"[{name} x{count}]");
            }
            return string.Join(", ", given);
        }
    }
}
