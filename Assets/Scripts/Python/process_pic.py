import os
import cv2 as cv
from datetime import datetime
from icecream import ic
import json

ic.configureOutput(
    prefix=lambda: datetime.now().strftime("%y-%m-%d %H:%M:%S | "), includeContext=False
)

IMG_PATH = "D:/Games/Unity/BrotatoM/Assets/Resources/ArtAssets/Upgrades/"

str_to_num_dict = {
    "I": 1,
    "II": 2,
    "III": 3,
    "IV": 4,
}

name_to_ability_dict = {
    "Back": "Dodge",
    "Brain": "ElementalDamage",
    "Chest": "Armor",
    "Eyes": "Range",
    "Fingers": "CritChance",
    "Forearms": "MeleeDamage",
    "Hands": "Harvesting",
    "Heart": "MaxHp",
    "Legs": "Speed",
    "Lungs": "HpRegeneration",
    "Nose": "Luck",
    "Reflexes": "AttackSpeed",
    "Shoulders": "RangedDamage",
    "Skull": "Engineering",
    "Teeth": "LifeSteal",
    "Triceps": "Damage",
}


def get_rarity_from_name(name):
    s = name.split("_")[1]
    return str_to_num_dict[s]


def get_ability_from_name(name):
    s = name.split("_")[0]
    return name_to_ability_dict[s]


def get_value_from_rarity_and_name(rarity, name):
    s = name.split("_")[0]
    if s in "Back, Legs":
        return rarity * 0.03
    elif s in "Brain, Chest, Shoulders":
        return rarity * 1
    elif s in "Skull, Lungs":
        return rarity + 1
    elif s in "Forearms":
        return rarity * 2
    elif s in "Heart":
        return rarity * 3
    elif s in "Nose":
        return rarity * 5
    elif s in "Eyes":
        return rarity * 15
    elif s in "Teeth":
        return rarity / 100
    elif s in "Triceps":
        return 0.05 if rarity == 1 else (4 * rarity / 100)
    elif s in "Reflexes":
        return rarity * 5 / 100
    elif s in "Fingers":
        return (1 + rarity * 2) / 100
    elif s in "Hands":
        return 5 if rarity == 1 else (4 + rarity * 2)


if __name__ == "__main__":
    json_lst = []
    for img_path in filter(
        lambda x: x.endswith(".png") and "200" in x, os.listdir(IMG_PATH)
    ):
        name = img_path[6:].split(".")[0]

        # 切图
        # img = cv.imread(IMG_PATH + img_path, cv.IMREAD_UNCHANGED)
        # img = img[8:63, 8:64]
        # cv.imwrite(IMG_PATH + name, img)

        # 生成数据
        item_dict = {}
        item_dict["Name"] = name  # back_I
        item_dict["Rarity"] = get_rarity_from_name(name)
        item_dict["Ability"] = get_ability_from_name(name)
        item_dict["Value"] = get_value_from_rarity_and_name(item_dict["Rarity"], name)
        item_dict["Path"] = "ArtAssets/Upgrades/" + name

        json_lst.append(item_dict)

    with open("ProcessedUpgrades.json", "w") as f:
        json.dump(json_lst, f)
