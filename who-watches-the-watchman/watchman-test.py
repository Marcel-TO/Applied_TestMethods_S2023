import string
import unittest
from ddt import ddt, data, file_data, idata, unpack
from watchman import Watchman

@ddt
class WatchmanTest(unittest.TestCase):
    def test_all_heroes(self):
        self.watchman = Watchman()
        heroes = self.watchman.get_all_heroes()
        self.assertTrue(heroes != None)
        self.assertTrue(len(heroes) == 9)

if __name__ == '__main__':
    unittest.main(argv=['first-arg-is-ignored'], exit=False)    
