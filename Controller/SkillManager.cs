using Team_Project.Model;

namespace Team_Project.Controller
{
    // ============================================================
    // SkillManager
    // ------------------------------------------------------------
    //  - 직업별 스킬 원본 데이터를 보관
    //  - 직업과 레벨 조건에 맞는 스킬 목록을 제공
    // ============================================================
    public static class SkillManager
    {
        private static readonly List<Skill> allSkills = new List<Skill>()
        {
            // 전사 스킬
            new Skill("강타", 10, 1.5, "검을 강하게 휘둘러 강력한 피해를 입힙니다.", JobType.Warrior, 1),
            new Skill("회전베기", 20, 2.5, "몸을 회전하며 피해를 입힙니다.", JobType.Warrior, 3),
            new Skill("궁극의 일격", 50, 4.0, "전력을 다해 내리쳐 피해를 입힙니다.", JobType.Warrior, 5),

            // 마법사 스킬
            new Skill("파이어볼", 15, 2.0, "화염구로 강력한 피해를 입힙니다.", JobType.Mage, 1),
            new Skill("냉기 폭발", 25, 3.0, "주변에 냉기 폭발을 일으킵니다.", JobType.Mage, 3),
            new Skill("메테오", 50, 4.0, "거대 운석을 소환해 피해를 입힙니다.", JobType.Mage, 5),

            // 도적 스킬
            new Skill("급소 찌르기", 15, 2.0, "약점을 노려 2.0배의 피해를 입힙니다.", JobType.Rogue, 1),
            new Skill("새비지 블로우", 20, 2.5, "배후를 잡아서 기습 공격을 합니다.", JobType.Rogue, 3)
        };

        // 해당 직업이면서 현재 레벨 이하에서 배울 수 있는 스킬 목록을 반환
        public static List<Skill> GetAvailableSkills(JobType jobType, int playerLevel)
        {
            return allSkills
                .Where(skill => skill.JobLimit == jobType && skill.RequiredLevel <= playerLevel)
                .ToList();
        }
    }
}
