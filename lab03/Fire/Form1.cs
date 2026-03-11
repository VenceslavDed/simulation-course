using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ForestFireSimulation
{
    public enum CellState { Empty, Grass, Bush, Tree, Fire, Barrier }
    public enum WindDir { None, N, NE, E, SE, S, SW, W, NW }

    public partial class Form1 : Form
    {
        
        static readonly Color[] CColors = {
            Color.FromArgb(210, 195, 175),  // пусто
            Color.FromArgb(150, 220, 60),   // трава
            Color.FromArgb(50,  160, 50),   // куст
            Color.FromArgb(20,  100, 20),   // дерево
            Color.FromArgb(255, 80,  10),   // огонь 
            Color.FromArgb(60,  140, 220),  // вода
        };

        // Вероятности 
        static readonly double[] BaseIgnition = { 0.0, 0.70, 0.45, 0.25, 0.0, 0.0 };
        static readonly double[] LightningP = { 0.0, 0.00015, 0.00035, 0.00080, 0.0, 0.0 };
        const double PGrass = 0.003, PBush = 0.001, PTree = 0.0003;

        // поля
        CellState[,] grid, next;
        int gridSize = 50;
        int cellSize = 12;   // пересчитывается автоматически
        int generation = 0;

        WindDir windDir = WindDir.None;
        double windStrength = 0.4;
        bool lightningOn = true;
        bool growthOn = true;
        CellState brush = CellState.Tree;

        Timer simTimer;
        Panel canvasPanel;   // панель с фиксированным размером под сетку
        Bitmap bmp;
        Label statusLbl;
        Random rng = new Random();

        const int LEFT_W = 280;   // ширина левой панели
        const int STATUS_H = 26;    // высота строки статуса
        const int PADDING = 10;    // отступ сетки от края

        
        public Form1()
        {
            InitializeComponent();
            BuildUI();
            RecalcCellSize();
            InitGrid();
            DrawGrid();
        }

        
        void BuildUI()
        {
            Text = "Клеточный автомат — Лесные пожары (окрестность Мура)";
            BackColor = Color.FromArgb(245, 245, 240);
            ForeColor = Color.FromArgb(40, 40, 40);
            Size = new Size(1180, 800);
            MinimumSize = new Size(900, 650);
            Font = new Font("Segoe UI", 9f);

            // левая панель
            var left = new Panel
            {
                Dock = DockStyle.Left,
                Width = LEFT_W,
                BackColor = Color.FromArgb(255, 255, 255),
                Padding = new Padding(12)
            };
            // тень справа от панели
            left.Paint += (s, e) => {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(200, 200, 195)), left.Width - 1, 0, left.Width - 1, left.Height);
            };
            Controls.Add(left);

            int y = 12;

            
            var title = new Label
            {
                Text = "Лесные пожары",
                Location = new Point(12, y),
                Width = 250,
                ForeColor = Color.FromArgb(200, 60, 0),
                Font = new Font("Segoe UI", 13f, FontStyle.Bold)
            };
            left.Controls.Add(title); y += 36;
            HSep(left, ref y, Color.FromArgb(220, 215, 210));

            
            Lbl(left, "Размер сетки (10 – 120):", ref y);
            var nudSize = new NumericUpDown
            {
                Minimum = 10,
                Maximum = 120,
                Value = gridSize,
                Location = new Point(12, y),
                Width = 80,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(40, 40, 40),
                BorderStyle = BorderStyle.FixedSingle
            };
            left.Controls.Add(nudSize); y += 30;

            var btnApply = LBtn("Применить размер", y, Color.FromArgb(60, 120, 200), Color.White);
            btnApply.Width = 210;
            btnApply.Click += (s, e) => {
                gridSize = (int)nudSize.Value;
                RecalcCellSize();
                InitGrid();
                DrawGrid();
                UpdStatus();
            };
            left.Controls.Add(btnApply); y += 36;
            HSep(left, ref y, Color.FromArgb(220, 215, 210));

            
            Lbl(left, "Направление ветра:", ref y);
            var cmbWind = new ComboBox
            {
                Location = new Point(12, y),
                Width = 248,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(40, 40, 40),
                FlatStyle = FlatStyle.Flat
            };
            string[] wnames = { "Нет ветра", "Север ↑", "СВ ↗", "Восток →", "ЮВ ↘", "Юг ↓", "ЮЗ ↙", "Запад ←", "СЗ ↖" };
            foreach (var n in wnames) cmbWind.Items.Add(n);
            cmbWind.SelectedIndex = 0;
            cmbWind.SelectedIndexChanged += (s, e) => windDir = (WindDir)cmbWind.SelectedIndex;
            left.Controls.Add(cmbWind); y += 30;

            Lbl(left, "Сила ветра:", ref y);
            var trk = new TrackBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = 40,
                Location = new Point(8, y),
                Width = 210,
                TickFrequency = 20,
                BackColor = Color.White
            };
            var lblWS = new Label
            {
                Text = "40%",
                Location = new Point(220, y + 6),
                ForeColor = Color.FromArgb(200, 80, 0),
                Width = 38,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            trk.Scroll += (s, e) => { windStrength = trk.Value / 100.0; lblWS.Text = trk.Value + "%"; };
            left.Controls.Add(trk); left.Controls.Add(lblWS); y += 52;
            HSep(left, ref y, Color.FromArgb(220, 215, 210));

            
            /*Lbl(left, "Кисть:", ref y);*/
            string[] bnames = { "Пусто", "Трава", "Куст", "Дерево", "Огонь", "Преграда" };
            CellState[] bstates = { CellState.Empty, CellState.Grass, CellState.Bush,
                                    CellState.Tree,  CellState.Fire,  CellState.Barrier };
            Color[] bcolors = {
                Color.FromArgb(210,195,175), Color.FromArgb(120,200,50),
                Color.FromArgb(40,140,40),   Color.FromArgb(20,100,20),
                Color.FromArgb(220,70,10),   Color.FromArgb(60,140,220)
            };
            var bbtns = new Button[bnames.Length];
            for (int i = 0; i < bnames.Length; i++)
            {
                int idx = i;
                bool isDark = (i == 3 || i == 4 || i == 2);
                var b = new Button
                {
                    Text = bnames[i],
                    Location = new Point(12 + (i % 2) * 130, y + (i / 2) * 32),
                    Size = new Size(122, 28),
                    BackColor = (i == 3) ? bcolors[3] : Color.FromArgb(230, 230, 225),
                    ForeColor = (i == 3) ? Color.White : Color.FromArgb(40, 40, 40),
                    FlatStyle = FlatStyle.Flat
                };
                b.FlatAppearance.BorderColor = bcolors[i];
                b.FlatAppearance.BorderSize = 2;
                b.Click += (s, e) => {
                    brush = bstates[idx];
                    foreach (var x in bbtns)
                    {
                        x.BackColor = Color.FromArgb(230, 230, 225);
                        x.ForeColor = Color.FromArgb(40, 40, 40);
                    }
                    b.BackColor = bcolors[idx];
                    b.ForeColor = (idx == 0 || idx == 1) ? Color.FromArgb(40, 40, 40) : Color.White;
                };
                bbtns[i] = b;
                left.Controls.Add(b);
            }
            y += (bnames.Length / 2) * 32 + 12;
            HSep(left, ref y, Color.FromArgb(220, 215, 210));

            // доп правила
            Lbl(left, "Дополнительные правила:", ref y);

            var chkL = LChk("⚡  Правило 1: Молнии", y, lightningOn);
            chkL.CheckedChanged += (s, e) => lightningOn = chkL.Checked;
            left.Controls.Add(chkL); y += 26;

            var lblWinfo = new Label
            {
                Text = "🌬  Правило 2: Ветер",
                Location = new Point(12, y),
                Width = 255,
                ForeColor = Color.FromArgb(60, 120, 200)
            };
            left.Controls.Add(lblWinfo); y += 26;

            var chkG = LChk("🌱  Правило 3: Рост растительности", y, growthOn);
            chkG.CheckedChanged += (s, e) => growthOn = chkG.Checked;
            left.Controls.Add(chkG); y += 32;
            HSep(left, ref y, Color.FromArgb(220, 215, 210));

            // управление
            Lbl(left, "Управление:", ref y);
            var btnStart = LBtn("▶  Старт", y, Color.FromArgb(50, 160, 70), Color.White);
            var btnStop = LBtn("⏸  Стоп", y, Color.FromArgb(200, 70, 40), Color.White);
            btnStop.Left = 146;
            btnStart.Click += (s, e) => simTimer.Start();
            btnStop.Click += (s, e) => simTimer.Stop();
            left.Controls.Add(btnStart); left.Controls.Add(btnStop); y += 36;

            var btnStep = LBtn("⏭  Один шаг", y, Color.FromArgb(60, 120, 200), Color.White);
            btnStep.Width = 136;
            btnStep.Click += (s, e) => { Step(); DrawGrid(); UpdStatus(); };
            left.Controls.Add(btnStep); y += 36;

            var btnClear = LBtn("🗑  Очистить", y, Color.FromArgb(180, 180, 170), Color.FromArgb(40, 40, 40));
            var btnRnd = LBtn("🌲  Случайный лес", y, Color.FromArgb(40, 140, 60), Color.White);
            btnRnd.Left = 146; btnRnd.Width = 132;
            btnClear.Click += (s, e) => { InitGrid(); generation = 0; DrawGrid(); UpdStatus(); };
            btnRnd.Click += (s, e) => { RandomForest(); generation = 0; DrawGrid(); UpdStatus(); };
            left.Controls.Add(btnClear); left.Controls.Add(btnRnd); y += 36;
            HSep(left, ref y, Color.FromArgb(220, 215, 210));

            
            Lbl(left, "обозначение:", ref y);
            string[] lnames = { "Пусто / выгорело", "Трава", "Куст", "Дерево", "Огонь", "Преграда (вода/дорога)" };
            for (int i = 0; i < lnames.Length; i++)
            {
                var ll = new Label
                {
                    Text = "■  " + lnames[i],
                    Location = new Point(12, y),
                    Width = 256,
                    Height = 18,
                    ForeColor = (i == 0) ? Color.FromArgb(150, 130, 110) : CColors[i]
                };
                left.Controls.Add(ll); y += 18;
            }

            // холст
            canvasPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(235, 232, 225)
            };
            canvasPanel.MouseDown += CanvasMouseDown;
            canvasPanel.MouseMove += CanvasMouseMove;
            canvasPanel.MouseUp += (s, e) => { mouseDown = false; };
            canvasPanel.MouseLeave += (s, e) => { mouseDown = false; };
            canvasPanel.Resize += (s, e) => { RecalcCellSize(); RebuildBmp(); DrawGrid(); };
            Controls.Add(canvasPanel);


            //  строка статуса
            statusLbl = new Label
            {
                Dock = DockStyle.Bottom,
                Height = STATUS_H,
                BackColor = Color.FromArgb(220, 218, 212),
                ForeColor = Color.FromArgb(80, 80, 80),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Font = new Font("Segoe UI", 8.5f)
            };
            Controls.Add(statusLbl);

            //таймер
            simTimer = new Timer { Interval = 130 };
            simTimer.Tick += (s, e) => { Step(); DrawGrid(); UpdStatus(); };
        }

        // пересчёт размера клетки под текущий холст
        void RecalcCellSize()
        {
            int w = canvasPanel.ClientSize.Width - PADDING * 2;
            int h = canvasPanel.ClientSize.Height - PADDING * 2;
            if (w <= 0 || h <= 0 || gridSize <= 0) { cellSize = 8; return; }
            int cs = Math.Min(w, h) / gridSize;
            cellSize = Math.Max(1, cs);
        }

        
        void RebuildBmp()
        {
            int totalW = gridSize * cellSize;
            int totalH = gridSize * cellSize;
            if (totalW < 1 || totalH < 1) return;
            bmp = new Bitmap(canvasPanel.ClientSize.Width > 0 ? canvasPanel.ClientSize.Width : totalW,
                             canvasPanel.ClientSize.Height > 0 ? canvasPanel.ClientSize.Height : totalH);
        }

        
        void DrawGrid()
        {
            if (grid == null) return;
            int pw = canvasPanel.ClientSize.Width;
            int ph = canvasPanel.ClientSize.Height;
            if (pw < 1 || ph < 1) return;

            if (bmp == null || bmp.Width != pw || bmp.Height != ph)
                bmp = new Bitmap(pw, ph);

            int totalW = gridSize * cellSize;
            int totalH = gridSize * cellSize;
            int offX = (pw - totalW) / 2;
            int offY = (ph - totalH) / 2;

            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(235, 232, 225));

                for (int r = 0; r < gridSize; r++)
                {
                    for (int c = 0; c < gridSize; c++)
                    {
                        var col = CColors[(int)grid[r, c]];
                        if (grid[r, c] == CellState.Fire)
                        {
                            int f = rng.Next(-20, 20);
                            col = Color.FromArgb(Cl(col.R + f), Cl(col.G + f / 4), col.B);
                        }
                        int px = offX + c * cellSize;
                        int py = offY + r * cellSize;
                        using (var br = new SolidBrush(col))
                            g.FillRectangle(br, px, py, cellSize, cellSize);
                        if (cellSize >= 5)
                        {
                            using (var pen = new Pen(Color.FromArgb(30, 0, 0, 0)))
                                g.DrawRectangle(pen, px, py, cellSize - 1, cellSize - 1);
                        }
                    }
                }
                using (var pen = new Pen(Color.FromArgb(160, 150, 130), 1))
                    g.DrawRectangle(pen, offX - 1, offY - 1, totalW + 1, totalH + 1);
            }

            using (var g = canvasPanel.CreateGraphics())
                g.DrawImageUnscaled(bmp, 0, 0);
        }

        
        void GetCell(int px, int py, out int row, out int col)
        {
            int totalW = gridSize * cellSize;
            int totalH = gridSize * cellSize;
            int offX = (canvasPanel.ClientSize.Width - totalW) / 2;
            int offY = (canvasPanel.ClientSize.Height - totalH) / 2;
            col = (px - offX) / cellSize;
            row = (py - offY) / cellSize;
        }

        bool mouseDown = false;
        void CanvasMouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            int r, c; GetCell(e.X, e.Y, out r, out c);
            if (r >= 0 && r < gridSize && c >= 0 && c < gridSize)
            {
                grid[r, c] = brush;
                DrawGrid();
            }
        }
        void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown) return;
            int r, c; GetCell(e.X, e.Y, out r, out c);
            if (r >= 0 && r < gridSize && c >= 0 && c < gridSize)
            {
                grid[r, c] = brush;
                DrawGrid();
            }
        }


        // инициализируем
        void InitGrid()
        {
            grid = new CellState[gridSize, gridSize];
            next = new CellState[gridSize, gridSize];
        }

        void RandomForest()
        {
            InitGrid();
            for (int r = 0; r < gridSize; r++)
                for (int c = 0; c < gridSize; c++)
                {
                    double v = rng.NextDouble();
                    if (v < 0.04) grid[r, c] = CellState.Empty;
                    else if (v < 0.18) grid[r, c] = CellState.Grass;
                    else if (v < 0.42) grid[r, c] = CellState.Bush;
                    else grid[r, c] = CellState.Tree;
                }
            for (int i = 0; i < 4; i++)
            {
                int r = rng.Next(gridSize), c = rng.Next(gridSize);
                if (grid[r, c] != CellState.Empty && grid[r, c] != CellState.Barrier)
                    grid[r, c] = CellState.Fire;
            }
        }

        // шаг
        void Step()
        {
            generation++;
            Array.Copy(grid, next, grid.Length);

            for (int r = 0; r < gridSize; r++)
                for (int c = 0; c < gridSize; c++)
                {
                    var cur = grid[r, c];
                    if (cur == CellState.Fire)
                    {
                        next[r, c] = CellState.Empty;
                        SpreadFire(r, c);
                    }
                    else if (cur == CellState.Empty && growthOn)
                    {
                        double v = rng.NextDouble();
                        if (v < PGrass) next[r, c] = CellState.Grass;
                        else if (v < PGrass + PBush) next[r, c] = CellState.Bush;
                        else if (v < PGrass + PBush + PTree) next[r, c] = CellState.Tree;
                    }
                    else if (cur != CellState.Barrier && cur != CellState.Fire && cur != CellState.Empty)
                    {
                        if (lightningOn && rng.NextDouble() < LightningP[(int)cur])
                            next[r, c] = CellState.Fire;
                    }
                }

            var tmp = grid; grid = next; next = tmp;
        }

        void SpreadFire(int r, int c)
        {
            for (int dr = -1; dr <= 1; dr++)
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0) continue;
                    int nr = r + dr, nc = c + dc;
                    if (nr < 0 || nr >= gridSize || nc < 0 || nc >= gridSize) continue;
                    var nb = grid[nr, nc];
                    if (nb == CellState.Barrier || nb == CellState.Empty || nb == CellState.Fire) continue;
                    double prob = BaseIgnition[(int)nb] * WindFactor(dr, dc);
                    if (prob > 0.97) prob = 0.97;
                    if (rng.NextDouble() < prob)
                        next[nr, nc] = CellState.Fire;
                }
        }

        double WindFactor(int dr, int dc)
        {
            if (windDir == WindDir.None) return 1.0;
            int wr, wc;
            WVec(out wr, out wc);
            double dot = dr * wr + dc * wc;
            double norm = Math.Sqrt(dr * dr + dc * dc) * Math.Sqrt(wr * wr + wc * wc);
            double cos = norm > 0 ? dot / norm : 0;
            return Math.Max(0.05, 1.0 + cos * windStrength * 1.6);
        }

        void WVec(out int wr, out int wc)
        {
            switch (windDir)
            {
                case WindDir.N: wr = -1; wc = 0; break;
                case WindDir.NE: wr = -1; wc = 1; break;
                case WindDir.E: wr = 0; wc = 1; break;
                case WindDir.SE: wr = 1; wc = 1; break;
                case WindDir.S: wr = 1; wc = 0; break;
                case WindDir.SW: wr = 1; wc = -1; break;
                case WindDir.W: wr = 0; wc = -1; break;
                case WindDir.NW: wr = -1; wc = -1; break;
                default: wr = 0; wc = 0; break;
            }
        }

        
        static int Cl(int v) { return v < 0 ? 0 : v > 255 ? 255 : v; }

        int Cnt(CellState s)
        {
            int n = 0;
            for (int r = 0; r < gridSize; r++)
                for (int c = 0; c < gridSize; c++)
                    if (grid[r, c] == s) n++;
            return n;
        }

        void UpdStatus()
        {
            statusLbl.Text = string.Format(
                "Поколение: {0}   |   Огонь: {1}   |   Деревьев: {2}   Кустов: {3}   Травы: {4}   |   Размер клетки: {5}px",
                generation, Cnt(CellState.Fire), Cnt(CellState.Tree),
                Cnt(CellState.Bush), Cnt(CellState.Grass), cellSize);
        }

        
        void Lbl(Panel p, string t, ref int y)
        {
            var l = new Label
            {
                Text = t,
                Location = new Point(12, y),
                Width = 256,
                ForeColor = Color.FromArgb(90, 80, 70),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };
            p.Controls.Add(l); y += l.PreferredHeight + 3;
        }

        void HSep(Panel p, ref int y, Color col)
        {
            p.Controls.Add(new Label { Location = new Point(8, y), Size = new Size(260, 1), BackColor = col });
            y += 8;
        }

        Button LBtn(string t, int y, Color bg, Color fg)
        {
            var b = new Button
            {
                Text = t,
                Location = new Point(12, y),
                Size = new Size(128, 30),
                BackColor = bg,
                ForeColor = fg,
                FlatStyle = FlatStyle.Flat
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        CheckBox LChk(string t, int y, bool val)
        {
            return new CheckBox
            {
                Text = t,
                Checked = val,
                Location = new Point(12, y),
                Width = 260,
                ForeColor = Color.FromArgb(50, 50, 50),
                BackColor = Color.Transparent
            };
        }
    }
}