using Team_Project.Model;

namespace Team_Project.View
{
    // ============================================================
    // ItemForm (아이템 가방 대화상자)
    // ------------------------------------------------------------
    //  - 보유 아이템을 버튼으로 보여줌 (이름 x개수)
    //  - 고른 아이템을 SelectedItem에 담고 DialogResult.OK로 닫힘
    //    → 실제 사용은 BattleForm이 BattleManager에 요청
    //  - ESC: 선택 없이 닫기
    // ============================================================
    public partial class ItemForm : Form
    {
        public Item? SelectedItem { get; private set; }

        public ItemForm(List<Item> inventory)
        {
            InitializeComponent();

            Text = "아이템 가방";
            StartPosition = FormStartPosition.CenterParent;

            KeyPreview = true;
            KeyDown += ItemForm_KeyDown;

            GenerateItemButtons(inventory);
        }

        private void ItemForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;   // 대화상자 닫기 (취소)
            }
        }

        private void GenerateItemButtons(List<Item> inventory)
        {
            ItemPanel.Controls.Clear();

            if (inventory.Count == 0)
            {
                ItemText.Text = "가방이 비었습니다.";
                return;
            }

            foreach (Item item in inventory)
            {
                var btn = new Button
                {
                    Text = $"{item.Name} x{item.Count}",
                    Size = new Size(150, 80),
                    Margin = new Padding(5),
                    BackColor = Color.LightGreen
                };
                FocusManager.FocusEvent(btn, FocusManager.ButtonKind.OptionSelect);

                // 클릭: 선택한 아이템을 저장하고 OK로 닫기
                btn.Click += (sender, e) =>
                {
                    SelectedItem = item;
                    DialogResult = DialogResult.OK;
                };

                // 포커스·마우스가 올라가면 설명 표시
                btn.GotFocus += (sender, e) => ItemText.Text = item.Description;
                btn.MouseEnter += (sender, e) => ItemText.Text = item.Description;

                ItemPanel.Controls.Add(btn);
            }
        }

        private void InitializeComponent()
        {
            ItemText = new TextBox();
            ItemPanel = new FlowLayoutPanel();
            SuspendLayout();
            //
            // ItemText
            //
            ItemText.BackColor = Color.Black;
            ItemText.Font = new Font("맑은 고딕", 16F);
            ItemText.ForeColor = Color.White;
            ItemText.Location = new Point(0, 315);
            ItemText.Margin = new Padding(0);
            ItemText.Multiline = true;
            ItemText.Name = "ItemText";
            ItemText.ReadOnly = true;
            ItemText.RightToLeft = RightToLeft.No;
            ItemText.Size = new Size(444, 105);
            ItemText.TabIndex = 1;
            ItemText.TabStop = false;
            //
            // ItemPanel
            //
            ItemPanel.Location = new Point(0, 0);
            ItemPanel.Name = "ItemPanel";
            ItemPanel.Size = new Size(444, 313);
            ItemPanel.TabIndex = 2;
            //
            // ItemForm
            //
            BackColor = Color.Black;
            ClientSize = new Size(444, 421);
            Controls.Add(ItemText);
            Controls.Add(ItemPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ItemForm";
            Text = "Inventory";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox ItemText = null!;
        private FlowLayoutPanel ItemPanel = null!;
    }
}
