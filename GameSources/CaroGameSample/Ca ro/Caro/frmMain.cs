using System;
using System.Drawing;
using System.Windows.Forms;

namespace GomokuGame
{
    public enum Player { Out = -1, None = 0, Human = 1, Machine = 2 }
    public struct Node
    {
        public int Row;
        public int Column;
    }

    public partial class frmMain : Form
    {
    // ************ VARIABLE *******************************
        private GomokuBoard view;
        private frmOption Option;

    // ************ CONSTRUCTOR ****************************
        public frmMain()
        {
            InitializeComponent();
            Option = new frmOption();
        }

    // ************ HANDLE EVENT ***************************
        private void frmMain_Load(object sender, EventArgs e)
        {
            view = new GomokuBoard(this);
            
            NewGame();
            statusbar.Hide();

            vScroll.Width = 20;
            hScroll.Height = 20;

            vScroll.Maximum = vScroll.Minimum = vScroll.Value = 1;
            hScroll.Maximum = hScroll.Minimum = hScroll.Value = 1;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            view.DrawBoard();
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                view.HandleMouseDown(e);
            }
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nhom thuc hien: Hoc Lại."
                            + "\n Nguyễn Thiên Hà."
                            + "\n Trần Phan Duy Luân."
                            + "\n Lương Hùng Dũng."
                            + "\n Nguyễn Hoàng Hùng Vương."
                            + "\n Phạm Văn Kiên."
                            + "\n Phạm Văn Chiến."
                            , "About"
                            , MessageBoxButtons.OK
                            , MessageBoxIcon.Information);
        }
        
        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Option.Show();
            
        }

        private void vScroll_Scroll(object sender, ScrollEventArgs e)
        {
            view.OffsetH = vScroll.Value;
            Invalidate();
        }
        
        private void hScroll_Scroll(object sender, ScrollEventArgs e)
        {
            view.OffsetW = hScroll.Value;
            Invalidate();
        }
    
    // *********** ADDING FUNCTION *************************
        public void SendMessage(uint Msg)
        {
            switch (Msg)
            {
                case 0:
                    statusbar.Hide();
                    break;
                case 1:
                    Message.Text = "Chơi 1 ván nữa đi... !";
                    statusbar.Show();
                    break;

                case 2:
                    Message.Text = "Bạn là một con gà,ko chơi với bạn nữa!";
                    statusbar.Show();
                    break;

                case 3:
                    Message.Text = "1.. 2.. 3.. chuan bi!?";
                    statusbar.Show();
                    break;

                case 4:
                    Message.Text = "Nguy hiem qua !?";
                    statusbar.Show();
                    break;
            }
        }

        public void NewGame()
        {
            // thiet lap lai ban co.
            view.ResetBoard();
            view.End = Player.None;
            // Do thong minh.
            GomokuBoard.MaxDepth = Option.scrTrinhDo.Value;
            // cho may danh truoc ?
            if (Option.chkFirstMove.Checked) view.CurrPlayer = Player.Human;
            else
                view.Board[Height / 40 + view.OffsetH, Width / 40 + view.OffsetW] = Player.Machine;
            // Loai doi thu:
            if (Option.cmbKinhNghiem.Text == "Chuyen gia tan cong")
            {
                view.TScore = new int[5] { 0, 1, 21, 341, 5461 };
                view.KScore = new int[5] { 0, 5, 85, 1635, 21885 };
            }
            else if (Option.cmbKinhNghiem.Text == "Chuyen gia phong thu")
            {
                view.TScore = new int[5] { 0, 5, 85, 1635, 21885 };
                view.KScore = new int[5] { 0, 1, 21, 341, 5461 };
            }
            else // Phong thu chat, tan cong nhanh.
            {
                view.TScore = new int[5] { 0, 1, 9, 85, 769 };
                view.KScore = new int[5] { 0, 2, 28, 256, 2308 };
            }

            if (Option.rbtNut.Checked)
            {
                view.bmpHuman = new Bitmap("..//..//bmpNodeHuman.bmp");
                view.bmpMachine = new Bitmap("..//..//bmpNodeMachine.bmp");
                view.bmpNone = new Bitmap("..//..//bmpNodeNone.bmp");
            }
            else if (Option.rbtClassic.Checked)
            {
                view.bmpHuman = new Bitmap("..//..//bmpHuman.bmp");
                view.bmpMachine = new Bitmap("..//..//bmpMachine.bmp");
                view.bmpNone = new Bitmap("..//..//bmpNone.bmp");
            }
            else if (Option.rbtWeb.Checked)
            {
                view.bmpHuman = new Bitmap("..//..//bmpWebHuman.bmp");
                view.bmpMachine = new Bitmap("..//..//bmpWebMachine.bmp");
                view.bmpNone = new Bitmap("..//..//bmpWebNone.bmp");
            }
            view.DrawBoard();
            statusbar.Hide();
        }

    // ********** OVERRIDE METHOD **************************
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (view != null)
            {
                hScroll.Maximum = view.Width + 1 - Width / 20;
                vScroll.Maximum = view.Height + 1 - Height / 20;

                view.OffsetH = vScroll.Value;
                view.OffsetW = hScroll.Value;
            }
        }
    }
}