from bs4 import BeautifulSoup
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
import time
from hero import Hero

class Watchman:
    def __init__(self) -> None:
        self.driver = webdriver.Chrome()
        page = "https://files.perry.fyi/hero/"
        self.driver.get(page)
        self.prepare_elements()
        
    def prepare_elements(self):
        self.dashboardLink = self.driver.find_element(By.LINK_TEXT, "Dashboard")
        self.heroesLink = self.driver.find_element(By.LINK_TEXT, "Heroes")
        self.searchbar = self.driver.find_element(By.ID, "search-box")
        # self.clearMessages = self.driver.find_element(By.CLASS_NAME, "clear")
        # self.heroesLink.click()
        # self.newHeroInput = self.driver.find_element(By.ID, "new-hero")
        # self.heroesContainer = self.driver.find_element(By.CLASS_NAME, "heroes")
        # self.addHeroBtn = self.driver.find_element(By.CLASS_NAME, "add-button")
        # self.dashboardLink.click()
    
    def get_all_heroes(self) -> list[Hero]:
        heroes: list[Hero] = []

        self.heroesLink.click()
        heroList = self.driver.find_element(By.CLASS_NAME, "heroes").find_elements(By.TAG_NAME, 'a')
        for hero in heroList:
            heroes.append(Hero(hero.find_element(By.CLASS_NAME, "badge"), hero.text))
        
        return heroes

    def search_hero(self):
        pass

    def check_current_heroes(self):
        pass

    def add_hero(self):
        pass

    def delete_hero(self):
        pass

    def detailed_hero(self, hero: Hero):
        pass

if __name__ == '__main__':
    watch = Watchman()
    watch.get_all_heroes()