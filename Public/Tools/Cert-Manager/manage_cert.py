import os
import sys
import subprocess
from pathlib import Path

def install_cert():
    cert_path = Path.home() / ".mitmproxy" / "mitmproxy-ca-cert.cer"
    
    if not cert_path.exists():
        print(f"[-] Certificado mitmproxy não encontrado em: {cert_path}")
        print("[!] Execute o servidor MITM (MITM-Proxy/server.py) uma vez para gerar o certificado.")
        return False

    print(f"[*] Instalando certificado Root CA: {cert_path}")
    cmd = ["certutil", "-addstore", "Root", str(cert_path)]
    try:
        res = subprocess.run(cmd, capture_output=True, text=True)
        if res.returncode == 0:
            print("[+] Certificado Root CA instalado com sucesso no Windows!")
            return True
        else:
            print(f"[-] Erro ao instalar certificado: {res.stderr}")
            return False
    except Exception as e:
        print(f"[-] Exceção ao executar certutil: {e}")
        return False

def remove_cert():
    print("[*] Removendo certificado mitmproxy do repositório Root do Windows...")
    cmd = ["certutil", "-delstore", "Root", "mitmproxy"]
    try:
        subprocess.run(cmd)
        print("[+] Certificado removido com sucesso.")
    except Exception as e:
        print(f"[-] Erro ao remover certificado: {e}")

def main():
    if len(sys.argv) > 1 and sys.argv[1] == "remove":
        remove_cert()
    else:
        install_cert()

if __name__ == "__main__":
    main()
