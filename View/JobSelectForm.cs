using Team_Project.Controller;
using Team_Project.Model;

namespace Team_Project.View
{
    // ============================================================
    // JobSelectForm (직업 선택 화면)
    // ------------------------------------------------------------
    //  - 전사(Warrior), 마법사(Mage), 도적(Rogue) 중 선택
    //  - 선택한 직업의 Player 객체를 만들어 전투 화면으로 이동
    // ============================================================
    public partial class JobSelectForm : GameForm
    {
        public JobSelectForm()
        {
            InitializeComponent();
            KeyPreview = true;

            // 런타임에 버튼 스타일 및 위치 지정
            StyleButton(btnWarrior, "전사", 280);
            StyleButton(btnMage, "마법사", 540);
            StyleButton(btnRogue, "도적", 800);

            FocusManager.FocusEvent(btnWarrior);
            FocusManager.FocusEvent(btnMage);
            FocusManager.FocusEvent(btnRogue);

            // 포커스를 받으면 직업 설명 표시
            btnWarrior.GotFocus += (sender, e) => JobText.Text = Warrior.Description;
            btnMage.GotFocus += (sender, e) => JobText.Text = Mage.Description;
            btnRogue.GotFocus += (sender, e) => JobText.Text = Rogue.Description;

            // 클릭하면 해당 직업으로 게임 시작
            btnWarrior.Click += (sender, e) => StartGame(new Warrior());
            btnMage.Click += (sender, e) => StartGame(new Mage());
            btnRogue.Click += (sender, e) => StartGame(new Rogue());
        }

        // ============================================================
        // StyleButton()
        // ------------------------------------------------------------
        //  - 버튼 공통 스타일 적용
        //  - 폼 디자이너는 기본 배치만 담당하고, 시각적 스타일은 여기서 통일
        // ============================================================
        private void StyleButton(Button btn, string text, int x)
        {
            btn.Text = text;
            btn.Font = new Font("Consolas", 20, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = Color.Black;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.White;
            btn.FlatAppearance.BorderSize = 2;
            btn.Size = new Size(200, 100);
            btn.Location = new Point(x, 500);
            btn.Cursor = Cursors.Hand;
        }

        // ============================================================
        // StartGame()
        // ------------------------------------------------------------
        //  - 다형성: 매개변수는 Player 타입이지만
        //    실제로는 Warrior / Mage / Rogue 객체가 전달됨
        //  - 전투 BGM으로 바꾸고 전투 화면으로 이동
        // ============================================================
        private void StartGame(Player player)
        {
            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlayBGM("Battle", 50);
            NavigateTo(new BattleForm(player));
        }
    }
}
