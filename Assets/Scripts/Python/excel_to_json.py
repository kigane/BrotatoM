import pandas as pd
import json

EXCEL_PATH = "./Items.xlsx"


def excel_to_json():
    df = pd.read_excel(EXCEL_PATH, header=0)
    df = df.fillna(value={"UnlockedBy": "Unlocked by default"})
    df = df.fillna(0)
    print(df)
    with open("./item_config.json", "w") as f:
        df.transpose().to_json(f)


def json_dict_to_list():
    with open("./item_config.json", "rb") as f:
        tables = json.load(f)

    json_lst = []
    for k, v in tables.items():
        json_lst.append(v)

    with open("./Items.json", "w") as f:
        json.dump(json_lst, f)


if __name__ == "__main__":
    # 读取Excel
    # excel_to_json()
    json_dict_to_list()
    # df = pd.read_json("./ProcessedCharacters.json")
    # df.to_excel("./Charas.xlsx")
    # print(df)
