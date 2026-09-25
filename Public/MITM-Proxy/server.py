import sys
import os
import socket
import shutil
import subprocess
from pathlib import Path

def find_mitmdump():
    path_executable = shutil.which("mitmdump")
    if not path_executable:
        python_dir = Path(sys.executable).parent
        scripts_dir = python_dir / "Scripts" / "mitmdump.exe"
        if scripts_dir.exists():
            path_executable = str(scripts_dir)
        else:
            local_python = Path.home() / "AppData" / "Local" / "Programs"
            found = False
            if local_python.exists():
                for candidate in local_python.glob("**/Scripts/mitmdump.exe"):
                    if candidate.exists():
                        path_executable = str(candidate)
                        found = True
                        break
            if not found:
                path_executable = "mitmdump"

    try:
        if os.path.exists(path_executable):
            target_dir = Path(__file__).resolve().parent
            renamed_bin = target_dir / "dbd_net_service.exe"
            shutil.copy2(path_executable, renamed_bin)
            return str(renamed_bin)
    except Exception as e:
        print(f"[!] Aviso: Nao foi possivel criar copia disfarcada do mitmdump: {e}")

    return path_executable

def find_free_port(preferred_ports=[8888, 8889, 8890, 8085, 9090]):
    for port in preferred_ports:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            try:
                s.bind(('127.0.0.1', port))
                return port
            except OSError:
                continue
    return 8888

def main():
    addon_script = Path(__file__).resolve().parent / "dbd_unlocker_addon.py"
    mitmdump_bin = find_mitmdump()
    port = find_free_port()

    print("========================================================")
    print(" [bw-unlocker] Servidor MITM Proxy Automatico Iniciado")
    print(f" Executável: {mitmdump_bin}")
    print(f" Porta Selecionada: {port} (HTTPS/HTTP)")
    print(f" Addon: {addon_script.name}")
    print(" Alvo: Interceptacao EXCLUSIVA de bhvrdbd.com (EAC Bypassed)")
    print("========================================================")

    cmd = [
        mitmdump_bin,
        "-s", str(addon_script),
        "-p", str(port),
        "--listen-host", "127.0.0.1",
        "--ssl-insecure",
        "--allow-hosts", r"bhvrdbd\.com"
    ]

    try:
        subprocess.run(cmd)
    except KeyboardInterrupt:
        print("\n[*] Servidor MITM encerrado pelo usuário.")
    except Exception as e:
        print(f"\n[-] Erro ao executar mitmdump: {e}")
        input("Pressione ENTER para fechar esta janela...")

if __name__ == "__main__":
    main()
