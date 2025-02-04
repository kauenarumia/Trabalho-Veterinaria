﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmRua : Form
    {
        DataTable Tabela_Rua;
        Boolean novo;
        int posicao;
        List<Rua> lista_rua = new List<Rua>();


        public FrmRua()
        {
            InitializeComponent();
            CarregaTabela();

            lista_rua = carregaListaRua();
            if(lista_rua.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCodigo.Text = lista_rua[posicao].codrua.ToString();
            txtRua.Text = lista_rua[posicao].nomerua.ToString();
        }

        List<Rua> carregaListaRua()
        {
            List<Rua> lista = new List<Rua>();

            C_Rua c_Rua = new C_Rua();
            lista = c_Rua.DadosRua();

            return lista;
        }

        List<Rua> carregaListaRuaFiltro()
        {
            List<Rua> lista = new List<Rua>();

            C_Rua c_Rua = new C_Rua();
            lista = c_Rua.DadosRuaFiltro(txtBuscar.Text);

            return lista;
        }

        private void CarregaTabela()
        {
            C_Rua c_Rua = new C_Rua();
            DataTable dt = new DataTable();
            dt = c_Rua.Buscar_Todos();
            Tabela_Rua = dt;
            dataGridView1.DataSource = Tabela_Rua;
        }

        private void FrmRua_Load(object sender, EventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            ativarCampos();
            ativaBotoes();

            novo = true;
        }

        private void ativaBotoes()
        {
            btnNovo.Enabled = false;
            btnApagar.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void ativarCampos()
        {
            txtRua.Enabled = true;
        }

        private void limparCampos()
        {
            txtCodigo.Text = "";
            txtRua.Text = "";
        }

        private void desativaCampos()
        {
            txtRua.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Rua rua = new Rua();
            rua.nomerua = txtRua.Text;

            C_Rua c_Rua = new C_Rua();
            if(novo == true)
            {
                c_Rua.Insere_Dados(rua);
            }
            else
            {
                rua.codrua = Int32.Parse(txtCodigo.Text);
                c_Rua.Atualizar_Dados(rua);
            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();
        }

        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnApagar.Enabled = true;
            btnEditar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desativaBotoes();
            desativaCampos();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                C_Rua rua = new C_Rua();

                if (txtCodigo.Text != "")
                {
                    int valor = Int32.Parse(txtCodigo.Text);
                    rua.Apaga_Dados(valor);
                    CarregaTabela();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ativarCampos();
            ativaBotoes();
            novo = false;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = 0;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao--;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            int total = lista_rua.Count - 1;
            if (total > posicao)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = lista_rua.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            C_Rua c_rua = new C_Rua();
            DataTable dt = new DataTable();
            dt = c_rua.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_Rua = dt;
            dataGridView1.DataSource = Tabela_Rua;

            lista_rua = carregaListaRuaFiltro();

            if(lista_rua.Count - 1 > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCodigo.Text = dr.Cells[0].Value.ToString();
            txtRua.Text = dr.Cells[1].Value.ToString();
        }
    }
}
