# bw-unlocker v1.0.0

> 🔔 **Support the project & updates:**  
> Subscribe to YouTube channel: **[youtube.com/@bwzeraaa](http://www.youtube.com/@bwzeraaa)**  
> 💬 **Official Discord:** **[https://discord.gg/GvzPKRGxrs](https://discord.gg/GvzPKRGxrs)**

---

## 🎮 Supported Platform & Version Disclaimer

> ⚠️ **ATTENTION:** Currently, this project is available **EXCLUSIVELY for the EPIC GAMES STORE version**.  
> Other platforms (Steam, MS Store) are not currently supported in this release.

> 🛠️ **VERSION DISCLAIMER:** This release is **NOT a final version** and may experience instability or unexpected game crashes. If you encounter any bugs, please report them in our Official Discord server.

---

## 📖 Step-by-Step Usage Guide

1. **Prerequisites**:
   - Have Dead by Daylight installed via **Epic Games Store**.
   - **Python 3.10+** installed (make sure to check `Add Python to PATH` during setup).
   - **.NET Framework 4.8** installed on Windows.

2. **Step 1 — Install SSL Certificate (First time only)**:
   - Run `DBD-Loader.exe`.
   - Click the **`🔗 Install SSL`** button.
   - The local Root CA proxy certificate will be generated and silently imported into Windows Trusted Root Store.

3. **Step 2 — Start the Unlocker**:
   - Click the **`▶ START AUTOMATIC UNLOCKER`** button.
   - Panel status will change to **STATUS: ACTIVE** (cyan color) and the internal proxy server will start silently in the background.

4. **Step 3 — Play the Game**:
   - Launch Dead by Daylight normally through **Epic Games Launcher**.
   - The proxy will automatically intercept and synchronize inventory, outfits, banners, badges, and bloodweb rules.

5. **Step 4 — Stop the Unlocker**:
   - When finished playing, click **`⏹ STOP UNLOCKER`** to restore your default Windows proxy settings.

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

## 📁 Project Directory Structure

```text
├── Loader/             # C# (.NET WinForms) Graphical User Interface
├── MITM-Proxy/         # Python MITM Server and Addon
├── Tools/              # SSL Certificate Manager Utilities
└── helps/              # JSON Rule Files (Market.json, GetAll.json, Bloodweb.json)
```
