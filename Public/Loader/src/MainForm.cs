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
        // ---------------------------------------------------------------------
        // PALETA SUAVE & CONFORTÁVEL PARA OS OLHOS (EYE-FRIENDLY DARK SYNTH)
        // ---------------------------------------------------------------------
        private static readonly Color BgMain = Color.FromArgb(12, 13, 22);           // #0C0D16 - Fundo escuro suave
        private static readonly Color CardBg = Color.FromArgb(18, 19, 32);           // #121320 - Fundo dos cards
        private static readonly Color AccentPurple = Color.FromArgb(168, 85, 247);   // #A855F7 - Roxo suave
        private static readonly Color AccentPink = Color.FromArgb(244, 114, 182);    // #F472B6 - Rosa suave
        private static readonly Color AccentCyan = Color.FromArgb(45, 212, 191);     // #2DD4BF - Ciano/Teal suave
        private static readonly Color AccentRed = Color.FromArgb(244, 63, 94);       // #F43F5E - Vermelho suave
        private static readonly Color BorderSubtle = Color.FromArgb(40, 42, 65);     // #282A41 - Borda discreta
        private static readonly Color TextMain = Color.FromArgb(243, 244, 246);    // #F3F4F6 - Branco frio suave
        private static readonly Color TextMuted = Color.FromArgb(156, 163, 175);   // #9CA3AF - Cinza leitura confortável

        private Panel pnlTitleBar = null!;
        private PictureBox picLogo = null!;
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
            LoadCustomIcon();
            SetupCustomUI();
            UpdateMitmUI(false);
        }

        private void LoadCustomIcon()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string iconPath = Path.Combine(baseDir, "app_icon.ico");
                if (!File.Exists(iconPath))
                {
                    iconPath = Path.Combine(baseDir, "..", "..", "app_icon.ico");
                }
                if (File.Exists(iconPath))
                {
                    this.Icon = new Icon(iconPath);
                }
            }
            catch { }
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
            this.Text = "BW UNLOCKER v1.0.0";
            this.Size = new Size(640, 435);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = BgMain;
            this.ForeColor = TextMain;
            this.DoubleBuffered = true;

            // -----------------------------------------------------------------
            // 1. BARRA DE TÍTULO CUSTOMIZADA (TitleBar)
            // -----------------------------------------------------------------
            pnlTitleBar = new Panel
            {
                Location = new Point(1, 1),
                Size = new Size(638, 42),
                BackColor = Color.FromArgb(15, 16, 26),
            };
            this.Controls.Add(pnlTitleBar);

            // Logo Retro Wave Image (PictureBox)
            picLogo = new PictureBox
            {
                Location = new Point(14, 8),
                Size = new Size(26, 26),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string logoPath = Path.Combine(baseDir, "src", "retro_logo.png");
                if (!File.Exists(logoPath))
                {
                    logoPath = Path.Combine(baseDir, "..", "..", "src", "retro_logo.png");
                }
                if (File.Exists(logoPath))
                {
                    picLogo.Image = Image.FromFile(logoPath);
                }
                else if (this.Icon != null)
                {
                    picLogo.Image = this.Icon.ToBitmap();
                }
            }
            catch { }
            pnlTitleBar.Controls.Add(picLogo);

            // Texto do Título
            lblTitleText = new Label
            {
                Text = "UNLOCKER   v1.0.0",
                Location = new Point(50, 11),
                Size = new Size(460, 20),
                Font = GetModernFont(10F, FontStyle.Bold),
                ForeColor = TextMain,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlTitleBar.Controls.Add(lblTitleText);

            // Botão Fechar (✕)
            btnClose = new Button
            {
                Text = "✕",
                Location = new Point(594, 6),
                Size = new Size(36, 30),
                FlatStyle = FlatStyle.Flat,
                ForeColor = TextMuted,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 29, 72);
            btnClose.Click += (s, e) => this.Close();
            pnlTitleBar.Controls.Add(btnClose);

            // Botão Minimizar (—)
            btnMinimize = new Button
            {
                Text = "—",
                Location = new Point(554, 6),
                Size = new Size(36, 30),
                FlatStyle = FlatStyle.Flat,
                ForeColor = TextMuted,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 32, 48);
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            pnlTitleBar.Controls.Add(btnMinimize);

            // -----------------------------------------------------------------
            // 2. CARD DE STATUS
            // -----------------------------------------------------------------
            pnlStatusCard = new Panel
            {
                Location = new Point(24, 56),
                Size = new Size(592, 82),
                BackColor = CardBg
            };
            this.Controls.Add(pnlStatusCard);

            // Ponto de Status (Dot)
            pnlStatusDot = new Panel
            {
                Location = new Point(22, 33),
                Size = new Size(14, 14),
                BackColor = AccentRed
            };
            pnlStatusCard.Controls.Add(pnlStatusDot);

            // Divisor vertical no Status
            pnlStatusDivider = new Panel
            {
                Location = new Point(50, 18),
                Size = new Size(1, 46),
                BackColor = BorderSubtle
            };
            pnlStatusCard.Controls.Add(pnlStatusDivider);

            // Título do Status
            lblStatusHeader = new Label
            {
                Text = "STATUS:  PARADO",
                Location = new Point(66, 16),
                Size = new Size(500, 24),
                Font = GetModernFont(12F, FontStyle.Bold),
                ForeColor = AccentRed
            };
            pnlStatusCard.Controls.Add(lblStatusHeader);

            // Detalhe do Status
            lblStatusDetail = new Label
            {
                Text = "Clique em 'INICIAR UNLOCKER AUTOMÁTICO' para ativar o desbloqueio.",
                Location = new Point(67, 43),
                Size = new Size(500, 22),
                Font = GetModernFont(9F, FontStyle.Regular),
                ForeColor = TextMuted
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
                BackColor = Color.FromArgb(32, 20, 52),
                ForeColor = TextMain,
                Font = GetModernFont(12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStartMitm.FlatAppearance.BorderSize = 1;
            btnStartMitm.FlatAppearance.BorderColor = AccentPurple;
            btnStartMitm.Click += BtnStartMitm_Click;
            this.Controls.Add(btnStartMitm);

            // Botão PARAR UNLOCKER (Legibilidade total com contraste limpo e sem cansar a visão)
            btnStopMitm = new Button
            {
                Text = "⏹   PARAR UNLOCKER",
                Location = new Point(24, 218),
                Size = new Size(592, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = CardBg,
                ForeColor = TextMain,
                Font = GetModernFont(10.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStopMitm.FlatAppearance.BorderSize = 1;
            btnStopMitm.FlatAppearance.BorderColor = BorderSubtle;
            btnStopMitm.Click += BtnStopMitm_Click;
            this.Controls.Add(btnStopMitm);

            // -----------------------------------------------------------------
            // 4. LINHA DIVISÓRIA HORIZONTAL
            // -----------------------------------------------------------------
            pnlMainDivider = new Panel
            {
                Location = new Point(24, 276),
                Size = new Size(592, 1),
                BackColor = BorderSubtle
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
                BackColor = CardBg,
                ForeColor = TextMain,
                Font = GetModernFont(9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnInstallCert.FlatAppearance.BorderSize = 1;
            btnInstallCert.FlatAppearance.BorderColor = BorderSubtle;
            btnInstallCert.Click += BtnInstallCert_Click;
            this.Controls.Add(btnInstallCert);

            btnOpenLogs = new Button
            {
                Text = "📄   Abrir Logs",
                Location = new Point(222, 290),
                Size = new Size(186, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = CardBg,
                ForeColor = TextMain,
                Font = GetModernFont(9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnOpenLogs.FlatAppearance.BorderSize = 1;
            btnOpenLogs.FlatAppearance.BorderColor = BorderSubtle;
            btnOpenLogs.Click += BtnOpenLogs_Click;
            this.Controls.Add(btnOpenLogs);

            btnDiscord = new Button
            {
                Text = "💬   Discord Oficial",
                Location = new Point(420, 290),
                Size = new Size(196, 44),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(28, 20, 48),
                ForeColor = AccentPink,
                Font = GetModernFont(9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDiscord.FlatAppearance.BorderSize = 1;
            btnDiscord.FlatAppearance.BorderColor = AccentPink;
            btnDiscord.Click += (s, e) => OpenDiscordLink();
            this.Controls.Add(btnDiscord);

            // -----------------------------------------------------------------
            // 6. AVISO DE GOLPE / SCAMMER (Rodapé)
            // -----------------------------------------------------------------
            lblScamWarning = new LinkLabel
            {
                Text = "⚠️ Se você pagou por este programa, você foi ENGANADO!\nClique para entrar no Discord Oficial: https://discord.gg/GvzPKRGxrs",
                Location = new Point(24, 348),
                Size = new Size(592, 65),
                Font = GetModernFont(9F, FontStyle.Bold),
                LinkColor = AccentPink,
                ActiveLinkColor = AccentCyan,
                VisitedLinkColor = AccentPink,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(20, 16, 32),
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

        // Desenha a borda sutil elegante em volta da janela inteira (Sem neon cegante)
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(BorderSubtle, 1.5f))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
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
                lblStatusHeader.ForeColor = AccentCyan;
                pnlStatusDot.BackColor = AccentCyan;
                lblStatusDetail.Text = "Proxy ativo interceptando bhvrdbd.com. Pode abrir o DBD e jogar!";
                lblStatusDetail.ForeColor = TextMain;

                btnStartMitm.Enabled = false;
                btnStartMitm.BackColor = CardBg;
                btnStartMitm.FlatAppearance.BorderColor = BorderSubtle;
                btnStartMitm.ForeColor = TextMuted;

                btnStopMitm.Enabled = true;
                btnStopMitm.BackColor = Color.FromArgb(48, 16, 30);
                btnStopMitm.FlatAppearance.BorderColor = AccentRed;
                btnStopMitm.ForeColor = TextMain;
            }
            else
            {
                lblStatusHeader.Text = "STATUS:  PARADO";
                lblStatusHeader.ForeColor = AccentRed;
                pnlStatusDot.BackColor = AccentRed;
                lblStatusDetail.Text = "Clique em 'INICIAR UNLOCKER AUTOMÁTICO' para ativar o desbloqueio.";
                lblStatusDetail.ForeColor = TextMuted;

                btnStartMitm.Enabled = true;
                btnStartMitm.BackColor = Color.FromArgb(32, 20, 52);
                btnStartMitm.FlatAppearance.BorderColor = AccentPurple;
                btnStartMitm.ForeColor = TextMain;

                btnStopMitm.Enabled = false;
                btnStopMitm.BackColor = CardBg;
                btnStopMitm.FlatAppearance.BorderColor = BorderSubtle;
                btnStopMitm.ForeColor = TextMuted;
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
