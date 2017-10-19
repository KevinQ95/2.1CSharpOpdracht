namespace ClientForm
{
    partial class Form1
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
            this._Label1 = new System.Windows.Forms.Label();
            this.CardsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._Label2 = new System.Windows.Forms.Label();
            this.OpponentCardsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.TopStack = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.TopStack)).BeginInit();
            this.SuspendLayout();
            // 
            // _Label1
            // 
            this._Label1.AutoSize = true;
            this._Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label1.ForeColor = System.Drawing.Color.White;
            this._Label1.Location = new System.Drawing.Point(770, 300);
            this._Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._Label1.Name = "_Label1";
            this._Label1.Size = new System.Drawing.Size(126, 46);
            this._Label1.TabIndex = 0;
            this._Label1.Text = "label1";
            // 
            // CardsPanel
            // 
            this.CardsPanel.Location = new System.Drawing.Point(211, 419);
            this.CardsPanel.Name = "CardsPanel";
            this.CardsPanel.Size = new System.Drawing.Size(756, 138);
            this.CardsPanel.TabIndex = 3;
            // 
            // _Label2
            // 
            this._Label2.AutoSize = true;
            this._Label2.Location = new System.Drawing.Point(77, 355);
            this._Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._Label2.Name = "_Label2";
            this._Label2.Size = new System.Drawing.Size(46, 17);
            this._Label2.TabIndex = 1;
            this._Label2.Text = "label1";
            // 
            // OpponentCardsPanel
            // 
            this.OpponentCardsPanel.Location = new System.Drawing.Point(211, 48);
            this.OpponentCardsPanel.Name = "OpponentCardsPanel";
            this.OpponentCardsPanel.Size = new System.Drawing.Size(756, 142);
            this.OpponentCardsPanel.TabIndex = 4;
            // 
            // TopStack
            // 
            this.TopStack.Location = new System.Drawing.Point(320, 241);
            this.TopStack.Name = "TopStack";
            this.TopStack.Size = new System.Drawing.Size(113, 172);
            this.TopStack.TabIndex = 5;
            this.TopStack.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(1189, 633);
            this.Controls.Add(this.TopStack);
            this.Controls.Add(this.CardsPanel);
            this.Controls.Add(this.OpponentCardsPanel);
            this.Controls.Add(this._Label2);
            this.Controls.Add(this._Label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Pesten the Game";
            ((System.ComponentModel.ISupportInitialize)(this.TopStack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _Label1;

        private System.Windows.Forms.FlowLayoutPanel CardsPanel;
        private System.Windows.Forms.Label _Label2;
        private System.Windows.Forms.FlowLayoutPanel OpponentCardsPanel;
        private System.Windows.Forms.PictureBox TopStack;
    }
}

