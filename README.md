# bw-unlocker v1.0.0

> 🔔 **Apoie o projeto e novidades:**  
> Inscreva-se no canal do YouTube: **[youtube.com/@bwzeraaa](http://www.youtube.com/@bwzeraaa)**  
> 💬 **Discord Oficial:** **[https://discord.gg/GvzPKRGxrs](https://discord.gg/GvzPKRGxrs)**

---

## 🎮 Plataforma Suportada & Aviso de Versão

> ⚠️ **ATENÇÃO:** Atualmente, este projeto está disponível **EXCLUSIVAMENTE para a versão da EPIC GAMES STORE**.  
> Outras plataformas (Steam, MS Store) ainda não possuem suporte ativo nesta versão.

> 🛠️ **AVISO DE VERSÃO:** Esta versão **NÃO é a versão definitiva** e pode apresentar instabilidades ou fechamentos inesperados (*crashes*). Caso encontre algum bug, informe em nosso Discord Oficial.

---

## 📖 Passo a Passo de Como Funciona

1. **Pré-requisitos**:
   - Ter o jogo instalado pela **Epic Games Store**.
   - Possuir o **Python 3.10+** instalado (com a opção `Add Python to PATH` marcada na instalação).
   - Possuir o **.NET Framework 4.8** instalado no Windows.

2. **Passo 1 — Instalar o Certificado SSL (Apenas na 1ª vez)**:
   - Execute o painel `DBD-Loader.exe`.
   - Clique no botão **`🔗 Instalar SSL`**.
   - O certificado Root CA local do proxy será gerado e instalado silenciosamente no repositório de segurança do Windows.

3. **Passo 2 — Iniciar o Desbloqueador**:
   - Clique no botão **`▶ INICIAR UNLOCKER AUTOMÁTICO`**.
   - O status no painel mudará para **STATUS: ATIVO** (cor ciano) e o servidor proxy interno será iniciado em segundo plano de forma silenciosa.

4. **Passo 3 — Jogar**:
   - Abra o Dead by Daylight normalmente através do **Epic Games Launcher**.
   - O proxy interceptará e sincronizará as regras de mercado, inventário e teia de sangue automaticamente.

5. **Passo 4 — Encerrar**:
   - Ao terminar de jogar, clique em **`⏹ PARAR UNLOCKER`** para restaurar a configuração original de proxy do seu Windows.

---

## ⚠️ AVISO ANTI-GOLPE (100% GRATUITO)

> 🚨 **ESTE PROGRAMA É 100% GRATUITO!**  
> Se você pagou por este unlocker em qualquer site, canal ou vendedor, **você foi enganado (scammado)!**  
> Denuncie e tire dúvidas no nosso Discord Oficial: **[https://discord.gg/GvzPKRGxrs](https://discord.gg/GvzPKRGxrs)**

---

## ⚠️ Isenção de Responsabilidade (Disclaimer)

> **AVISO IMPORTANTE:**  
> Este projeto foi desenvolvido para **fins educacionais e de pesquisa de segurança/engenharia reversa**.  
> O uso de qualquer modificação, proxy de rede ou substituição de arquivos em jogos online pode violar os Termos de Serviço (ToS) da desenvolvedora.  
> **Não nos responsabilizamos por quaisquer banimentos, suspensões de conta, perdas ou penalidades aplicadas à sua conta.** O uso é inteiramente por sua conta e risco.

---

## 📁 Estrutura de Pastas do Projeto

```text
├── Loader/             # Interface gráfica em C# (.NET WinForms)
├── MITM-Proxy/         # Servidor e Addon Python
├── Tools/              # Gerenciador de Certificados SSL
└── helps/              # Arquivos de regras JSON
```
