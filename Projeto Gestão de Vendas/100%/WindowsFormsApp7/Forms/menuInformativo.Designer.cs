
namespace WindowsFormsApp7.Forms
{
    partial class menuInformativo
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
            this.buttonBack = new System.Windows.Forms.Button();
            this.textBoxInformacao = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(549, 469);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(88, 39);
            this.buttonBack.TabIndex = 16;
            this.buttonBack.Text = "Voltar";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // textBoxInformacao
            // 
            this.textBoxInformacao.Location = new System.Drawing.Point(8, 63);
            this.textBoxInformacao.Multiline = true;
            this.textBoxInformacao.Name = "textBoxInformacao";
            this.textBoxInformacao.Size = new System.Drawing.Size(629, 400);
            this.textBoxInformacao.TabIndex = 17;
            // 
            // menuInformativo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 520);
            this.Controls.Add(this.textBoxInformacao);
            this.Controls.Add(this.buttonBack);
            this.Name = "menuInformativo";
            this.Text = "Gestão de Vendas - Menu Informativo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.TextBox textBoxInformacao;
    }
}