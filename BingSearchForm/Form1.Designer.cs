namespace BingSearchForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TxtTimes = new TextBox();
            label1 = new Label();
            MWindowToDo = new Button();
            BtnToDo = new Button();
            SuspendLayout();
            // 
            // TxtTimes
            // 
            TxtTimes.Location = new Point(127, 31);
            TxtTimes.Name = "TxtTimes";
            TxtTimes.Size = new Size(121, 23);
            TxtTimes.TabIndex = 1;
            TxtTimes.Text = "30";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 34);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 2;
            label1.Text = "执行次数";
            // 
            // MWindowToDo
            // 
            MWindowToDo.Location = new Point(29, 64);
            MWindowToDo.Name = "MWindowToDo";
            MWindowToDo.Size = new Size(219, 23);
            MWindowToDo.TabIndex = 0;
            MWindowToDo.Text = "执 行 - 多窗口";
            MWindowToDo.UseVisualStyleBackColor = true;
            MWindowToDo.Click += MWindowToDo_Click;
            // 
            // BtnToDo
            // 
            BtnToDo.Location = new Point(29, 106);
            BtnToDo.Name = "BtnToDo";
            BtnToDo.Size = new Size(219, 23);
            BtnToDo.TabIndex = 0;
            BtnToDo.Text = "执 行";
            BtnToDo.UseVisualStyleBackColor = true;
            BtnToDo.Click += BtnToDo_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(274, 150);
            Controls.Add(label1);
            Controls.Add(TxtTimes);
            Controls.Add(BtnToDo);
            Controls.Add(MWindowToDo);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "bing自动搜索";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox TxtTimes;
        private Label label1;
        private Button MWindowToDo;
        private Button BtnToDo;
    }
}
