using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHOP
{
    public partial class FrmDangNhap : Form
    {
        int expandStep = 10; 

        public FrmDangNhap()
        {
            InitializeComponent();
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                Application.Exit(); //thoat
            }
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra người dùng đã nhập thông tin chưa
            if (string.IsNullOrWhiteSpace(txttendangnhap.Text) || string.IsNullOrWhiteSpace(txtmatkhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = txttendangnhap.Text;
            string password = txtmatkhau.Text;

            if (username == "admin" && password == "123") // Kiểm tra thông tin đăng nhập
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Đưa PictureBox lên trên cùng
                pictureBoxLogo.BringToFront();

                // Bắt đầu hiệu ứng di chuyển
                timerEfect.Start();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txttendangnhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtmatkhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void timerEfect_Tick(object sender, EventArgs e)
        {
            pictureBoxLogo.Left -= expandStep; // Dịch sang trái
            pictureBoxLogo.Width += expandStep; // Tăng chiều rộng

            // Kiểm tra nếu PictureBox đã che hết form
            if (pictureBoxLogo.Left <= 0)
            {
                timerEfect.Stop(); // Dừng Timer
                OpenNextForm();    // Mở form tiếp theo
            }
        }
        private void OpenNextForm()
        {
            // Mở form mới
            trangchu form2 = new trangchu(); // Thay Form2 bằng form thực tế của bạn
            form2.Show();
            this.Hide();

        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
