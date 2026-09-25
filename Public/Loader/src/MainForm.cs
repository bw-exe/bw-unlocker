#nullable enable
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace DbdLoader
{
    public partial class MainForm : Form
    {
        private Panel pnlTitleBar = null!;
        private Label lblLogoBW = null!;
        private Label lblTitleText = null!;
        private Button btnMinimize = null!;
        private Button btnClose = null!;

        private Panel pnlStatusCard = null!;
        private Panel pnlStatusDot = null!;
        private Panel pnlStatusDivider = null!;
        private Label lblStatusHeader = null!;
        private Label lblStatusDetail = null!;

        private Button btnStartMitm = null!;
        private Button btnStopMitm = null!;
        private Panel pnlMainDivider = null!;
        private Button btnInstallCert = null!;
        private Button btnOpenLogs = null!;

        public MainForm()
        {
            InitializeComponent();
            SetupCustomUI();
            UpdateMitmUI(false);
        }

        private void SetupCustomUI()
        {
            this.Text = "BW UNLOCKER v11.0.0 - 100% Automático";
            this.Size = new Size(620, 390);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(10, 10, 16);
            this.ForeColor = Color.White;
            this.DoubleBuffered = true;

            // -----------------------------------------------------------------
            // 1. BARRA DE TÍTULO CUSTOMIZADA (TitleBar)
            // -----------------------------------------------------------------
            pnlTitleBar = new Panel
            {
                Location = new Point(2, 2),
                Size = new Size(616, 42),
                BackColor = Color.FromArgb(12, 11, 19),
            };
            this.Controls.Add(pnlTitleBar);

            // Badge / Logo "BW"
            lblLogoBW = new Label
            {
                Text = "BW",
                Location = new Point(16, 10),
                Size = new Size(36, 22),
                Font = new Font("Consolas", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlTitleBar.Controls.Add(lblLogoBW);

            // Texto do Título
            lblTitleText = new Label
            {
                Text = "UNLOCKER   v11.0.0 - 100% Automático",
                Location = new Point(58, 11),
                Size = new Size(440, 20),
                Font = new Font("Consolas", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(180, 180, 200),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlTitleBar.Controls.Add(lblTitleText);

            // Botão Fechar (✕)
            btnClose = new Button
            {
                Text = "✕",
                Location = new Point(572, 6),
                Size = new Size(36, 30),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(160, 160, 180),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 17, 35);
            btnClose.Click += (s, e) => this.Close();
            pnlTitleBar.Controls.Add(btnClose);

            // Botão Minimizar (—)
            btnMinimize = new Button
            {
                Text = "—",
                Location = new Point(532, 6),
                Size = new Size(36, 30),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(160, 160, 180),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(35, 35, 50);
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            pnlTitleBar.Controls.Add(btnMinimize);

            // -----------------------------------------------------------------
            // 2. CARD DE STATUS
            // -----------------------------------------------------------------
            pnlStatusCard = new Panel
            {
                Location = new Point(24, 60),
                Size = new Size(572, 85),
                BackColor = Color.FromArgb(14, 13, 22)
            };
            this.Controls.Add(pnlStatusCard);

            // Ponto de Status com brilho (Dot)
            pnlStatusDot = new Panel
            {
                Location = new Point(24, 34),
                Size = new Size(14, 14),
                BackColor = Color.FromArgb(255, 46, 99)
            };
            pnlStatusCard.Controls.Add(pnlStatusDot);

            // Divisor vertical no Status
            pnlStatusDivider = new Panel
            {
                Location = new Point(52, 20),
                Size = new Size(1, 45),
                BackColor = Color.FromArgb(40, 38, 55)
            };
            pnlStatusCard.Controls.Add(pnlStatusDivider);

            // Título do Status
            lblStatusHeader = new Label
            {
                Text = "STATUS:  PARADO",
                Location = new Point(68, 18),
                Size = new Size(480, 24),
                Font = new Font("Consolas", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 46, 99)
            };
            pnlStatusCard.Controls.Add(lblStatusHeader);

            // Detalhe do Status
            lblStatusDetail = new Label
            {
                Text = "Clique em 'INICIAR UNLOCKER AUTOMÁTICO' para ativar o desbloqueio.",
                Location = new Point(69, 44),
                Size = new Size(480, 22),
                Font = new Font("Consolas", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(138, 138, 158)
            };
            pnlStatusCard.Controls.Add(lblStatusDetail);

            // -----------------------------------------------------------------
            // 3. BOTÕES PRINCIPAIS (Iniciar / Parar)
            // -----------------------------------------------------------------
            // Botão INICIAR UNLOCKER AUTOMÁTICO
            btnStartMitm = new Button
            {
                Text = "▶   INICIAR UNLOCKER AUTOMÁTICO",
                Location = new Point(24, 162),
                Size = new Size(572, 54),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(24, 14, 38),
                ForeColor = Color.White,
                Font = new Font("Consolas", 11.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStartMitm.FlatAppearance.BorderSize = 1;
            btnStartMitm.FlatAppearance.BorderColor = Color.FromArgb(214, 52, 132);
            btnStartMitm.Click += BtnStartMitm_Click;
            this.Controls.Add(btnStartMitm);

            // Botão PARAR UNLOCKER
            btnStopMitm = new Button
            {
                Text = "⏹   PARAR UNLOCKER",
                Location = new Point(24, 228),
                Size = new Size(572, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(15, 15, 23),
                ForeColor = Color.FromArgb(120, 120, 140),
                Font = new Font("Consolas", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStopMitm.FlatAppearance.BorderSize = 1;
            btnStopMitm.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 58);
            btnStopMitm.Click += BtnStopMitm_Click;
            this.Controls.Add(btnStopMitm);

            // -----------------------------------------------------------------
            // 4. LINHA DIVISÓRIA HORIZONTAL
            // -----------------------------------------------------------------
            pnlMainDivider = new Panel
            {
                Location = new Point(24, 290),
                Size = new Size(572, 1),
                BackColor = Color.FromArgb(28, 28, 40)
            };
            this.Controls.Add(pnlMainDivider);

            // -----------------------------------------------------------------
            // 5. BOTÕES SECUNDÁRIOS (Instalar Certificado / Abrir Logs)
            // -----------------------------------------------------------------
            btnInstallCert = new Button
            {
                Text = "🔗   Instalar Certificado SSL",
                Location = new Point(24, 308),
                Size = new Size(276, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 16, 26),
                ForeColor = Color.FromArgb(200, 200, 220),
                Font = new Font("Consolas", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnInstallCert.FlatAppearance.BorderSize = 1;
            btnInstallCert.FlatAppearance.BorderColor = Color.FromArgb(50, 50, 70);
            btnInstallCert.Click += BtnInstallCert_Click;
            this.Controls.Add(btnInstallCert);

            btnOpenLogs = new Button
            {
                Text = "📄   Abrir Logs",
                Location = new Point(320, 308),
                Size = new Size(276, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 16, 26),
                ForeColor = Color.FromArgb(200, 200, 220),
                Font = new Font("Consolas", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnOpenLogs.FlatAppearance.BorderSize = 1;
            btnOpenLogs.FlatAppearance.BorderColor = Color.FromArgb(50, 50, 70);
            btnOpenLogs.Click += BtnOpenLogs_Click;
            this.Controls.Add(btnOpenLogs);
        }

        // Desenha a borda neon rosa/magenta em volta da janela inteira
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(Color.FromArgb(214, 52, 132), 2))
            {
                e.Graphics.DrawRectangle(borderPen, 1, 1, this.Width - 2, this.Height - 2);
            }
        }

        // Arrastar janela sem borda nativa
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84 /* WM_NCHITTEST */)
            {
                if (m.Result == (IntPtr)1 /* HTCLIENT */)
                {
                    Point p = PointToClient(new Point(m.LParam.ToInt32()));
                    if (p.Y <= 42)
                    {
                        m.Result = (IntPtr)2; // HTCAPTION
                    }
                }
            }
        }

        private void UpdateMitmUI(bool running)
        {
            if (running)
            {
                lblStatusHeader.Text = "STATUS:  ATIVO";
                lblStatusHeader.ForeColor = Color.FromArgb(0, 245, 212); // Neon Cyan/Teal
                pnlStatusDot.BackColor = Color.FromArgb(0, 245, 212);
                lblStatusDetail.Text = "Proxy ativo interceptando bhvrdbd.com. Pode abrir o DBD e jogar!";
                lblStatusDetail.ForeColor = Color.FromArgb(160, 230, 210);

                btnStartMitm.Enabled = false;
                btnStartMitm.BackColor = Color.FromArgb(15, 15, 24);
                btnStartMitm.FlatAppearance.BorderColor = Color.FromArgb(50, 50, 70);
                btnStartMitm.ForeColor = Color.FromArgb(80, 80, 100);

                btnStopMitm.Enabled = true;
                btnStopMitm.BackColor = Color.FromArgb(40, 14, 25);
                btnStopMitm.FlatAppearance.BorderColor = Color.FromArgb(255, 46, 99);
                btnStopMitm.ForeColor = Color.White;
            }
            else
            {
                lblStatusHeader.Text = "STATUS:  PARADO";
                lblStatusHeader.ForeColor = Color.FromArgb(255, 46, 99); // Pink/Red
                pnlStatusDot.BackColor = Color.FromArgb(255, 46, 99);
                lblStatusDetail.Text = "Clique em 'INICIAR UNLOCKER AUTOMÁTICO' para ativar o desbloqueio.";
                lblStatusDetail.ForeColor = Color.FromArgb(138, 138, 158);

                btnStartMitm.Enabled = true;
                btnStartMitm.BackColor = Color.FromArgb(24, 14, 38);
                btnStartMitm.FlatAppearance.BorderColor = Color.FromArgb(214, 52, 132);
                btnStartMitm.ForeColor = Color.White;

                btnStopMitm.Enabled = false;
                btnStopMitm.BackColor = Color.FromArgb(15, 15, 23);
                btnStopMitm.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 58);
                btnStopMitm.ForeColor = Color.FromArgb(70, 70, 90);
            }
        }

        private void BtnStartMitm_Click(object? sender, EventArgs e)
        {
            if (MitmManager.StartMitmServer(out string error))
            {
                UpdateMitmUI(true);
            }
            else
            {
                MessageBox.Show($"Falha ao iniciar Servidor MITM:\n{error}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStopMitm_Click(object? sender, EventArgs e)
        {
            if (MitmManager.StopMitmServer(out string error))
            {
                UpdateMitmUI(false);
            }
            else
            {
                MessageBox.Show($"Falha ao parar Servidor MITM:\n{error}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnInstallCert_Click(object? sender, EventArgs e)
        {
            if (MitmManager.InstallCert(out string error))
            {
                MessageBox.Show("Gerenciador de Certificado SSL executado!", "Certificado SSL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Erro no certificado:\n{error}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpenLogs_Click(object? sender, EventArgs e)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string logPath = Path.Combine(baseDir, "eos_proxy_debug.log");

            if (File.Exists(logPath))
            {
                Process.Start(new ProcessStartInfo(logPath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Inicie o servidor para gerar os registros.", "Logs", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
