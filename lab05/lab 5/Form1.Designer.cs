namespace lab_5
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();


            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblQuestionHint1 = new System.Windows.Forms.Label();
            this.txtQuestion1 = new System.Windows.Forms.TextBox();
            this.lblProbHint = new System.Windows.Forms.Label();
            this.numProbability = new System.Windows.Forms.NumericUpDown();
            this.lblProbValue = new System.Windows.Forms.Label();
            this.btnAsk1 = new System.Windows.Forms.Button();
            this.lblAnswer1 = new System.Windows.Forms.Label();
            this.lblStats1 = new System.Windows.Forms.Label();
            this.btnReset1 = new System.Windows.Forms.Button();


            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblQuestionHint2 = new System.Windows.Forms.Label();
            this.txtQuestion2 = new System.Windows.Forms.TextBox();
            this.lblBallProbs = new System.Windows.Forms.Label();
            this.panelBall = new System.Windows.Forms.Panel();
            this.btnAsk2 = new System.Windows.Forms.Button();
            this.btnReset2 = new System.Windows.Forms.Button();
            this.lblBallAnswer = new System.Windows.Forms.Label();
            this.lblStats2 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();


            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 580);
            this.tabControl1.TabIndex = 0;

            // ════════════════════════════════════════════════════════════            // TAB PAGE 1
            this.tabPage1.Text = "Часть 1 — «Да или нет»";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(10);
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.tabPage1.Controls.Add(this.lblTitle1);
            this.tabPage1.Controls.Add(this.lblQuestionHint1);
            this.tabPage1.Controls.Add(this.txtQuestion1);
            this.tabPage1.Controls.Add(this.lblProbHint);
            this.tabPage1.Controls.Add(this.numProbability);
            this.tabPage1.Controls.Add(this.lblProbValue);
            this.tabPage1.Controls.Add(this.btnAsk1);
            this.tabPage1.Controls.Add(this.lblAnswer1);
            this.tabPage1.Controls.Add(this.lblStats1);
            this.tabPage1.Controls.Add(this.btnReset1);


            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle1.Location = new System.Drawing.Point(12, 14);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Text = "Приложение «Скажи да или нет»";


            this.lblQuestionHint1.AutoSize = true;
            this.lblQuestionHint1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuestionHint1.Location = new System.Drawing.Point(12, 52);
            this.lblQuestionHint1.Name = "lblQuestionHint1";
            this.lblQuestionHint1.Text = "Ваш вопрос:";


            this.txtQuestion1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtQuestion1.Location = new System.Drawing.Point(12, 74);
            this.txtQuestion1.Name = "txtQuestion1";
            this.txtQuestion1.Size = new System.Drawing.Size(520, 26);
            this.txtQuestion1.Text = "Пойти сегодня в университет?";


            this.lblProbHint.AutoSize = true;
            this.lblProbHint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProbHint.Location = new System.Drawing.Point(12, 116);
            this.lblProbHint.Name = "lblProbHint";
            this.lblProbHint.Text = "Вероятность «Да»:";


            this.numProbability.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numProbability.Location = new System.Drawing.Point(185, 113);
            this.numProbability.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numProbability.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numProbability.Name = "numProbability";
            this.numProbability.Size = new System.Drawing.Size(70, 26);
            this.numProbability.Value = new decimal(new int[] { 50, 0, 0, 0 });
            this.numProbability.ValueChanged += new System.EventHandler(this.numProbability_ValueChanged);


            this.lblProbValue.AutoSize = true;
            this.lblProbValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProbValue.ForeColor = System.Drawing.Color.FromArgb(30, 80, 180);
            this.lblProbValue.Location = new System.Drawing.Point(265, 116);
            this.lblProbValue.Name = "lblProbValue";
            this.lblProbValue.Text = "p = 50%";


            this.btnAsk1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAsk1.Location = new System.Drawing.Point(12, 155);
            this.btnAsk1.Name = "btnAsk1";
            this.btnAsk1.Size = new System.Drawing.Size(200, 36);
            this.btnAsk1.Text = "Получить ответ";
            this.btnAsk1.UseVisualStyleBackColor = true;
            this.btnAsk1.Click += new System.EventHandler(this.btnAsk1_Click);


            this.lblAnswer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAnswer1.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblAnswer1.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblAnswer1.Location = new System.Drawing.Point(12, 205);
            this.lblAnswer1.Name = "lblAnswer1";
            this.lblAnswer1.Size = new System.Drawing.Size(520, 90);
            this.lblAnswer1.Text = "...";
            this.lblAnswer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


            this.lblStats1.AutoSize = false;
            this.lblStats1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStats1.ForeColor = System.Drawing.Color.Gray;
            this.lblStats1.Location = new System.Drawing.Point(12, 310);
            this.lblStats1.Name = "lblStats1";
            this.lblStats1.Size = new System.Drawing.Size(520, 36);
            this.lblStats1.Text = "Статистика: ещё нет данных";


            this.btnReset1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReset1.Location = new System.Drawing.Point(12, 358);
            this.btnReset1.Name = "btnReset1";
            this.btnReset1.Size = new System.Drawing.Size(170, 30);
            this.btnReset1.Text = "Сбросить статистику";
            this.btnReset1.UseVisualStyleBackColor = true;
            this.btnReset1.Click += new System.EventHandler(this.btnReset1_Click);


            // TAB PAGE 2
            this.tabPage2.Text = "Часть 2 — Magic 8-Ball";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(10);
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.tabPage2.Controls.Add(this.lblTitle2);
            this.tabPage2.Controls.Add(this.lblQuestionHint2);
            this.tabPage2.Controls.Add(this.txtQuestion2);
            this.tabPage2.Controls.Add(this.lblBallProbs);
            this.tabPage2.Controls.Add(this.panelBall);
            this.tabPage2.Controls.Add(this.btnAsk2);
            this.tabPage2.Controls.Add(this.btnReset2);
            this.tabPage2.Controls.Add(this.lblBallAnswer);
            this.tabPage2.Controls.Add(this.lblStats2);


            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle2.Location = new System.Drawing.Point(12, 14);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Text = "Шар предсказаний погоды (Magic 8-Ball)";


            this.lblQuestionHint2.AutoSize = true;
            this.lblQuestionHint2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuestionHint2.Location = new System.Drawing.Point(12, 52);
            this.lblQuestionHint2.Name = "lblQuestionHint2";
            this.lblQuestionHint2.Text = "Задайте вопрос о погоде:";


            this.txtQuestion2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtQuestion2.Location = new System.Drawing.Point(12, 74);
            this.txtQuestion2.Name = "txtQuestion2";
            this.txtQuestion2.Size = new System.Drawing.Size(400, 26);
            this.txtQuestion2.Text = "Какая сегодня будет погода ?";

            // lblBallProbs (список вероятностей — левая колонка)
            this.lblBallProbs.AutoSize = false;
            this.lblBallProbs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBallProbs.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblBallProbs.Location = new System.Drawing.Point(12, 112);
            this.lblBallProbs.Name = "lblBallProbs";
            this.lblBallProbs.Size = new System.Drawing.Size(390, 175);
            this.lblBallProbs.Text = "";

            // panelBall — рисуем шар справа
            this.panelBall.Location = new System.Drawing.Point(415, 60);
            this.panelBall.Name = "panelBall";
            this.panelBall.Size = new System.Drawing.Size(200, 200);
            this.panelBall.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.panelBall.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBall_Paint);


            this.btnAsk2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAsk2.Location = new System.Drawing.Point(12, 298);
            this.btnAsk2.Name = "btnAsk2";
            this.btnAsk2.Size = new System.Drawing.Size(185, 36);
            this.btnAsk2.Text = "Спросить шар";
            this.btnAsk2.UseVisualStyleBackColor = true;
            this.btnAsk2.Click += new System.EventHandler(this.btnAsk2_Click);


            this.btnReset2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReset2.Location = new System.Drawing.Point(210, 298);
            this.btnReset2.Name = "btnReset2";
            this.btnReset2.Size = new System.Drawing.Size(170, 36);
            this.btnReset2.Text = "Сбросить статистику";
            this.btnReset2.UseVisualStyleBackColor = true;
            this.btnReset2.Click += new System.EventHandler(this.btnReset2_Click);


            this.lblBallAnswer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBallAnswer.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBallAnswer.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblBallAnswer.Location = new System.Drawing.Point(12, 348);
            this.lblBallAnswer.Name = "lblBallAnswer";
            this.lblBallAnswer.Size = new System.Drawing.Size(600, 50);
            this.lblBallAnswer.Text = "Задайте вопрос о погоде...";
            this.lblBallAnswer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


            this.lblStats2.AutoSize = false;
            this.lblStats2.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStats2.ForeColor = System.Drawing.Color.Gray;
            this.lblStats2.Location = new System.Drawing.Point(12, 410);
            this.lblStats2.Name = "lblStats2";
            this.lblStats2.Size = new System.Drawing.Size(620, 140);
            this.lblStats2.Text = "Статистика: ещё нет данных";

            // ── Form1 ────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 580);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(676, 619);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лабораторная 5 — Моделирование случайных событий";

            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion


        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;


        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label lblQuestionHint1;
        private System.Windows.Forms.TextBox txtQuestion1;
        private System.Windows.Forms.Label lblProbHint;
        private System.Windows.Forms.NumericUpDown numProbability;
        private System.Windows.Forms.Label lblProbValue;
        private System.Windows.Forms.Button btnAsk1;
        private System.Windows.Forms.Label lblAnswer1;
        private System.Windows.Forms.Label lblStats1;
        private System.Windows.Forms.Button btnReset1;


        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblQuestionHint2;
        private System.Windows.Forms.TextBox txtQuestion2;
        private System.Windows.Forms.Label lblBallProbs;
        private System.Windows.Forms.Panel panelBall;
        private System.Windows.Forms.Button btnAsk2;
        private System.Windows.Forms.Button btnReset2;
        private System.Windows.Forms.Label lblBallAnswer;
        private System.Windows.Forms.Label lblStats2;
    }
}