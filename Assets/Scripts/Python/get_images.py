import requests
import re
import os
from datetime import datetime
from icecream import ic

ic.configureOutput(
    prefix=lambda: datetime.now().strftime("%y-%m-%d %H:%M:%S | "), includeContext=False
)


def download_brotato_wiki(part):
    print(f"================================================================")
    print(f"=========================={part.center(12, ' ')}==========================")
    print(f"================================================================")
    # 设置保存路径
    path = f"{part}/"
    os.makedirs(path, exist_ok=True)

    # 目标url
    url = f"https://brotato.wiki.spellsandguns.com/{part}"
    # 伪装请求头 防止被反爬
    headers = {
        "User-Agent": "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1",
    }

    # 发送请求 获取响应
    response = requests.get(url, headers=headers)
    # 重新设置编码解决编码问题
    response.encoding = "GBK"

    # 正则匹配提取想要的数据 得到图片链接和名称
    img_info = re.findall('img alt="(.*?)" src="(.*?)" ', response.text)

    for alt, src in img_info:
        img_name = src.split("/")[-1]
        # 加上 'https://brotato.wiki.spellsandguns.com/'才是真正的图片url
        img_url = "https://brotato.wiki.spellsandguns.com/" + src
        img_content = requests.get(img_url, headers=headers).content
        with open(path + img_name, "wb") as f:  # 图片保存到本地
            ic(img_name)
            f.write(img_content)


if __name__ == "__main__":
    parts = [
        "Characters",
        "Stats",
        "Items",
        "Weapons",
        "Enemies",
        "Dangers",
        "Upgrades",
    ]
    for part in parts:
        download_brotato_wiki(part)
