
namespace WindowsFormsApp7.Forms
{
    partial class menuPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonInformativo = new System.Windows.Forms.Button();
            this.buttonVendas = new System.Windows.Forms.Button();
            this.buttonProdutos = new System.Windows.Forms.Button();
            this.buttonClientes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonInformativo
            // 
            this.buttonInformativo.Location = new System.Drawing.Point(549, 469);
            this.buttonInformativo.Name = "buttonInformativo";
            this.buttonInformativo.Size = new System.Drawing.Size(88, 39);
            this.buttonInformativo.TabIndex = 15;
            this.buttonInformativo.Text = "Informativo";
            this.buttonInformativo.UseVisualStyleBackColor = true;
            this.buttonInformativo.Click += new System.EventHandler(this.buttonInformativo_Click);
            // 
            // buttonVendas
            // 
            this.buttonVendas.Location = new System.Drawing.Point(200, 469);
            this.buttonVendas.Name = "buttonVendas";
            this.buttonVendas.Size = new System.Drawing.Size(88, 39);
            this.buttonVendas.TabIndex = 14;
            this.buttonVendas.Text = "Vendas";
            this.buttonVendas.UseVisualStyleBackColor = true;
            this.buttonVendas.Click += new System.EventHandler(this.buttonVendas_Click);
            // 
            // buttonProdutos
            // 
            this.buttonProdutos.Location = new System.Drawing.Point(106, 469);
            this.buttonProdutos.Name = "buttonProdutos";
            this.buttonProdutos.Size = new System.Drawing.Size(88, 39);
            this.buttonProdutos.TabIndex = 13;
            this.buttonProdutos.Text = "Produtos";
            this.buttonProdutos.UseVisualStyleBackColor = true;
            this.buttonProdutos.Click += new System.EventHandler(this.buttonProdutos_Click_1);
            // 
            // buttonClientes
            // 
            this.buttonClientes.Location = new System.Drawing.Point(12, 469);
            this.buttonClientes.Name = "buttonClientes";
            this.buttonClientes.Size = new System.Drawing.Size(88, 39);
            this.buttonClientes.TabIndex = 12;
            this.buttonClientes.Text = "Clientes";
            this.buttonClientes.UseVisualStyleBackColor = true;
            this.buttonClientes.Click += new System.EventHandler(this.buttonClientes_Click);
            // 
            // menuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 520);
            this.Controls.Add(this.buttonInformativo);
            this.Controls.Add(this.buttonVendas);
            this.Controls.Add(this.buttonProdutos);
            this.Controls.Add(this.buttonClientes);
            this.Name = "menuPrincipal";
            this.Text = "Gestão de Vendas - Menu Principal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonInformativo;
        private System.Windows.Forms.Button buttonVendas;
        private System.Windows.Forms.Button buttonProdutos;
        private System.Windows.Forms.Button buttonClientes;
    }
}