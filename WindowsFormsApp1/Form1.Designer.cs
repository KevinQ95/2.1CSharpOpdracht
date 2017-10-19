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
            this.OpponentCardsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.TopStack = new System.Windows.Forms.PictureBox();
            this._playerNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TopStack)).BeginInit();
            this.SuspendLayout();
            // 
            // _Label1
            // 
            this._Label1.AutoSize = true;
            this._Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label1.ForeColor = System.Drawing.Color.White;
            this._Label1.Location = new System.Drawing.Point(578, 244);
            this._Label1.Name = "_Label1";
            this._Label1.Size = new System.Drawing.Size(100, 37);
            this._Label1.TabIndex = 0;
            this._Label1.Text = "label1";
            // 
            // CardsPanel
            // 
            this.CardsPanel.Location = new System.Drawing.Point(158, 340);
            this.CardsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.CardsPanel.Name = "CardsPanel";
            this.CardsPanel.Size = new System.Drawing.Size(567, 112);
            this.CardsPanel.TabIndex = 3;
            // 
            // OpponentCardsPanel
            // 
            this.OpponentCardsPanel.Location = new System.Drawing.Point(158, 39);
            this.OpponentCardsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.OpponentCardsPanel.Name = "OpponentCardsPanel";
            this.OpponentCardsPanel.Size = new System.Drawing.Size(567, 115);
            this.OpponentCardsPanel.TabIndex = 4;
            // 
            // TopStack
            // 
            this.TopStack.Location = new System.Drawing.Point(240, 196);
            this.TopStack.Margin = new System.Windows.Forms.Padding(2);
            this.TopStack.Name = "TopStack";
            this.TopStack.Size = new System.Drawing.Size(85, 140);
            this.TopStack.TabIndex = 5;
            this.TopStack.TabStop = false;
            // 
            // _playerNumber
            // 
            this._playerNumber.AutoSize = true;
            this._playerNumber.Location = new System.Drawing.Point(13, 13);
            this._playerNumber.Name = "_playerNumber";
            this._playerNumber.Size = new System.Drawing.Size(35, 13);
            this._playerNumber.TabIndex = 6;
            this._playerNumber.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(892, 514);
            this.Controls.Add(this._playerNumber);
            this.Controls.Add(this.TopStack);
            this.Controls.Add(this.CardsPanel);
            this.Controls.Add(this.OpponentCardsPanel);
            this.Controls.Add(this._Label1);
            this.Name = "Form1";
            this.Text = "Pesten the Game";
            ((System.ComponentModel.ISupportInitialize)(this.TopStack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _Label1;

        private System.Windows.Forms.FlowLayoutPanel CardsPanel;
        private System.Windows.Forms.FlowLayoutPanel OpponentCardsPanel;
        private System.Windows.Forms.PictureBox TopStack;
        private System.Windows.Forms.Label _playerNumber;
    }
}

