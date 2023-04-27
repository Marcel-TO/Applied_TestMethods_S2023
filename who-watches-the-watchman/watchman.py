import string
from bs4 import BeautifulSoup
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
from webdriver_manager.chrome import ChromeDriverManager
import time
from hero import Hero

class Watchman:
    def __init__(self) -> None:
        # initialize the webdriver
        self.driver = webdriver.Chrome(ChromeDriverManager().install())
        
        # go to the webpage
        page = "https://files.perry.fyi/hero/"
        self.driver.get(page)

        # save the elements of the navigation links.
        self.dashboardLink = self.driver.find_element(By.LINK_TEXT, "Dashboard")
        self.heroesLink = self.driver.find_element(By.LINK_TEXT, "Heroes")
        self.go_to_dashboard_page()
    

    def close_driver(self):
        # close the driver.
        self.driver.close()


    def go_to_dashboard_page(self):
        # click on the dashboard navigation and wait for the side to load.
        self.dashboardLink.click()
        time.sleep(3)

        # initialize the needed elements of the current page.
        self.searchbar = self.driver.find_element(By.ID, "search-box")
        self.searchResult = self.driver.find_element(By.CLASS_NAME, "search-result")
    

    def go_to_heroes_page(self):
        # click on the heroes navigation and wait for the side to load
        self.heroesLink.click()
        time.sleep(3)

        # initialize the needed elements of the current page.
        self.newHeroInput = self.driver.find_element(By.ID, "new-hero")
        self.heroesContainer = self.driver.find_element(By.CLASS_NAME, "heroes")
        self.addHeroBtn = self.driver.find_element(By.CLASS_NAME, "add-button")
    

    def get_all_heroes(self) -> list[Hero]:
        # go to the heroes page.
        self.go_to_heroes_page()

        # iterate through each hero element and save them as heroes.
        heroes: list[Hero] = []
        heroList = self.heroesContainer.find_elements(By.TAG_NAME, 'a')
        for hero in heroList:
            heroes.append(Hero(hero.find_element(By.CLASS_NAME, "badge").text, hero.text.replace(hero.find_element(By.CLASS_NAME, "badge").text + " ", "")))
        return heroes
    

    def add_hero(self, newHeroName: string):
        # switch to heroes page.
        self.go_to_heroes_page()
        
        # input the name of the new hero and click the add button.
        self.newHeroInput.send_keys(newHeroName)
        self.addHeroBtn.click()


    def search_hero(self, wantedHeroName: string) -> bool:
        # switch to the dashboard page.
        self.go_to_dashboard_page()
        
        # search for a hero and click on the suggestion with the same name if possible.
        self.searchbar.send_keys(wantedHeroName)
        time.sleep(1)
        try:
            self.searchResult.find_element(By.TAG_NAME, 'a').click()
        except:
            print("It appears that there is no hero with this name. Please check if name is spelled correctly.")
            return False
        return True


    def delete_hero(self, heroName: string) -> bool:
        # gets the list of all current heroes.
        heroList = self.get_all_heroes()
        for i in range(len(heroList)):
            # checks if there is a hero with the same name. If yes, click the delete button.
            if heroName == heroList[i].name:
                self.heroesContainer.find_elements(By.CLASS_NAME, "delete")[i].click()
                return True
        return False


    def detailed_hero(self, hero: Hero) -> bool:
        # gets the list of all current heroes.
        heroList = self.get_all_heroes()
        for i in range(len(heroList)):
            # if the selected hero exists, switch to the detailed hero page.
            if hero.id == heroList[i].id and hero.name == heroList[i].name:
                self.heroesContainer.find_elements(By.TAG_NAME, 'a')[i].click()
                time.sleep(3)
                return True
        return False
    

    def change_hero_name(self, hero: Hero, newName: string) -> bool:
        # checks if the selected hero exists.
        isvalid = self.detailed_hero(hero)
        if not isvalid:
            return isvalid
        
        # initializes the needed elements.
        currentName = self.driver.find_element(By.ID, "hero-name")
        saveBtn = self.driver.find_element(By.XPATH, "//button[text()='save']")
        
        # replaces the old name with the current name and saves.
        currentName.clear()
        currentName.send_keys(newName)
        saveBtn.click()
        time.sleep(3)
        return isvalid

if __name__ == '__main__':
    # watch = Watchman()
    # old = watch.get_all_heroes()
    # watch.change_hero_name(old[0], "Ultimator")
    # new = watch.get_all_heroes()
    pass