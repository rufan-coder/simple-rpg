namespace Team_Project.View
{
    partial class StatusForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;   // 제목 (플레이어 이름)
        private Label lblStatus;  // 상태 요약

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Consolas", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Cyan;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(600, 100);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "플레이어 상태";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = new Font("Consolas", 18F);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(0, 100);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(600, 300);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "HP, MP, 공격력, 방어력 등";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StatusForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            ClientSize = new Size(600, 400);
            Controls.Add(lblStatus);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "StatusForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "플레이어 상태";
            ResumeLayout(false);
        }
    }
}