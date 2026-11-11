namespace Team_Project.View
{
    partial class GameOverForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblGameOver; // "GAME OVER" 표시
        private Label lblStats;    // 플레이어의 레벨/스탯 요약
        private Button btnRestart; // "처음으로" 버튼

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        // ============================================================
        // InitializeComponent()
        // ------------------------------------------------------------
        //  - GameOverForm의 UI 구성 및 초기 배치 정의
        // ============================================================
        private void InitializeComponent()
        {
            lblGameOver = new Label();
            lblStats = new Label();
            btnRestart = new Button();
            SuspendLayout();
            // 
            // lblGameOver
            // 
            lblGameOver.Font = new Font("Consolas", 48F, FontStyle.Bold);
            lblGameOver.ForeColor = Color.Red;
            lblGameOver.Location = new Point(0, 150);
            lblGameOver.Name = "lblGameOver";
            lblGameOver.Size = new Size(1280, 150);
            lblGameOver.TabIndex = 0;
            lblGameOver.Text = "GAME OVER";
            lblGameOver.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStats
            // 
            lblStats.Font = new Font("Consolas", 20F);
            lblStats.ForeColor = Color.White;
            lblStats.Location = new Point(0, 350);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(1280, 200);
            lblStats.TabIndex = 1;
            lblStats.Text = "스탯 정보 표시";
            lblStats.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnRestart
            // 
            btnRestart.BackColor = Color.Black;
            btnRestart.FlatAppearance.BorderColor = Color.White;
            btnRestart.FlatAppearance.BorderSize = 2;
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.Font = new Font("Consolas", 20F, FontStyle.Bold);
            btnRestart.ForeColor = Color.White;
            btnRestart.Location = new Point(540, 600);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(200, 80);
            btnRestart.TabIndex = 2;
            btnRestart.Text = "처음으로";
            btnRestart.UseVisualStyleBackColor = false;
            btnRestart.Click += btnRestart_Click;
            // 
            // GameOverForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            ClientSize = new Size(1280, 720);
            Controls.Add(lblGameOver);
            Controls.Add(lblStats);
            Controls.Add(btnRestart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GameOverForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Game Over";
            ResumeLayout(false);
        }
    }
}