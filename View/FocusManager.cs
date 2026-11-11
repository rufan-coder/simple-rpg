namespace Team_Project.View
{
    // ============================================================
    // FocusManager
    // ------------------------------------------------------------
    //  - 키보드로 버튼을 이동할 때 선택된 버튼을 강조 표시하는 화면 도우미
    //  - 화면(View) 전용 기능이라 View 폴더에 둠
    //  - 버튼 종류는 호출하는 쪽에서 알려줌 (특정 Form 타입을 검사하지 않음)
    // ============================================================
    public static class FocusManager
    {
        public enum ButtonKind
        {
            BattleAction, // 직업 선택·전투·결과 화면 버튼 (강조: 흰 배경 + 검은 글자)
            OptionSelect  // 스킬·아이템 선택 버튼 (강조: 노란 배경)
        }

        // 버튼의 원래 색상과 종류를 기억해 두는 구조체 (버튼의 Tag에 저장)
        private readonly struct FocusContext
        {
            public FocusContext(ButtonKind kind, Color backColor, Color foreColor)
            {
                Kind = kind;
                OriginalBackColor = backColor;
                OriginalForeColor = foreColor;
            }

            public ButtonKind Kind { get; }
            public Color OriginalBackColor { get; }
            public Color OriginalForeColor { get; }
        }

        // 버튼에 포커스 강조 효과를 연결
        public static void FocusEvent(Button btn, ButtonKind kind = ButtonKind.BattleAction)
        {
            btn.Tag = new FocusContext(kind, btn.BackColor, btn.ForeColor);
            btn.GotFocus += OnGotFocus;
            btn.LostFocus += OnLostFocus;
        }

        // 포커스를 받으면 종류에 따라 강조 색상 적용
        private static void OnGotFocus(object? sender, EventArgs e)
        {
            if (sender is not Button btn || !btn.Enabled || btn.Tag is not FocusContext context) return;

            if (context.Kind == ButtonKind.BattleAction)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.Yellow;
            }
        }

        // 포커스를 잃으면 강조를 해제
        //  - BattleAction: 게임 기본 스타일(검은 배경 + 흰 글자)로
        //  - OptionSelect: 처음 저장해 둔 원래 색상으로
        private static void OnLostFocus(object? sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not FocusContext context) return;

            if (context.Kind == ButtonKind.BattleAction)
            {
                btn.BackColor = Color.Black;
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.BackColor = context.OriginalBackColor;
                btn.ForeColor = context.OriginalForeColor;
            }
        }
    }
}
