#nullable enable
using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace DbdLoader
{
    internal static class MitmManager
    {
        private static Process? _proxyProcess = null;
        public const string ProxyAddress = "127.0.0.1:8888";

        public static bool IsProxyRunning => _proxyProcess != null && !_proxyProcess.HasExited;

        private static ProcessStartInfo CreateSilentPythonStartInfo(string scriptPath, string arguments = "")
        {
            string fullArgs = string.IsNullOrEmpty(arguments) ? $"\"{scriptPath}\"" : $"\"{scriptPath}\" {arguments}";
            string pythonExe = "pythonw";

            try
            {
                using var p = Process.Start(new ProcessStartInfo
                {
                    FileName = "pythonw",
                    Arguments = "-V",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });
            }
            catch
            {
                pythonExe = "python";
            }

            return new ProcessStartInfo
            {
                FileName = pythonExe,
                Arguments = fullArgs,
                WorkingDirectory = Path.GetDirectoryName(scriptPath),
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
        }

        public static bool StartMitmServer(out string error)
        {
            error = string.Empty;
            try
            {
                if (IsProxyRunning)
                {
                    error = "O servidor MITM Proxy já está em execução.";
                    return false;
                }

                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string serverScript = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "MITM-Proxy", "server.py"));

                if (!File.Exists(serverScript))
                {
                    serverScript = Path.GetFullPath(Path.Combine(baseDir, "MITM-Proxy", "server.py"));
                }

                if (!File.Exists(serverScript))
                {
                    error = $"Script MITM-Proxy/server.py não encontrado em:\n{serverScript}";
                    return false;
                }

                ProcessStartInfo psi = CreateSilentPythonStartInfo(serverScript);

                _proxyProcess = Process.Start(psi);
                SetSystemProxy(true, ProxyAddress);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static bool StopMitmServer(out string error)
        {
            error = string.Empty;
            try
            {
                if (_proxyProcess != null && !_proxyProcess.HasExited)
                {
                    _proxyProcess.Kill();
                    _proxyProcess = null;
                }

                SetSystemProxy(false, "");
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static bool InstallCert(out string error)
        {
            error = string.Empty;
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string certScript = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "Tools", "Cert-Manager", "manage_cert.py"));

                if (!File.Exists(certScript))
                {
                    certScript = Path.GetFullPath(Path.Combine(baseDir, "Tools", "Cert-Manager", "manage_cert.py"));
                }

                ProcessStartInfo psi = CreateSilentPythonStartInfo(certScript);

                Process.Start(psi);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static void DisableSystemProxyOnly()
        {
            SetSystemProxy(false, "");
        }

        private static void SetSystemProxy(bool enable, string proxyServer)
        {
            try
            {
                using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
                if (key != null)
                {
                    key.SetValue("ProxyEnable", enable ? 1 : 0);
                    if (enable)
                    {
                        key.SetValue("ProxyServer", proxyServer);
                        key.SetValue("ProxyOverride", "*.easyanticheat.net;*.epicgames.com;*.live.eac.net;*.eac-cdn.com;<local>");
                    }
                }
            }
            catch
            {
                // Silencioso se permissões forem restritas
            }
        }
    }
}
