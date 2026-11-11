using Team_Project.Model;

namespace Team_Project.View
{
    // ============================================================
    // StatusForm (상태 창)
    // ------------------------------------------------------------
    //  - 플레이어의 현재 능력치를 보여주는 대화상자
    //  - 전투 화면에서 ShowDialog로 띄우므로 닫으면 자동으로 전투 화면으로 돌아감
    //  - Esc 또는 X 버튼으로 닫기
    // ============================================================
    public partial class StatusForm : Form
    {
        public StatusForm(Player player)
        {
            InitializeComponent();

            KeyPreview = true;
            KeyDown += StatusForm_KeyDown;

            int bonusAttack = player.GetCurrentAttackPower() - player.AttackPower;

            lblTitle.Text = $"{player.Name}의 상태";
            lblStatus.Text = $"레벨 : {player.Level}\n" +
                             $"HP : {player.HP}/{player.MaxHP}\n" +
                             $"MP : {player.MP}/{player.MaxMP}\n" +
                             $"공격력 : {player.AttackPower} (+{bonusAttack})\n" +
                             $"공격력 버프 남은 턴 : {player.BuffTurn}\n" +
                             $"방어력 : {player.DefensePower}";
        }

        private void StatusForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
