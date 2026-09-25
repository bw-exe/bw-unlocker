# bw-unlocker

> 🔔 **Apoie o projeto e novidades:**  
> Inscreva-se no canal do YouTube: **[youtube.com/@bwzeraaa](http://www.youtube.com/@bwzeraaa)** para acompanhar atualizações, tutoriais e novidades em primeira mão!

---

## ⚠️ Isenção de Responsabilidade (Disclaimer)

> **AVISO IMPORTANTE:**  
> Este projeto foi desenvolvido para **fins educacionais e de pesquisa de segurança/engenharia reversa**.  
> O uso de qualquer modificação, proxy de rede ou substituição de arquivos em jogos online pode violar os Termos de Serviço (ToS) da desenvolvedora.  
> **Não nos responsabilizamos por quaisquer banimentos, suspensões de conta, perdas ou penalidades aplicadas à sua conta.** O uso é inteiramente por sua conta e risco.

---

## 📁 Estrutura de Pastas do Repositório

```text
├── Public/             # PROJETO COMPLETO PÚBLICO (Para publicar no GitHub)
│   ├── Loader/         # Interface gráfica em C# (.NET WinForms)
│   ├── MITM-Proxy/     # Servidor e Addon Python (Personagens possuídos)
│   ├── Tools/          # Gerenciador de Certificados SSL
│   └── helps/          # Arquivos de regras JSON
│
└── Private/            # PROJETO COMPLETO PESSOAL (Full Unlock - No .gitignore, NUNCA vai ao GitHub)
    ├── Loader/         # Interface gráfica em C# (.NET WinForms)
    ├── MITM-Proxy/     # Servidor e Addon Python (Full Unlock)
    ├── Tools/          # Gerenciador de Certificados SSL
    └── helps/          # Arquivos de regras JSON
```

---

## 🚀 Como Usar Cada Versão

- **Para Usar a Versão Pessoal (Full Unlock)**:
  - Acesse a pasta `Private/Loader/bin/Release/DBD-Loader.exe` e execute.
- **Para Publicar no GitHub**:
  - A pasta `Private/` está listada no `.gitignore`, então o Git ignorará ela automaticamente. Você pode commit do conteúdo da pasta `Public/` para o seu repositório.
