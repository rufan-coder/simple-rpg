namespace Team_Project.View
{
    // ============================================================
    // ClearForm (게임 클리어 화면)
    // ------------------------------------------------------------
    //  - 최종 보스(드래곤)를 쓰러뜨리면 표시
    //  - 'retry?' 버튼: 시작 화면으로 복귀 / '끝내기' 버튼: 프로그램 종료
    //  - (예전에는 생성자에서 제목을 "GAME OVER"로 덮어써서 클리어인데 게임 오버로 보였음)
    // ============================================================
    public partial class ClearForm : GameForm
    {
        public ClearForm()
        {
            InitializeComponent();
            FocusManager.FocusEvent(btnRestart);
            FocusManager.FocusEvent(btnEnd);
        }

        private void btnEnd_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        // 시작 화면으로 돌아가고 이 화면은 닫음
        private void btnRestart_Click(object? sender, EventArgs e)
        {
            StartForm.ReturnToStart();
            CloseWithoutExit();
        }

        private void InitializeComponent()
        {
            lblGameClear = new Label();
            lblCongratulations = new Label();
            btnRestart = new Button();
            btnEnd = new Button();
            SuspendLayout();
            //
            // lblGameClear
            //
            lblGameClear.Font = new Font("Consolas", 48F, FontStyle.Bold);
            lblGameClear.ForeColor = Color.Cyan;
            lblGameClear.Location = new Point(0, 301);
            lblGameClear.Name = "lblGameClear";
            lblGameClear.Size = new Size(1280, 150);
            lblGameClear.TabIndex = 1;
            lblGameClear.Text = "GAME CLEAR";
            lblGameClear.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblCongratulations
            //
            lblCongratulations.Font = new Font("Consolas", 48F, FontStyle.Bold);
            lblCongratulations.ForeColor = Color.Cyan;
            lblCongratulations.Location = new Point(0, 151);
            lblCongratulations.Name = "lblCongratulations";
            lblCongratulations.Size = new Size(1280, 150);
            lblCongratulations.TabIndex = 2;
            lblCongratulations.Text = "★Congratulations★";
            lblCongratulations.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnRestart
            //
            btnRestart.BackColor = Color.Black;
            btnRestart.FlatAppearance.BorderColor = Color.White;
            btnRestart.FlatAppearance.BorderSize = 2;
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.Font = new Font("Consolas", 20F, FontStyle.Bold);
            btnRestart.ForeColor = Color.White;
            btnRestart.Location = new Point(412, 524);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(200, 80);
            btnRestart.TabIndex = 3;
            btnRestart.Text = "retry?";
            btnRestart.UseVisualStyleBackColor = false;
            btnRestart.Click += btnRestart_Click;
            //
            // btnEnd
            //
            btnEnd.BackColor = Color.Black;
            btnEnd.FlatAppearance.BorderColor = Color.White;
            btnEnd.FlatAppearance.BorderSize = 2;
            btnEnd.FlatStyle = FlatStyle.Flat;
            btnEnd.Font = new Font("Consolas", 20F, FontStyle.Bold);
            btnEnd.ForeColor = Color.White;
            btnEnd.Location = new Point(677, 524);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(200, 80);
            btnEnd.TabIndex = 4;
            btnEnd.Text = "끝내기";
            btnEnd.UseVisualStyleBackColor = false;
            btnEnd.Click += btnEnd_Click;
            //
            // ClearForm
            //
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            ClientSize = new Size(1280, 720);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(btnEnd);
            Controls.Add(btnRestart);
            Controls.Add(lblCongratulations);
            Controls.Add(lblGameClear);
            Name = "ClearForm";
            Text = "Game clear";
            ResumeLayout(false);
        }

        private Label lblCongratulations = null!;
        private Button btnRestart = null!;
        private Button btnEnd = null!;
        private Label lblGameClear = null!;
    }
}
