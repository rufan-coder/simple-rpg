namespace Team_Project.View
{
    partial class JobSelectForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;   // 제목 라벨 ("직업 선택")
        private Button btnWarrior; // 전사 버튼
        private Button btnMage;    // 마법사 버튼
        private Button btnRogue;   // 도적 버튼

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        // ============================================================
        // InitializeComponent()
        // ------------------------------------------------------------
        //  - 폼의 UI 구성 요소 초기화
        //  - 디자이너에서 생성되는 부분 (수정해도 실행 가능)
        // ============================================================
        private void InitializeComponent()
        {
            lblTitle = new Label();
            btnWarrior = new Button();
            btnMage = new Button();
            btnRogue = new Button();
            JobText = new TextBox();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Consolas", 36F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1280, 150);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "직업을 선택하세요";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnWarrior
            // 
            btnWarrior.Font = new Font("Consolas", 20F);
            btnWarrior.ForeColor = Color.White;
            btnWarrior.Location = new Point(280, 500);
            btnWarrior.Name = "btnWarrior";
            btnWarrior.Size = new Size(200, 100);
            btnWarrior.TabIndex = 1;
            btnWarrior.Text = "전사";
            // 
            // btnMage
            // 
            btnMage.Font = new Font("Consolas", 20F);
            btnMage.ForeColor = Color.White;
            btnMage.Location = new Point(540, 500);
            btnMage.Name = "btnMage";
            btnMage.Size = new Size(200, 100);
            btnMage.TabIndex = 2;
            btnMage.Text = "마법사";
            // 
            // btnRogue
            // 
            btnRogue.Font = new Font("Consolas", 20F);
            btnRogue.ForeColor = Color.White;
            btnRogue.Location = new Point(800, 500);
            btnRogue.Name = "btnRogue";
            btnRogue.Size = new Size(200, 100);
            btnRogue.TabIndex = 3;
            btnRogue.Text = "도적";
            // 
            // JobText
            // 
            JobText.BackColor = Color.Black;
            JobText.CausesValidation = false;
            JobText.Enabled = false;
            JobText.Font = new Font("맑은 고딕", 24F, FontStyle.Bold, GraphicsUnit.Point, 129);
            JobText.ForeColor = Color.WhiteSmoke;
            JobText.Location = new Point(280, 300);
            JobText.Margin = new Padding(0);
            JobText.Multiline = true;
            JobText.Name = "JobText";
            JobText.ReadOnly = true;
            JobText.RightToLeft = RightToLeft.No;
            JobText.Size = new Size(720, 161);
            JobText.TabIndex = 4;
            JobText.TabStop = false;
            // 
            // JobSelectForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            ClientSize = new Size(1280, 720);
            Controls.Add(JobText);
            Controls.Add(lblTitle);
            Controls.Add(btnWarrior);
            Controls.Add(btnMage);
            Controls.Add(btnRogue);
            Font = new Font("Consolas", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "JobSelectForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "직업 선택";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox JobText;
    }
}