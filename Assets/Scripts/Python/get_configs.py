import pandas as pd
import os


def read_html_table_to_json(part):
    url = f"https://brotato.wiki.spellsandguns.com/{part}"
    if part == "Weapons":
        df1 = pd.read_html(url)[0]
        df2 = pd.read_html(url)[1]
        df = pd.concat([df1, df2])
        df = df.reset_index()
        df.drop(columns=["index"], inplace=True)
        df.transpose().to_json(os.path.curdir + f"/{part}.json")
        return

    table_index = 0
    if part in "Characters, Stats":
        table_index = 1
    df = pd.read_html(url)[table_index]
    # [0]：表示第一个table，多个table需要指定，如果不指定默认第一个
    df.transpose().to_json(os.path.curdir + f"/{part}.json")


if __name__ == "__main__":
    parts = [
        # "Characters",
        # "Items",
        "Weapons",
        # "Enemies",
        # "Dangers",
        # "Stats"
    ]
    for part in parts:
        read_html_table_to_json(part)

    print("获取结束")
