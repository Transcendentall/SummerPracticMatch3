namespace Match3
{
    partial class Game
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.LabelNameOfGame = new System.Windows.Forms.Label();
            this.LabelNameOfScoreBalls = new System.Windows.Forms.Label();
            this.LabelNameOfTime = new System.Windows.Forms.Label();
            this.LabelIntNumberOfScorePoints = new System.Windows.Forms.Label();
            this.LabelIntNumberOfTimeInSeconds = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelNameOfGame
            // 
            this.LabelNameOfGame.AutoSize = true;
            this.LabelNameOfGame.BackColor = System.Drawing.Color.Transparent;
            this.LabelNameOfGame.Font = new System.Drawing.Font("Segoe Script", 48F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNameOfGame.ForeColor = System.Drawing.Color.Red;
            this.LabelNameOfGame.Location = new System.Drawing.Point(352, 70);
            this.LabelNameOfGame.Name = "LabelNameOfGame";
            this.LabelNameOfGame.Size = new System.Drawing.Size(513, 133);
            this.LabelNameOfGame.TabIndex = 0;
            this.LabelNameOfGame.Text = "Три-в-ряд";
            this.LabelNameOfGame.Click += new System.EventHandler(this.LabelNameOfGame_Click);
            // 
            // LabelNameOfScoreBalls
            // 
            this.LabelNameOfScoreBalls.AutoSize = true;
            this.LabelNameOfScoreBalls.BackColor = System.Drawing.Color.Transparent;
            this.LabelNameOfScoreBalls.Font = new System.Drawing.Font("Segoe Script", 36F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNameOfScoreBalls.ForeColor = System.Drawing.Color.Red;
            this.LabelNameOfScoreBalls.Location = new System.Drawing.Point(1280, 148);
            this.LabelNameOfScoreBalls.Name = "LabelNameOfScoreBalls";
            this.LabelNameOfScoreBalls.Size = new System.Drawing.Size(550, 99);
            this.LabelNameOfScoreBalls.TabIndex = 1;
            this.LabelNameOfScoreBalls.Text = "Очков набрано:";
            this.LabelNameOfScoreBalls.Click += new System.EventHandler(this.LabelNameOfScoreBalls_Click);
            // 
            // LabelNameOfTime
            // 
            this.LabelNameOfTime.AutoSize = true;
            this.LabelNameOfTime.BackColor = System.Drawing.Color.Transparent;
            this.LabelNameOfTime.Font = new System.Drawing.Font("Segoe Script", 36F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelNameOfTime.ForeColor = System.Drawing.Color.Red;
            this.LabelNameOfTime.Location = new System.Drawing.Point(1205, 494);
            this.LabelNameOfTime.Name = "LabelNameOfTime";
            this.LabelNameOfTime.Size = new System.Drawing.Size(674, 99);
            this.LabelNameOfTime.TabIndex = 2;
            this.LabelNameOfTime.Text = "Времени осталось:";
            // 
            // LabelIntNumberOfScorePoints
            // 
            this.LabelIntNumberOfScorePoints.AutoSize = true;
            this.LabelIntNumberOfScorePoints.BackColor = System.Drawing.Color.Transparent;
            this.LabelIntNumberOfScorePoints.Font = new System.Drawing.Font("Snap ITC", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIntNumberOfScorePoints.ForeColor = System.Drawing.Color.Red;
            this.LabelIntNumberOfScorePoints.Location = new System.Drawing.Point(1472, 278);
            this.LabelIntNumberOfScorePoints.Name = "LabelIntNumberOfScorePoints";
            this.LabelIntNumberOfScorePoints.Size = new System.Drawing.Size(206, 155);
            this.LabelIntNumberOfScorePoints.TabIndex = 3;
            this.LabelIntNumberOfScorePoints.Text = "lll";
            this.LabelIntNumberOfScorePoints.Click += new System.EventHandler(this.LabelIntNumberOfScorePoints_Click);
            // 
            // LabelIntNumberOfTimeInSeconds
            // 
            this.LabelIntNumberOfTimeInSeconds.AutoSize = true;
            this.LabelIntNumberOfTimeInSeconds.BackColor = System.Drawing.Color.Transparent;
            this.LabelIntNumberOfTimeInSeconds.Font = new System.Drawing.Font("Snap ITC", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIntNumberOfTimeInSeconds.ForeColor = System.Drawing.Color.Red;
            this.LabelIntNumberOfTimeInSeconds.Location = new System.Drawing.Point(1472, 612);
            this.LabelIntNumberOfTimeInSeconds.Name = "LabelIntNumberOfTimeInSeconds";
            this.LabelIntNumberOfTimeInSeconds.Size = new System.Drawing.Size(206, 155);
            this.LabelIntNumberOfTimeInSeconds.TabIndex = 4;
            this.LabelIntNumberOfTimeInSeconds.Text = "lll";
            this.LabelIntNumberOfTimeInSeconds.Click += new System.EventHandler(this.LabelIntNumberOfTimeInSeconds_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.GameBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.LabelIntNumberOfTimeInSeconds);
            this.Controls.Add(this.LabelIntNumberOfScorePoints);
            this.Controls.Add(this.LabelNameOfTime);
            this.Controls.Add(this.LabelNameOfScoreBalls);
            this.Controls.Add(this.LabelNameOfGame);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Game";
            this.Text = "Три-в-ряд";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LabelNameOfGame;
        private System.Windows.Forms.Label LabelNameOfScoreBalls;
        private System.Windows.Forms.Label LabelNameOfTime;
        private System.Windows.Forms.Label LabelIntNumberOfScorePoints;
        private System.Windows.Forms.Label LabelIntNumberOfTimeInSeconds;
    }
}
