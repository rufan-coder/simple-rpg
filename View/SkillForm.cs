using Team_Project.Model;

namespace Team_Project.View
{
    // ============================================================
    // SkillForm (스킬 선택 대화상자)
    // ------------------------------------------------------------
    //  - 배운 스킬을 버튼으로 보여주고, MP가 부족한 스킬은 비활성화
    //  - 고른 스킬을 SelectedSkill에 담고 DialogResult.OK로 닫힘
    //    → 실제 스킬 사용은 BattleForm이 BattleManager에 요청
    //  - ESC: 선택 없이 닫기
    // ============================================================
    public class SkillForm : Form
    {
        public Skill? SelectedSkill { get; private set; }

        public SkillForm(List<Skill> skills, int currentMp)
        {
            InitializeComponent();

            KeyPreview = true;
            KeyDown += SkillForm_KeyDown;

            Text = "특수 스킬 선택";
            StartPosition = FormStartPosition.CenterParent;

            GenerateSkillButtons(skills, currentMp);
        }

        private void SkillForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;   // 대화상자 닫기 (취소)
            }
        }

        private void GenerateSkillButtons(List<Skill> skills, int currentMp)
        {
            skillPanel.Controls.Clear();

            foreach (Skill skill in skills)
            {
                var btn = new Button
                {
                    Text = $"{skill.Name}\n(MP {skill.MpCost})",
                    Size = new Size(150, 80),
                    Margin = new Padding(5)
                };

                if (currentMp < skill.MpCost)
                {
                    // 마나 부족: 선택 불가
                    btn.Enabled = false;
                    btn.BackColor = Color.LightGray;
                }
                else
                {
                    btn.BackColor = Color.LightBlue;
                    FocusManager.FocusEvent(btn, FocusManager.ButtonKind.OptionSelect);

                    // 클릭: 선택한 스킬을 저장하고 OK로 닫기
                    btn.Click += (sender, e) =>
                    {
                        SelectedSkill = skill;
                        DialogResult = DialogResult.OK;
                    };
                }

                // 포커스·마우스가 올라가면 설명 표시, 마우스가 나가면 지움
                btn.GotFocus += (sender, e) => txtDescription.Text = skill.Description;
                btn.MouseEnter += (sender, e) => txtDescription.Text = skill.Description;
                btn.MouseLeave += (sender, e) => txtDescription.Text = "";

                skillPanel.Controls.Add(btn);
            }
        }

        private void InitializeComponent()
        {
            skillPanel = new FlowLayoutPanel();
            txtDescription = new TextBox();
            SuspendLayout();
            //
            // skillPanel
            //
            skillPanel.Location = new Point(0, 0);
            skillPanel.Name = "skillPanel";
            skillPanel.Size = new Size(501, 312);
            skillPanel.TabIndex = 0;
            //
            // txtDescription
            //
            txtDescription.BackColor = Color.Black;
            txtDescription.Font = new Font("맑은 고딕", 16F);
            txtDescription.ForeColor = Color.White;
            txtDescription.Location = new Point(0, 310);
            txtDescription.Margin = new Padding(0);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.RightToLeft = RightToLeft.No;
            txtDescription.Size = new Size(502, 113);
            txtDescription.TabIndex = 0;
            txtDescription.TabStop = false;
            //
            // SkillForm
            //
            BackColor = Color.Black;
            ClientSize = new Size(502, 423);
            Controls.Add(txtDescription);
            Controls.Add(skillPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SkillForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "스킬 선택";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtDescription = null!;
        private FlowLayoutPanel skillPanel = null!;
    }
}
