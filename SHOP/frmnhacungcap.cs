using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SHOP
{
    public partial class frmnhacungcap : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=LAPTOP-BI6NJ1G1;Initial Catalog=QLShopThoiTrang;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();


        public frmnhacungcap()
        {
            InitializeComponent();
        }
        void loaddata(string searchKeyword = "")
        {
            command = connection.CreateCommand();
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                command.CommandText = "select * from NhaCungCap where TenNCC like @searchKeyword";
                command.Parameters.AddWithValue("@searchKeyword", "%" + searchKeyword + "%");
            }
            else
            {
                command.CommandText = "select * from NhaCungCap";
            }

            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvNcc.DataSource = table;
            // Cập nhật lại DataGridView
            dgvNcc.Refresh();
        }

        private void frmnhacungcap_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "insert into NhaCungCap values('" + txtMaNCC.Text + "',N'" + txtName.Text + "'," + txtHotline.Text + ",'" + txtEmail.Text + "')";
            command.ExecuteNonQuery();
            loaddata();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "delete from NhaCungCap where MaNCC='" + txtMaNCC.Text + "'";
            command.ExecuteNonQuery();
            loaddata();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "update NhaCungCap set TenNCC=N'" + txtName.Text + "',Hotline=" + txtHotline.Text + ",Email='" + txtEmail.Text + "' where MaNCC='" + txtMaNCC.Text + "'";
            command.ExecuteNonQuery();
            loaddata();
        }

        private void btnKhoitao_Click(object sender, EventArgs e)
        {
            txtMaNCC.Text = "";
            txtName.Text = "";
            txtHotline.Text = "";
            txtEmail.Text = "";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text;
            loaddata(searchKeyword);
        }

        private void dgvNcc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNCC.ReadOnly = true;
            int i = dgvNcc.CurrentCell.RowIndex;
            txtMaNCC.Text = dgvNcc.Rows[i].Cells[0].Value.ToString();
            txtName.Text = dgvNcc.Rows[i].Cells[1].Value.ToString();
            txtHotline.Text = dgvNcc.Rows[i].Cells[2].Value.ToString();
            txtEmail.Text = dgvNcc.Rows[i].Cells[3].Value.ToString();
        }
    }
}
