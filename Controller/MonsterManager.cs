using Team_Project.Model;

namespace Team_Project.Controller
{
    // ============================================================
    // MonsterManager
    // ------------------------------------------------------------
    //  - 라운드 번호를 받아 그 라운드의 몬스터를 생성
    //  - 몬스터 능력치(밸런스)를 이 파일 한 곳에서 관리
    // ============================================================
    public static class MonsterManager
    {
        public const int FinalRound = 8;   // 마지막(보스) 라운드

        // 라운드를 받아와 그에 맞는 몬스터 객체를 반환 (switch expression)
        // 인자 순서: 이름, 라운드, HP, 공격력, 방어력, 보상 경험치, 기본/피격/사망 이미지
        public static Monster GetMonster(int round)
        {
            return round switch
            {
                1 => new Monster("초록 슬라임", round, 60, 12, 1, 20, @"Image\slime.gif", @"Image\slime_hit.png", @"Image\slime_die.gif"),
                2 => new Monster("파란 슬라임", round, 90, 16, 3, 25, @"Image\bubbling.gif", @"Image\bubbling_hit.png", @"Image\bubbling_die.gif"),
                3 => new Monster("골렘", round, 120, 20, 5, 30, @"Image\stone_golem.gif", @"Image\stone_golem_hit.png", @"Image\stone_golem_die.gif"),
                4 => new Monster("다크 골렘", round, 150, 28, 8, 40, @"Image\dark_stone_golem.gif", @"Image\dark_stone_golem_hit.png", @"Image\dark_stone_golem_die.gif"),
                5 => new Monster("늑대 전사", round, 180, 28, 8, 50, @"Image\warewolf.gif", @"Image\warewolf_hit.png", @"Image\warewolf_die.gif"),
                6 => new Monster("라이칸스로프", round, 220, 32, 10, 60, @"Image\lycanthrope.gif", @"Image\lycanthrope_hit.png", @"Image\lycanthrope_die.gif"),
                7 => new Monster("미노타우로스", round, 250, 35, 12, 70, @"Image\tauromasis.gif", @"Image\tauromasis_hit.png", @"Image\tauromasis_die.gif"),
                // 드래곤은 알맞은 사망 이미지가 없어 null (사망 시 이미지를 지움)
                FinalRound => new Monster("드래곤", round, 500, 55, 25, 150, @"Image\dragon.png", @"Image\dragon_hit.png", null),
                _ => throw new ArgumentOutOfRangeException(nameof(round), round, $"라운드는 1~{FinalRound} 사이여야 합니다.")
            };
        }
    }
}
