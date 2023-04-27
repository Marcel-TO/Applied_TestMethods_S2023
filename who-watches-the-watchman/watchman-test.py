import string
import unittest
from ddt import ddt, data, file_data, idata, unpack
from watchman import Watchman
from hero import Hero
import random

@ddt
class WatchmanTest(unittest.TestCase):
    def test_all_heroes(self):
        self.watchman = Watchman()
        heroes = self.watchman.get_all_heroes()
        self.assertTrue(heroes != None)
        self.assertTrue(len(heroes) == 9)
    

    @data({'heroName': 'ImNewHero'},
          {'heroName': 'BestNameEver'})
    @unpack
    def test_add_hero(self, heroName: string):
        self.watchman = Watchman()
        oldHeroes = self.watchman.get_all_heroes()
        self.watchman.add_hero(heroName)
        newHeroes = self.watchman.get_all_heroes()

        self.assertTrue(oldHeroes != None)
        self.assertTrue(len(oldHeroes) == 9)
        self.assertTrue(newHeroes != None)
        self.assertTrue(len(newHeroes) == 10)
        for hero in oldHeroes:
            self.assertTrue(hero.name != heroName)

        isValid = False
        for hero in newHeroes:
            if hero.name == heroName:
                isValid = True
        self.assertTrue(isValid)
    

    @data({'heroName': 'Dr. Nice'},
          {'heroName': 'Dynama'})
    @unpack
    def test_search_hero(self, heroName: string):
        self.watchman = Watchman()
        isFound = self.watchman.search_hero(heroName)
        self.assertTrue(isFound)
    

    @data({'heroName': 'ImNewHero'},
          {'heroName': 'BestNameEver'})
    @unpack
    def test_wrong_search_hero(self, heroName: string):
        self.watchman = Watchman()
        isFound = self.watchman.search_hero(heroName)
        self.assertFalse(isFound)
    

    @data({'heroName': 'Dr. Nice'},
          {'heroName': 'Dynama'})
    @unpack
    def test_delete_hero(self, heroName: string):
        self.watchman = Watchman()
        oldHeroes = self.watchman.get_all_heroes()
        isDeleted = self.watchman.delete_hero(heroName)
        newHeroes = self.watchman.get_all_heroes()
        
        self.assertTrue(isDeleted)
        self.assertTrue(len(oldHeroes) > len(newHeroes))
        for hero in newHeroes:
            self.assertTrue(hero.name != heroName)
    

    @data({'heroName': 'ImNewHero'},
          {'heroName': 'BestNameEver'})
    @unpack
    def test_wrong_delete_hero(self, heroName: string):
        self.watchman = Watchman()
        oldHeroes = self.watchman.get_all_heroes()
        isDeleted = self.watchman.delete_hero(heroName)
        newHeroes = self.watchman.get_all_heroes()
        self.assertFalse(isDeleted)
        self.assertTrue(len(oldHeroes) == len(newHeroes))

    def test_detailed_hero(self):
        self.watchman = Watchman()
        heroes = self.watchman.get_all_heroes()
        
        for hero in heroes:
            isDetailed = self.watchman.detailed_hero(hero)
            self.assertTrue(isDetailed)
        self.watchman.close_driver()


    @data({'hero': Hero(99,'ImNewHero')},
          {'hero': Hero(0, 'BestNameEver')})
    @unpack
    def test_wrong_detailed_hero(self, hero: Hero):
        self.watchman = Watchman()        
        isDetailed = self.watchman.detailed_hero(hero)
        self.assertFalse(isDetailed)  
        self.watchman.close_driver()

    @data({'newHeroName': 'EinSchnabelTier?'},
          {'newHeroName': 'NeinPerryDasSchnabelTier'})
    @unpack
    def test_change_hero_name(self, newHeroName: string):
        self.watchman = Watchman()
        oldHeroes = self.watchman.get_all_heroes()
        index = random.randint(0, len(oldHeroes) - 1)
        isChanged = self.watchman.change_hero_name(oldHeroes[index], newHeroName)
        newHeroes = self.watchman.get_all_heroes()
        
        self.assertTrue(isChanged)
        isValid = False
        for hero in newHeroes:
            if hero.name == newHeroName:
                isValid = True
        self.assertTrue(isValid)
        self.watchman.close_driver()
    

    @data({'hero': Hero(99,'ImNewHero'), 'newHeroName': 'EinSchnabelTier?'},
          {'hero': Hero(0, 'BestNameEver'), 'newHeroName': 'NeinPerryDasSchnabelTier'})
    @unpack
    def test_wrong_change_hero_name(self, hero, newHeroName: string):
        self.watchman = Watchman()
        isChanged = self.watchman.change_hero_name(hero, newHeroName)
        heroes = self.watchman.get_all_heroes()
        self.assertFalse(isChanged)     

        for h in heroes:
            self.assertTrue(h.name != newHeroName)
        self.watchman.close_driver()

if __name__ == '__main__':
    unittest.main(argv=['first-arg-is-ignored'], exit=False)    
