# bw-unlocker (Versão Pública v1.0.0 - GitHub)

> 🔔 **Apoie o projeto e novidades:**  
> Inscreva-se no canal do YouTube: **[youtube.com/@bwzeraaa](http://www.youtube.com/@bwzeraaa)**  
> 💬 **Discord Oficial:** **[https://discord.gg/GvzPKRGxrs](https://discord.gg/GvzPKRGxrs)**

---

## 🎮 Plataforma Suportada & Aviso de Versão

> ⚠️ **ATENÇÃO:** Atualmente, este projeto está disponível **EXCLUSIVAMENTE para a versão da EPIC GAMES STORE**.  
> Outras plataformas (Steam, MS Store) ainda não possuem suporte ativo nesta versão.

> 🛠️ **AVISO DE VERSÃO:** Esta versão **NÃO é a versão definitiva** e pode apresentar instabilidades ou fechamentos inesperados (*crashes*). Caso encontre algum bug, informe em nosso Discord Oficial.

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

## 📖 Como Funciona (Método MITM Proxy - Versão Pública)

- **Servidor Proxy Local:** Inicia uma instância local do `mitmproxy` escutando em `127.0.0.1:8888`.
- **Roteamento de Rede Segura:** Configura o proxy do Windows para direcionar chamadas de endpoints do jogo para o servidor local.
- **Interceptação HTTPS:** Utiliza certificado SSL raiz confiável instalado localmente.
- **Injeção de Respostas (Spoofing para Personagens Possuídos):**
  - Respeita a posse original da conta (`isEntitled`).
  - Intercepta `/api/v1/dbd-inventories` / `/player-card` para injetar skins, banners e badges.
  - Intercepta `/api/v1/dbd-character-data/get-all` para sincronizar prestígio (50+), nível 50 da teia e 3x de cada item em personagens possuídos.
  - Intercepta `/bloodweb` para entregar a teia concluída no nível 50 sem travamento de compra.
  - Bloqueia envios de telemetria em `/api/v1/gameLogs/batch`.

---

## 🚀 Passo a Passo de Como Usar

1. **Pré-requisitos**:
   - Windows 10 ou 11 (64-bit).
   - Jogo instalado pela **Epic Games Store**.
   - **Python 3.10+** (com `pip` adicionado ao PATH do sistema).
   - **.NET Framework 4.8** (para executar o Loader).

2. **Passo 1 — Instalar dependências (Apenas na 1ª vez)**:
   ```bash
   pip install mitmproxy
   ```

3. **Passo 2 — Instalar o Certificado SSL (Apenas na 1ª vez)**:
   - Abra o `DBD-Loader.exe` na pasta `Public/Loader/bin/Release/`.
   - Clique em **`🔗 Instalar SSL`**.
   - O certificado Root CA local será instalado silenciosamente no Windows.

4. **Passo 3 — Iniciar o Unlocker**:
   - Clique em **`▶ INICIAR UNLOCKER AUTOMÁTICO`**.
   - O status no painel ficará **STATUS: ATIVO** e o proxy iniciará em segundo plano sem abrir janelas extras.

5. **Passo 4 — Jogar**:
   - Abra o Dead by Daylight no **Epic Games Launcher** e aproveite.

6. **Passo 5 — Encerrar**:
   - Clique em **`⏹ PARAR UNLOCKER`** ao finalizar.

## 📁 Estrutura do Projeto Público

```text
├── Loader/             # Interface gráfica em C# (.NET WinForms) e Gerenciador MITM
├── MITM-Proxy/         # Servidor proxy e script addon Python público
├── Tools/              # Utilitários e script de Certificado SSL
└── helps/              # Arquivos de regras JSON (Market.json, GetAll.json, Bloodweb.json)
```

---

## 📺 Comunidade & Créditos
- Canal oficial: [bwzeraaa no YouTube](http://www.youtube.com/@bwzeraaa)
- Discord Oficial: [https://discord.gg/GvzPKRGxrs](https://discord.gg/GvzPKRGxrs)
- Contribuições e melhorias via Pull Requests são bem-vindas!
