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
        private Button btnDiscord = null!;
        private LinkLabel lblScamWarning = null!;

        public MainForm()
        {
            InitializeComponent();
            SetupCustomUI();
            UpdateMitmUI(false);
        }

        private Font GetModernFont(float size, FontStyle style = FontStyle.Regular)
        {
            try
            {
                return new Font("Bahnschrift", size, style);
            }
            catch
            {
                return new Font("Segoe UI", size, style);
            }
        }

        private void SetupCustomUI()
        {
            this.Text = "BW UNLOCKER v1.0.0 - 100% Automático";
            this.Size = new Size(640, 435);
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
                Size = new Size(636, 42),
                BackColor = Color.FromArgb(12, 11, 19),
            };
            this.Controls.Add(pnlTitleBar);

            // Badge / Logo "BW"
            lblLogoBW = new Label
            {
                Text = "BW",
                Location = new Point(14, 10),
                Size = new Size(36, 22),
                Font = GetModernFont(11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 0, 127),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlTitleBar.Controls.Add(lblLogoBW);

            // Texto do Título
            lblTitleText = new Label
            {
                Text = "UNLOCKER   v1.0.0 - 100% Automático",
                Location = new Point(56, 11),
                Size = new Size(460, 20),
                Font = GetModernFont(9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(200, 200, 220),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlTitleBar.Controls.Add(lblTitleText);

            // Botão Fechar (✕)
            btnClose = new Button
            {
                Text = "✕",
                Location = new Point(592, 6),
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
                Location = new Point(552, 6),
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
                Location = new Point(24, 56),
                Size = new Size(592, 82),
                BackColor = Color.FromArgb(14, 13, 24)
            };
            this.Controls.Add(pnlStatusCard);

            // Ponto de Status (Dot)
            pnlStatusDot = new Panel
            {
                Location = new Point(22, 33),
                Size = new Size(14, 14),
                BackColor = Color.FromArgb(255, 46, 99)
            };
            pnlStatusCard.Controls.Add(pnlStatusDot);

            // Divisor vertical no Status
            pnlStatusDivider = new Panel
            {
                Location = new Point(50, 18),
                Size = new Size(1, 46),
                BackColor = Color.FromArgb(40, 38, 58)
            };
            pnlStatusCard.Controls.Add(pnlStatusDivider);

            // Título do Status
            lblStatusHeader = new Label
            {
                Text = "STATUS:  PARADO",
                Location = new Point(66, 16),
                Size = new Size(500, 24),
                Font = GetModernFont(12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 46, 99)
            };
            pnlStatusCard.Controls.Add(lblStatusHeader);

            // Detalhe do Status
            lblStatusDetail = new Label
            {
                Text = "Clique em 'INICIAR UNLOCKER AUTOMÁTICO' para ativar o desbloqueio.",
                Location = new Point(67, 43),
                Size = new Size(500, 22),
                Font = GetModernFont(9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(150, 150, 175)
            };
            pnlStatusCard.Controls.Add(lblStatusDetail);

            // -----------------------------------------------------------------
            // 3. BOTÕES PRINCIPAIS (Iniciar / Parar)
            // -----------------------------------------------------------------
            // Botão INICIAR UNLOCKER AUTOMÁTICO
            btnStartMitm = new Button
            {
                Text = "▶   INICIAR UNLOCKER AUTOMÁTICO",
                Location = new Point(24, 152),
                Size = new Size(592, 54),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(28, 14, 42),
                ForeColor = Color.White,
                Font = GetModernFont(12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStartMitm.FlatAppearance.BorderSize = 1;
            btnStartMitm.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 127);
            btnStartMitm.Click += BtnStartMitm_Click;
            this.Controls.Add(btnStartMitm);

            // Botão PARAR UNLOCKER (Com nitidez e visibilidade aprimorada!)
            btnStopMitm = new Button
            {
                Text = "⏹   PARAR UNLOCKER",
                Location = new Point(24, 218),
                Size = new Size(592, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(18, 18, 28),
                ForeColor = Color.FromArgb(180, 180, 205),
                Font = GetModernFont(10.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStopMitm.FlatAppearance.BorderSize = 1;
            btnStopMitm.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 85);
            btnStopMitm.Click += BtnStopMitm_Click;
            this.Controls.Add(btnStopMitm);

            // -----------------------------------------------------------------
            // 4. LINHA DIVISÓRIA HORIZONTAL
            // -----------------------------------------------------------------
            pnlMainDivider = new Panel
            {
                Location = new Point(24, 276),
                Size = new Size(592, 1),
                BackColor = Color.FromArgb(28, 28, 42)
            };
            this.Controls.Add(pnlMainDivider);

            // -----------------------------------------------------------------
            // 5. BOTÕES SECUNDÁRIOS (SSL / Logs / Discord)
            // -----------------------------------------------------------------
            btnInstallCert = new Button
            {
                Text = "🔗   Instalar SSL",
                Location = new Point(24, 290),
                Size = new Size(186, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 16, 26),
                ForeColor = Color.FromArgb(210, 210, 230),
                Font = GetModernFont(9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnInstallCert.FlatAppearance.BorderSize = 1;
            btnInstallCert.FlatAppearance.BorderColor = Color.FromArgb(50, 50, 75);
            btnInstallCert.Click += BtnInstallCert_Click;
            this.Controls.Add(btnInstallCert);

            btnOpenLogs = new Button
            {
                Text = "📄   Abrir Logs",
                Location = new Point(222, 290),
                Size = new Size(186, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 16, 26),
                ForeColor = Color.FromArgb(210, 210, 230),
                Font = GetModernFont(9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnOpenLogs.FlatAppearance.BorderSize = 1;
            btnOpenLogs.FlatAppearance.BorderColor = Color.FromArgb(50, 50, 75);
            btnOpenLogs.Click += BtnOpenLogs_Click;
            this.Controls.Add(btnOpenLogs);

            btnDiscord = new Button
            {
                Text = "💬   Discord Oficial",
                Location = new Point(420, 290),
                Size = new Size(196, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(24, 18, 38),
                ForeColor = Color.FromArgb(180, 140, 255),
                Font = GetModernFont(9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDiscord.FlatAppearance.BorderSize = 1;
            btnDiscord.FlatAppearance.BorderColor = Color.FromArgb(140, 80, 255);
            btnDiscord.Click += (s, e) => OpenDiscordLink();
            this.Controls.Add(btnDiscord);

            // -----------------------------------------------------------------
            // 6. AVISO DE GOLPE / SCAMMER (Rodapé)
            // -----------------------------------------------------------------
            lblScamWarning = new LinkLabel
            {
                Text = "⚠️ Se você pagou por este programa, você foi ENGANADO!\nClique para entrar no Discord: https://discord.gg/GvzPKRGxrs",
                Location = new Point(24, 348),
                Size = new Size(592, 65),
                Font = GetModernFont(9F, FontStyle.Bold),
                LinkColor = Color.FromArgb(255, 90, 130),
                ActiveLinkColor = Color.FromArgb(255, 0, 127),
                VisitedLinkColor = Color.FromArgb(255, 90, 130),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(20, 12, 24),
                Padding = new Padding(6)
            };
            lblScamWarning.Click += (s, e) => OpenDiscordLink();
            this.Controls.Add(lblScamWarning);
        }

        private void OpenDiscordLink()
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://discord.gg/GvzPKRGxrs") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível abrir o link: {ex.Message}", "Discord", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Desenha a borda neon magenta em volta da janela inteira
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(Color.FromArgb(255, 0, 127), 2))
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
                btnStartMitm.ForeColor = Color.FromArgb(90, 90, 110);

                btnStopMitm.Enabled = true;
                btnStopMitm.BackColor = Color.FromArgb(50, 14, 30);
                btnStopMitm.FlatAppearance.BorderColor = Color.FromArgb(255, 46, 99);
                btnStopMitm.ForeColor = Color.White;
            }
            else
            {
                lblStatusHeader.Text = "STATUS:  PARADO";
                lblStatusHeader.ForeColor = Color.FromArgb(255, 46, 99); // Pink/Red
                pnlStatusDot.BackColor = Color.FromArgb(255, 46, 99);
                lblStatusDetail.Text = "Clique em 'INICIAR UNLOCKER AUTOMÁTICO' para ativar o desbloqueio.";
                lblStatusDetail.ForeColor = Color.FromArgb(150, 150, 175);

                btnStartMitm.Enabled = true;
                btnStartMitm.BackColor = Color.FromArgb(28, 14, 42);
                btnStartMitm.FlatAppearance.BorderColor = Color.FromArgb(255, 0, 127);
                btnStartMitm.ForeColor = Color.White;

                btnStopMitm.Enabled = false;
                btnStopMitm.BackColor = Color.FromArgb(18, 18, 28);
                btnStopMitm.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 85);
                btnStopMitm.ForeColor = Color.FromArgb(140, 140, 160);
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
