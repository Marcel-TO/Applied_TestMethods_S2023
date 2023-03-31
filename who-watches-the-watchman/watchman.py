from bs4 import BeautifulSoup
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.service import Service
from webdriver_manager.chrome import ChromeDriverManager

class Watchman:
    def __init__(self) -> None:
        self.driver = webdriver.Chrome(ChromeDriverManager().install())

        page = "https://files.perry.fyi/hero/"
        self.driver.get(page)
        self.soup = BeautifulSoup(self.driver.page_source, 'html.parser')