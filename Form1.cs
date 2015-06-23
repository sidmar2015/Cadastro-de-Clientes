using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroDePessoas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.nome = txbNome.Text;
            cliente.cpf = txbCPF.Text;
            /* percorre o listbox com o for para obter os números*/

            for (int i = 0; i < ltbNumeros.Items.Count; i++)
            {
            cliente.numSorte[i] = Convert.ToInt32(ltbNumeros.Items[i]);
            }
            ClienteDAO c = new ClienteDAO();
            c.Cadastrar(cliente);
            txbCPF.Text = "";
            txbNome.Text = "";
            ltbNumeros.Items.Clear();
            this.tb_clienteTableAdapter.Fill(this.db_clientesDataSet.tb_cliente);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            ltbNumeros.Items.Add(txbNumero.Text);
            txbNumero.Text = "";
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            ltbNumeros.Items.RemoveAt(ltbNumeros.SelectedIndex);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'db_clientesDataSet.tb_cliente' table. You can move, or remove it, as needed.
            this.tb_clienteTableAdapter.Fill(this.db_clientesDataSet.tb_cliente);

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ClienteDAO c = new ClienteDAO();
            c.Exportar();
          
        }
    }
}
