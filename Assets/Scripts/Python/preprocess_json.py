import json
import os

RESOURCES_DIR = "D:/Games/Unity/BrotatoM/Assets/Resources/"


def name_to_path(name: str, part: str):
    """根据name找到相应的Resources的图像加载路径(basename)"""
    base_path = f"ArtAssets/{part}/"
    name = "_".join(name.split())  # 'A B' => 'A_B'
    name = name.replace("'", "%27")  # 转义单引号

    for path in filter(  # 过滤.meta文件
        lambda x: x.endswith(".png"), os.listdir(RESOURCES_DIR + base_path)
    ):
        if name in path:  # 匹配路径
            return base_path + path


def spacial_dispose(part: str, v: dict):
    """
    不同配置表的特殊处理逻辑
    """
    if part == "Items":
        v["Rarity"] = int(v["Rarity"][-1])
    elif part == "Characters":
        v.pop("Item Tags")
    elif part == "Stats":
        v["Path Big"] = v["Path"].replace("20", "60")
    elif part == "Enemies":
        #! 需要手动替换掉Unicode字符u00a0,和†。
        #! 第一个ID由'-'改为'0'。
        #! 第18个"First Wave"在冒号旁加两个空格
        v["Food Drop Rate"] = int(v["Food Drop Rate"][:-1]) / 100
        v["Container Drop Rate"] = int(v["Container Drop Rate"][:-1]) / 100
        v["ID"] = int(v["ID"])
        if "D" in v["First Wave"]:  # example 'D1 : 15', '13 D1 : 10'
            content = v["First Wave"]
            v["First Wave"] = (
                0 if content.split("D1 : ")[0] == "" else int(content.split("D1 : ")[0])
            )
            v["First Wave D1"] = int(content.split("D1 : ")[1])
        else:
            v["First Wave"] = int(v["First Wave"])
            v["First Wave D1"] = 0
    elif part == "Weapons":
        lst = v["Damage"].split()
        # 有些枪没有普通版 "- 10(100%) 15(100%) 25(100%)"
        damage_lst = [int(el[: el.index("(")]) if el != "-" else 0 for el in lst]
        modifier_lst = [
            int(el[el.index("(") + 1 : el.index("%")]) / 100 if el != "-" else 0
            for el in lst
        ]
        v["Damage"] = damage_lst
        v["Damage Modifier"] = modifier_lst

        v["Attack Speed"] = str_to_lst(
            v["Attack Speed"], lambda x: float(x[:-1])
        )  # 1.18s => 1.18

        v["Range"] = str_to_lst(v["Range"], lambda x: int(x))
        v["Class"] = [el.strip() for el in v["Class"].split(",")]
        v["Knockback"] = str_to_lst(v["Knockback"], lambda x: int(x))
        v["Lifesteal"] = str_to_lst(v["Lifesteal"], lambda x: float(x))
        v["Base price"] = str_to_lst(v["Base price"], lambda x: int(x))

        clst = v["Crit Chance/Damage"].split()
        critical_chance_lst = [
            int(el[: el.index("%")]) / 100 if el != "-" else 0 for el in clst
        ]
        critical_damage_lst = [
            float(el[el.index("x") + 1 : el.index(")")]) if el != "-" else 0
            for el in clst
        ]
        v.pop("Crit Chance/Damage")
        v["Crit Chance"] = critical_chance_lst
        v["Crit Multiplicator"] = critical_damage_lst

        v.pop("DPS")


def str_to_lst(content, preprocess):
    return [preprocess(el) if el != "-" else 0 for el in content.split()]


def preprocess_jsons(part):
    with open(f"./{part}.json") as f:
        tables = json.load(f)

    config_lst = []  # 字典转列表
    if part == "Dangers":
        for k, v in tables.items():
            v.pop("Screenshot")
            config_lst.append(v)
    else:
        valid_counts = 0
        for k, v in tables.items():
            v["Path"] = name_to_path(v["Name"], part)

            # 特殊处理
            spacial_dispose(part, v)

            # 把key转换为C#变量名形式
            new_dic = {}
            for key, val in v.items():
                if key == "+hp/wave":
                    newKey = "HpIncreasePerWave"
                elif key == "+dmg/wave":
                    newKey = "DamageIncreasePerWave"
                else:
                    newKey = "".join([s.capitalize() for s in key.split()])
                new_dic[newKey] = v[key]

            config_lst.append(new_dic)
            if os.path.exists(RESOURCES_DIR + v["Path"]):
                valid_counts += 1
            try:
                if os.path.exists(RESOURCES_DIR + v["PathBig"]):
                    valid_counts += 1
            except KeyError:
                pass

        print(f"有效图片数: {valid_counts}")

    with open(f"./Processed{part}.json", "w") as pf:
        json.dump(config_lst, pf)

    print("转换结束")


if __name__ == "__main__":
    parts = [
        # "Characters",
        # "Items",
        # "Weapons",
        # "Enemies",
        # "Dangers",
        "Stats",
    ]
    for part in parts:
        preprocess_jsons(part)
