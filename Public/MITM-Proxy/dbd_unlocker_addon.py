import json
import logging
import hashlib
from pathlib import Path
from mitmproxy import http

logging.basicConfig(level=logging.INFO, format="[mitm-unlocker-public] %(asctime)s - %(message)s")

class DBDUnlockerAddon:
    def __init__(self):
        logging.info("DBD Unlocker MITM Addon (Versão Pública) Inicializado.")
        
        base_root = Path(__file__).resolve().parent.parent
        self.helps_dir = base_root / "helps"
        
        if not self.helps_dir.exists():
            self.helps_dir = Path(__file__).resolve().parent.parent.parent / "helps"

        self.market_dir = self.helps_dir / "MarketFiles"
        logging.info(f"Diretório de regras selecionado: {self.market_dir}")
        
        self.market_data = self.load_json_file(self.market_dir / "Market.json")
        self.get_all_data = self.load_json_file(self.market_dir / "GetAll.json")
        self.bloodweb_data = self.load_json_file(self.market_dir / "Bloodweb.json")

        self.default_character_items = []
        if self.bloodweb_data and "characterItems" in self.bloodweb_data:
            self.default_character_items = self.bloodweb_data["characterItems"]
        elif self.get_all_data and "list" in self.get_all_data and self.get_all_data["list"]:
            self.default_character_items = self.get_all_data["list"][0].get("characterItems", [])

    def load_json_file(self, filepath: Path):
        if filepath.exists():
            try:
                data = json.loads(filepath.read_text(encoding="utf-8"))
                logging.info(f"[+] Carregado arquivo com sucesso: {filepath.name} ({len(data)} chaves)")
                return data
            except Exception as e:
                logging.error(f"[-] Erro ao carregar {filepath.name}: {e}")
        else:
            logging.warning(f"[!] Arquivo não encontrado: {filepath}")
        return None

    def get_random_prestige(self, char_name: str) -> int:
        """ Gera um nível de prestígio único e variado de no MÍNIMO 50 (entre 50 e 100) para cada personagem possuído """
        if not char_name:
            return 50
        h = hashlib.md5(char_name.encode('utf-8')).hexdigest()
        num = int(h[:8], 16)
        levels = [50, 53, 56, 60, 65, 70, 74, 78, 82, 85, 90, 93, 97, 100]
        return levels[num % len(levels)]

    def is_killer(self, char_name: str) -> bool:
        if not char_name:
            return False
        known_killers = {
            'Spirit', 'Nurse', 'Shape', 'Oni', 'Pig', 'Hag', 'Clown', 'Plague', 
            'Legion', 'Ghostface', 'Demogorgon', 'Gunslinger', 'Cannibal', 'HillBilly', 
            'Chuckles', 'Bear', 'Witch', 'Nightmare', 'Bob', 'Killer07'
        }
        return char_name in known_killers or char_name.startswith('K')

    def merge_character_items(self, target_list: list, char_name: str = "", default_qty: int = 3) -> list:
        """ Garante que apenas os itens apropriados do spoof existam no target_list com quantidade 3 """
        if not isinstance(target_list, list):
            target_list = []
        
        char_is_killer = self.is_killer(char_name)

        item_map = {item.get("itemId"): item for item in target_list if isinstance(item, dict) and "itemId" in item}
        for spoof_item in self.default_character_items:
            item_id = spoof_item.get("itemId")
            if not item_id:
                continue

            # Evita injetar itens de Sobrevivente (Item_Camper_*) em Assassinos (Killers)
            if char_is_killer and ("Camper" in item_id or "Item_Camper" in item_id):
                continue
            # Evita injetar itens de Assassino em Sobreviventes
            if not char_is_killer and ("Slasher" in item_id or "Killer" in item_id):
                continue

            if item_id in item_map:
                item_map[item_id]["quantity"] = max(item_map[item_id].get("quantity", 0), default_qty)
            else:
                new_item = dict(spoof_item)
                new_item["quantity"] = default_qty
                target_list.append(new_item)
                item_map[item_id] = new_item
        return target_list

    def process_character_entry(self, entry: dict, spoof_entry: dict = None):
        """ Aplica prestígio variado (50..100), nível 50 e injeta characterItems (x3) APENAS se o personagem for possuído pelo usuário """
        if not isinstance(entry, dict):
            return
        
        # Versão Pública: Apenas processa se for um personagem possuído (isEntitled == True)
        if not entry.get("isEntitled", False):
            return

        char_name = entry.get("characterName") or (spoof_entry.get("characterName") if spoof_entry else "")
        
        entry["prestigeLevel"] = self.get_random_prestige(char_name)
        entry["legacyPrestigeLevel"] = 3
        entry["bloodWebLevel"] = 50

        if spoof_entry and "customizations" in spoof_entry and "customizations" not in entry:
            entry["customizations"] = spoof_entry["customizations"]

        existing_items = entry.get("characterItems", [])
        entry["characterItems"] = self.merge_character_items(existing_items, char_name=char_name, default_qty=3)

    def make_level50_bloodweb(self, orig_json: dict, req_char_name: str = "") -> dict:
        """ Converte a teia do personagem em Nível 50 concluída para personagens possuídos sem corromper a estrutura do jogo """
        if not isinstance(orig_json, dict):
            return orig_json

        if not orig_json.get("isEntitled", True):
            return orig_json

        char_name = req_char_name or orig_json.get("characterName", "")
        
        orig_json["bloodwebLevel"] = 50
        orig_json["bloodwebLevelChanged"] = True
        orig_json["prestigeLevel"] = self.get_random_prestige(char_name)
        orig_json["legacyPrestigeLevel"] = 3
        orig_json["updatedWallets"] = [{"currencyType": "Bloodpoints", "balance": 2000000}]
        orig_json["characterItems"] = self.merge_character_items(orig_json.get("characterItems", []), char_name=char_name, default_qty=3)

        bwd = orig_json.get("bloodWebData")
        if isinstance(bwd, dict):
            ring_data = bwd.get("ringData", [])
            if isinstance(ring_data, list):
                for ring in ring_data:
                    if isinstance(ring, dict) and "nodeData" in ring and isinstance(ring["nodeData"], list):
                        for node in ring["nodeData"]:
                            if isinstance(node, dict):
                                node["state"] = "Collected"

        return orig_json

    def response(self, flow: http.HTTPFlow) -> None:
        url = flow.request.pretty_url

        if "bhvrdbd.com" not in url:
            return

        # 1. Inventários / Player Cards / Banners / Badges
        if any(k in url for k in ["/dbd-inventories", "/player-card", "/player-profile", "/card"]) and self.market_data:
            logging.info(f"[+] Interceptado endpoint de inventário/player-card: {url} - Injetando skins, banners e badges...")
            try:
                if flow.response.status_code >= 400:
                    flow.response.status_code = 200

                orig_body = flow.response.get_text()
                orig_json = json.loads(orig_body) if orig_body else {}
                
                orig_items = orig_json.get("inventoryItems", [])
                if not isinstance(orig_items, list):
                    orig_items = []

                spoof_items = self.market_data.get("inventoryItems", [])
                existing_map = {item.get("objectId"): item for item in orig_items if isinstance(item, dict) and "objectId" in item}

                added_count = 0
                for spoof_item in spoof_items:
                    obj_id = spoof_item.get("objectId")
                    if not obj_id:
                        continue
                    if obj_id not in existing_map:
                        orig_items.append(spoof_item)
                        existing_map[obj_id] = spoof_item
                        added_count += 1
                    else:
                        existing_map[obj_id]["quantity"] = spoof_item.get("quantity", 1)

                orig_json["inventoryItems"] = orig_items
                flow.response.set_text(json.dumps(orig_json))
                logging.info(f"[+] Sucesso! Injetadas {added_count} skins, Banners e Badges.")
            except Exception as e:
                logging.error(f"[-] Erro ao mesclar inventários ({url}): {e}")

        # 2. Dados de Personagem (/api/v1/dbd-character-data/...)
        elif "/api/v1/dbd-character-data" in url:
            logging.info(f"[+] Interceptado endpoint de dados do personagem: {url}")
            try:
                if flow.response.status_code >= 400:
                    flow.response.status_code = 200

                orig_body = flow.response.get_text()
                if not orig_body:
                    return

                orig_json = json.loads(orig_body)
                if not isinstance(orig_json, dict):
                    return

                # Caso A: Lista completa de personagens (/get-all)
                if "list" in orig_json and isinstance(orig_json["list"], list):
                    orig_list = orig_json["list"]
                    spoof_list = self.get_all_data.get("list", []) if self.get_all_data else []
                    spoof_map = {entry.get("characterName"): entry for entry in spoof_list if isinstance(entry, dict) and "characterName" in entry}

                    for entry in orig_list:
                        if isinstance(entry, dict):
                            char_name = entry.get("characterName")
                            spoof_entry = spoof_map.get(char_name)
                            self.process_character_entry(entry, spoof_entry)

                    orig_json["list"] = orig_list
                    flow.response.set_text(json.dumps(orig_json))
                    logging.info("[+] Sucesso! Personagens possuídos atualizados com Prestígio 50+ e Nível 50.")

                # Caso B: Requisição da Teia de Sangue (/bloodweb) - Apenas se contiver bloodWebData válido
                elif "/bloodweb" in url and "bloodWebData" in orig_json:
                    req_text = flow.request.get_text()
                    req_char_name = None
                    if req_text:
                        try:
                            req_json = json.loads(req_text)
                            req_char_name = req_json.get("characterName")
                        except Exception:
                            pass

                    if not req_char_name:
                        req_char_name = orig_json.get("characterName", "")

                    orig_json = self.make_level50_bloodweb(orig_json, req_char_name)
                    flow.response.set_text(json.dumps(orig_json))
                    logging.info(f"[+] Teia de Sangue de {req_char_name} convertida para Nível 50 concluída!")

            except Exception as e:
                logging.error(f"[-] Erro ao processar endpoint de personagem ({url}): {e}")

        # 3. Bloqueio de logs do jogo (/api/v1/gameLogs/batch)
        elif "/api/v1/gameLogs/batch" in url:
            logging.info("[*] Bloqueando envio de gameLogs para o servidor.")
            try:
                req_text = flow.request.get_text()
                req_json = json.loads(req_text) if req_text else {}
                events = req_json.get("Events", req_json.get("logs", []))
                count = len(events) if isinstance(events, list) else 1
                if count < 1:
                    count = 1
                responses = [{"RecordId": f"rec_{i}"} for i in range(count)]
                resp_body = json.dumps({"Encrypted": False, "FailedPutCount": 0, "RequestResponses": responses})
                flow.response = http.Response.make(
                    200,
                    resp_body.encode("utf-8"),
                    {"Content-Type": "application/json"}
                )
            except Exception:
                flow.response = http.Response.make(
                    200,
                    b'{"Encrypted":false,"FailedPutCount":0,"RequestResponses":[{"RecordId":""}]}',
                    {"Content-Type": "application/json"}
                )

addons = [DBDUnlockerAddon()]
