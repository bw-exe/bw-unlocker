#nullable enable
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DbdLoader
{
    public partial class MainForm : Form
    {
        private Button btnStartMitm = null!;
        private Button btnStopMitm = null!;
        private Button btnInstallCert = null!;
        private Button btnOpenLogs = null!;
        private Label lblStatusHeader = null!;
        private Label lblStatusDetail = null!;
        private Panel statusCard = null!;

        public MainForm()
        {
            InitializeComponent();
            SetupUI();
            UpdateMitmUI(false);
        }

        private void SetupUI()
        {
            this.Text = "DBD MITM Unlocker v11.0.0 - 100% Automático";
            this.Size = new Size(460, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(24, 24, 28);
            this.ForeColor = Color.White;

            // Painel de Status (Card no topo)
            statusCard = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(405, 85),
                BackColor = Color.FromArgb(35, 35, 42),
            };
            this.Controls.Add(statusCard);

            lblStatusHeader = new Label
            {
                Text = "STATUS: PARADO",
                Location = new Point(15, 12),
                Size = new Size(375, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.IndianRed
            };
            statusCard.Controls.Add(lblStatusHeader);

            lblStatusDetail = new Label
            {
                Text = "Clique em 'INICIAR UNLOCKER AUTOMÁTICO' -> Abra o DBD e jogue à vontade! Skins, itens, perks, banners e partidas sem kick EAC.",
                Location = new Point(16, 38),
                Size = new Size(375, 42),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.Gray
            };
            statusCard.Controls.Add(lblStatusDetail);

            // Botão Principal: Iniciar MITM
            btnStartMitm = new Button
            {
                Text = "▶  INICIAR UNLOCKER AUTOMÁTICO",
                Location = new Point(20, 120),
                Size = new Size(405, 48),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(46, 160, 67),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStartMitm.FlatAppearance.BorderSize = 0;
            btnStartMitm.Click += BtnStartMitm_Click;
            this.Controls.Add(btnStartMitm);

            // Botão: Parar MITM
            btnStopMitm = new Button
            {
                Text = "⏹  PARAR UNLOCKER",
                Location = new Point(20, 178),
                Size = new Size(405, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(180, 50, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStopMitm.FlatAppearance.BorderSize = 0;
            btnStopMitm.Click += BtnStopMitm_Click;
            this.Controls.Add(btnStopMitm);

            // Botão secundário: Instalar Certificado SSL
            btnInstallCert = new Button
            {
                Text = "🔑  Instalar Certificado SSL",
                Location = new Point(20, 235),
                Size = new Size(270, 38),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(45, 45, 55),
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnInstallCert.FlatAppearance.BorderSize = 1;
            btnInstallCert.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 80);
            btnInstallCert.Click += BtnInstallCert_Click;
            this.Controls.Add(btnInstallCert);

            // Botão secundário: Abrir Logs
            btnOpenLogs = new Button
            {
                Text = "📂  Abrir Logs",
                Location = new Point(300, 235),
                Size = new Size(125, 38),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(45, 45, 55),
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnOpenLogs.FlatAppearance.BorderSize = 1;
            btnOpenLogs.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 80);
            btnOpenLogs.Click += BtnOpenLogs_Click;
            this.Controls.Add(btnOpenLogs);
        }

        private void UpdateMitmUI(bool isRunning)
        {
            if (isRunning)
            {
                lblStatusHeader.Text = "● UNLOCKER ATIVO (100% Automático)";
                lblStatusHeader.ForeColor = Color.LightGreen;
                lblStatusDetail.Text = "Proxy ativo interceptando bhvrdbd.com. EAC bypassed! Pode abrir o DBD e jogar partidas normais!";
                lblStatusDetail.ForeColor = Color.FromArgb(180, 230, 180);
                
                btnStartMitm.Enabled = false;
                btnStopMitm.Enabled = true;
                statusCard.BackColor = Color.FromArgb(25, 45, 30);
            }
            else
            {
                lblStatusHeader.Text = "○ STATUS: PARADO";
                lblStatusHeader.ForeColor = Color.IndianRed;
                lblStatusDetail.Text = "Clique em 'INICIAR UNLOCKER AUTOMÁTICO' para ativar o desbloqueio.";
                lblStatusDetail.ForeColor = Color.Gray;

                btnStartMitm.Enabled = true;
                btnStopMitm.Enabled = false;
                statusCard.BackColor = Color.FromArgb(35, 35, 42);
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
