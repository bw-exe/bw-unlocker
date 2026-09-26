# bw-unlocker (Public Version v1.0.0 - GitHub)

🌐 **Language / Idioma:** [Versão em Português](README.md) | **English Version**

> 🔔 **Support the project & updates:**  
> Subscribe to YouTube channel: **[youtube.com/@bwzeraaa](http://www.youtube.com/@bwzeraaa)**  
> 💬 **Official Discord:** **[https://discord.gg/GvzPKRGxrs](https://discord.gg/GvzPKRGxrs)**

---

## 🎮 Supported Platform & Version Disclaimer

> ⚠️ **ATTENTION:** Currently, this project is available **EXCLUSIVELY for the EPIC GAMES STORE version**.  
> Other platforms (Steam, MS Store) are not currently supported in this release.

> 🛠️ **VERSION DISCLAIMER:** This release is **NOT a final version** and may experience instability or unexpected game crashes. If you encounter any bugs, please report them in our Official Discord server.

---

## ⚠️ SCAM WARNING (100% FREE)

> 🚨 **THIS SOFTWARE IS 100% FREE!**  
> If you paid for this unlocker on any website, channel, or seller, **you have been scammed!**  
> Report scammers and ask questions on our Official Discord: **[https://discord.gg/GvzPKRGxrs](https://discord.gg/GvzPKRGxrs)**

---

## ⚠️ Disclaimer

> **IMPORTANT NOTICE:**  
> This project was developed strictly for **educational, security research, and reverse engineering purposes**.  
> Using any network proxy, game file modification, or memory spoofing in online games may violate the game developer's Terms of Service (ToS).  
> **We take no responsibility for account bans, suspensions, losses, or penalties applied to your account.** Use entirely at your own risk.

---

## 📖 How It Works (MITM Proxy Method - Public Version)

- **Local Proxy Server:** Spawns a local instance of `mitmproxy` listening on `127.0.0.1:8888`.
- **Secure Network Routing:** Configures Windows system proxy to route game endpoint requests locally.
- **HTTPS Interception:** Uses a locally installed trusted Root CA SSL certificate.
- **Response Injection (Spoofing for Owned Characters):**
  - Respects original account entitlement (`isEntitled`).
  - Intercepts `/api/v1/dbd-inventories` / `/player-card` to inject outfits, banners, and badges.
  - Intercepts `/api/v1/dbd-character-data/get-all` to sync prestige (50+), level 50 bloodweb, and 3x items on owned characters.
  - Intercepts `/bloodweb` to deliver level 50 completed bloodweb without purchase locks.
  - Blocks telemetry reporting to `/api/v1/gameLogs/batch`.

---

## 🚀 Step-by-Step Usage Guide

1. **Prerequisites**:
   - Windows 10 or 11 (64-bit).
   - Game installed via **Epic Games Store**.
   - **Python 3.10+** (with `pip` added to system PATH).
   - **.NET Framework 4.8** (to run the Loader).

2. **Step 1 — Install Python Dependencies (First time only)**:
   ```bash
   pip install mitmproxy
   ```

3. **Step 2 — Install SSL Certificate (First time only)**:
   - Run `DBD-Loader.exe` located at `Public/Loader/bin/Release/`.
   - Click **`🔗 Install SSL`**.
   - The local Root CA certificate will be installed silently into Windows.

4. **Step 3 — Start the Unlocker**:
   - Click **`▶ START AUTOMATIC UNLOCKER`**.
   - Panel status will turn **STATUS: ACTIVE** and the proxy will start silently in the background.

5. **Step 4 — Play the Game**:
   - Launch Dead by Daylight on **Epic Games Launcher** and enjoy.

6. **Step 5 — Stop**:
   - Click **`⏹ STOP UNLOCKER`** when finished.

---

## 📁 Public Directory Structure

```text
├── Loader/             # C# (.NET WinForms) GUI and MITM Manager
├── MITM-Proxy/         # Python Proxy Server and Public Addon Script
├── Tools/              # SSL Certificate Manager Script
└── helps/              # JSON Rule Files (Market.json, GetAll.json, Bloodweb.json)
```

---

## 📺 Community & Credits
- Official Channel: [bwzeraaa on YouTube](http://www.youtube.com/@bwzeraaa)
- Official Discord: [https://discord.gg/GvzPKRGxrs](https://discord.gg/GvzPKRGxrs)
- Contributions and PRs are welcome!
