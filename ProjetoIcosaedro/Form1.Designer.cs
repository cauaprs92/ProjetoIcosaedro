namespace ProjetoIcosaedro
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblMosaico;
        private System.Windows.Forms.Label lblAjuda;
        private System.Windows.Forms.Label lblTransX;
        private System.Windows.Forms.Label lblTransY;
        private System.Windows.Forms.Label lblEscala;
        private System.Windows.Forms.Label lblRotacao;
        private System.Windows.Forms.TrackBar tbTransX;
        private System.Windows.Forms.TrackBar tbTransY;
        private System.Windows.Forms.TrackBar tbEscala;
        private System.Windows.Forms.TrackBar tbRotacao;
        private System.Windows.Forms.GroupBox gbAlgoritmo;
        private System.Windows.Forms.RadioButton rbBreseham;
        private System.Windows.Forms.RadioButton rbDDA;
        private System.Windows.Forms.CheckBox ckNumeros;
        private System.Windows.Forms.Button btLimpar;
        private System.Windows.Forms.Button btRestaurar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblMosaico = new System.Windows.Forms.Label();
            this.lblAjuda = new System.Windows.Forms.Label();
            this.lblTransX = new System.Windows.Forms.Label();
            this.lblTransY = new System.Windows.Forms.Label();
            this.lblEscala = new System.Windows.Forms.Label();
            this.lblRotacao = new System.Windows.Forms.Label();
            this.tbTransX = new System.Windows.Forms.TrackBar();
            this.tbTransY = new System.Windows.Forms.TrackBar();
            this.tbEscala = new System.Windows.Forms.TrackBar();
            this.tbRotacao = new System.Windows.Forms.TrackBar();
            this.gbAlgoritmo = new System.Windows.Forms.GroupBox();
            this.rbBreseham = new System.Windows.Forms.RadioButton();
            this.rbDDA = new System.Windows.Forms.RadioButton();
            this.ckNumeros = new System.Windows.Forms.CheckBox();
            this.btLimpar = new System.Windows.Forms.Button();
            this.btRestaurar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbEscala)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRotacao)).BeginInit();
            this.gbAlgoritmo.SuspendLayout();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(660, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Icosaedro 2D - ICG";
            //
            // lblMosaico
            //
            this.lblMosaico.AutoSize = true;
            this.lblMosaico.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMosaico.Location = new System.Drawing.Point(660, 40);
            this.lblMosaico.Name = "lblMosaico";
            this.lblMosaico.TabIndex = 1;
            this.lblMosaico.Text = "Mosaico de Cores";
            //
            // lblAjuda
            //
            this.lblAjuda.AutoSize = true;
            this.lblAjuda.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.lblAjuda.ForeColor = System.Drawing.Color.DimGray;
            this.lblAjuda.Location = new System.Drawing.Point(660, 60);
            this.lblAjuda.Name = "lblAjuda";
            this.lblAjuda.TabIndex = 2;
            this.lblAjuda.Text = "Clique em uma cor e depois na face (1 a 10) da figura.";
            //
            // tbTransX
            //
            this.tbTransX.Location = new System.Drawing.Point(657, 300);
            this.tbTransX.Maximum = 250;
            this.tbTransX.Minimum = -250;
            this.tbTransX.Name = "tbTransX";
            this.tbTransX.Size = new System.Drawing.Size(320, 45);
            this.tbTransX.TabIndex = 3;
            this.tbTransX.TickFrequency = 25;
            this.tbTransX.Value = 0;
            this.tbTransX.Scroll += new System.EventHandler(this.Controle_Scroll);
            //
            // lblTransX
            //
            this.lblTransX.AutoSize = true;
            this.lblTransX.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTransX.Location = new System.Drawing.Point(660, 282);
            this.lblTransX.Name = "lblTransX";
            this.lblTransX.TabIndex = 4;
            this.lblTransX.Text = "Translacao X (tx) = 0";
            //
            // tbTransY
            //
            this.tbTransY.Location = new System.Drawing.Point(657, 368);
            this.tbTransY.Maximum = 250;
            this.tbTransY.Minimum = -250;
            this.tbTransY.Name = "tbTransY";
            this.tbTransY.Size = new System.Drawing.Size(320, 45);
            this.tbTransY.TabIndex = 5;
            this.tbTransY.TickFrequency = 25;
            this.tbTransY.Value = 0;
            this.tbTransY.Scroll += new System.EventHandler(this.Controle_Scroll);
            //
            // lblTransY
            //
            this.lblTransY.AutoSize = true;
            this.lblTransY.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTransY.Location = new System.Drawing.Point(660, 350);
            this.lblTransY.Name = "lblTransY";
            this.lblTransY.TabIndex = 6;
            this.lblTransY.Text = "Translacao Y (ty) = 0";
            //
            // tbEscala
            //
            this.tbEscala.Location = new System.Drawing.Point(657, 436);
            this.tbEscala.Maximum = 200;
            this.tbEscala.Minimum = 20;
            this.tbEscala.Name = "tbEscala";
            this.tbEscala.Size = new System.Drawing.Size(320, 45);
            this.tbEscala.TabIndex = 7;
            this.tbEscala.TickFrequency = 10;
            this.tbEscala.Value = 100;
            this.tbEscala.Scroll += new System.EventHandler(this.Controle_Scroll);
            //
            // lblEscala
            //
            this.lblEscala.AutoSize = true;
            this.lblEscala.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblEscala.Location = new System.Drawing.Point(660, 418);
            this.lblEscala.Name = "lblEscala";
            this.lblEscala.TabIndex = 8;
            this.lblEscala.Text = "Escala (sx,sy) = 1,00";
            //
            // tbRotacao
            //
            this.tbRotacao.Location = new System.Drawing.Point(657, 504);
            this.tbRotacao.Maximum = 360;
            this.tbRotacao.Minimum = 0;
            this.tbRotacao.Name = "tbRotacao";
            this.tbRotacao.Size = new System.Drawing.Size(320, 45);
            this.tbRotacao.TabIndex = 9;
            this.tbRotacao.TickFrequency = 30;
            this.tbRotacao.Value = 0;
            this.tbRotacao.Scroll += new System.EventHandler(this.Controle_Scroll);
            //
            // lblRotacao
            //
            this.lblRotacao.AutoSize = true;
            this.lblRotacao.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblRotacao.Location = new System.Drawing.Point(660, 486);
            this.lblRotacao.Name = "lblRotacao";
            this.lblRotacao.TabIndex = 10;
            this.lblRotacao.Text = "Rotacao (teta) = 0 graus";
            //
            // gbAlgoritmo
            //
            this.gbAlgoritmo.Controls.Add(this.rbBreseham);
            this.gbAlgoritmo.Controls.Add(this.rbDDA);
            this.gbAlgoritmo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.gbAlgoritmo.Location = new System.Drawing.Point(660, 552);
            this.gbAlgoritmo.Name = "gbAlgoritmo";
            this.gbAlgoritmo.Size = new System.Drawing.Size(200, 50);
            this.gbAlgoritmo.TabIndex = 11;
            this.gbAlgoritmo.TabStop = false;
            this.gbAlgoritmo.Text = "Algoritmo de reta";
            //
            // rbBreseham
            //
            this.rbBreseham.AutoSize = true;
            this.rbBreseham.Checked = true;
            this.rbBreseham.Location = new System.Drawing.Point(10, 22);
            this.rbBreseham.Name = "rbBreseham";
            this.rbBreseham.TabIndex = 0;
            this.rbBreseham.TabStop = true;
            this.rbBreseham.Text = "Breseham";
            this.rbBreseham.UseVisualStyleBackColor = true;
            this.rbBreseham.CheckedChanged += new System.EventHandler(this.Controle_Scroll);
            //
            // rbDDA
            //
            this.rbDDA.AutoSize = true;
            this.rbDDA.Location = new System.Drawing.Point(110, 22);
            this.rbDDA.Name = "rbDDA";
            this.rbDDA.TabIndex = 1;
            this.rbDDA.Text = "DDA";
            this.rbDDA.UseVisualStyleBackColor = true;
            this.rbDDA.CheckedChanged += new System.EventHandler(this.Controle_Scroll);
            //
            // ckNumeros
            //
            this.ckNumeros.AutoSize = true;
            this.ckNumeros.Checked = true;
            this.ckNumeros.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckNumeros.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.ckNumeros.Location = new System.Drawing.Point(875, 570);
            this.ckNumeros.Name = "ckNumeros";
            this.ckNumeros.TabIndex = 12;
            this.ckNumeros.Text = "Numerar faces";
            this.ckNumeros.UseVisualStyleBackColor = true;
            this.ckNumeros.CheckedChanged += new System.EventHandler(this.Controle_Scroll);
            //
            // btLimpar
            //
            this.btLimpar.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.btLimpar.Location = new System.Drawing.Point(660, 612);
            this.btLimpar.Name = "btLimpar";
            this.btLimpar.Size = new System.Drawing.Size(150, 28);
            this.btLimpar.TabIndex = 13;
            this.btLimpar.Text = "Limpar cores das faces";
            this.btLimpar.UseVisualStyleBackColor = true;
            this.btLimpar.Click += new System.EventHandler(this.btLimpar_Click);
            //
            // btRestaurar
            //
            this.btRestaurar.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.btRestaurar.Location = new System.Drawing.Point(825, 612);
            this.btRestaurar.Name = "btRestaurar";
            this.btRestaurar.Size = new System.Drawing.Size(150, 28);
            this.btRestaurar.TabIndex = 14;
            this.btRestaurar.Text = "Restaurar transformacoes";
            this.btRestaurar.UseVisualStyleBackColor = true;
            this.btRestaurar.Click += new System.EventHandler(this.btRestaurar_Click);
            //
            // Form1
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(990, 655);
            this.Controls.Add(this.btRestaurar);
            this.Controls.Add(this.btLimpar);
            this.Controls.Add(this.ckNumeros);
            this.Controls.Add(this.gbAlgoritmo);
            this.Controls.Add(this.lblRotacao);
            this.Controls.Add(this.tbRotacao);
            this.Controls.Add(this.lblEscala);
            this.Controls.Add(this.tbEscala);
            this.Controls.Add(this.lblTransY);
            this.Controls.Add(this.tbTransY);
            this.Controls.Add(this.lblTransX);
            this.Controls.Add(this.tbTransX);
            this.Controls.Add(this.lblAjuda);
            this.Controls.Add(this.lblMosaico);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Introducao a Computacao Grafica - Projeto 3o Bimestre - Icosaedro 2D";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.tbTransX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbEscala)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRotacao)).EndInit();
            this.gbAlgoritmo.ResumeLayout(false);
            this.gbAlgoritmo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
