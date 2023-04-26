import string
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
        self.dashboardLink = self.driver.find_element(By.LINK_TEXT, "Dashboard")
        self.heroesLink = self.driver.find_element(By.LINK_TEXT, "Heroes")
        self.go_to_dashboard_page()
        
    def go_to_dashboard_page(self):
        self.dashboardLink.click()
        time.sleep(3)

        self.searchbar = self.driver.find_element(By.ID, "search-box")
        self.searchResult = self.driver.find_element(By.CLASS_NAME, "search-result")
    
    def go_to_heroes_page(self):
        self.heroesLink.click()
        time.sleep(3)

        self.newHeroInput = self.driver.find_element(By.ID, "new-hero")
        self.heroesContainer = self.driver.find_element(By.CLASS_NAME, "heroes")
        self.addHeroBtn = self.driver.find_element(By.CLASS_NAME, "add-button")
    
    def get_all_heroes(self) -> list[Hero]:
        self.go_to_heroes_page()

        heroes: list[Hero] = []
        heroList = self.heroesContainer.find_elements(By.TAG_NAME, 'a')
        for hero in heroList:
            heroes.append(Hero(hero.find_element(By.CLASS_NAME, "badge").text, hero.text.replace(hero.find_element(By.CLASS_NAME, "badge").text + " ", "")))
        
        return heroes
    
    def add_hero(self, newHeroName: string):
        self.go_to_heroes_page()
        
        self.newHeroInput.send_keys(newHeroName)
        self.addHeroBtn.click()

    def search_hero(self, wantedHeroName: string) -> bool:
        self.go_to_dashboard_page()
        
        self.searchbar.send_keys(wantedHeroName)
        time.sleep(1)
        try:
            self.searchResult.find_element(By.TAG_NAME, 'a').click()
        except:
            print("It appears that there is no hero with this name. Please check if name is spelled correctly.")
            return False
        return True

    def delete_hero(self, heroName: string) -> bool:
        heroList = self.get_all_heroes()
        for i in range(len(heroList)):
            if heroName == heroList[i].name:
                self.heroesContainer.find_elements(By.CLASS_NAME, "delete")[i].click()
                return True
        return True

    def detailed_hero(self, hero: Hero) -> bool:
        heroList = self.get_all_heroes()
        for i in range(len(heroList)):
            if hero.id == heroList[i].id and hero.name == heroList[i].name:
                self.heroesContainer.find_elements(By.TAG_NAME, 'a')[i].click()
                time.sleep(3)
                return True
        return False
    
    def change_hero_name(self, hero: Hero, newName: string) -> bool:
        isvalid = self.detailed_hero(hero)
        if not isvalid:
            return isvalid
        
        currentName = self.driver.find_element(By.ID, "hero-name")
        saveBtn = self.driver.find_element(By.XPATH, "//button[text()='save']")
        currentName.clear()
        currentName.send_keys(newName)
        saveBtn.click()
        time.sleep(3)

                



if __name__ == '__main__':
    watch = Watchman()
    old = watch.get_all_heroes()
    watch.change_hero_name(old[0], "Ultimator")
    new = watch.get_all_heroes()
    print("test")