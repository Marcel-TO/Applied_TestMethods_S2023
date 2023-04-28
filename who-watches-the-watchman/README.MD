# Who watches the watchman?
<p align="center">
<img src="./banner.gif" width="1000" >
</p>

## Assignment
Prepare a comprehensive test that covers the functionality of the <a href="https://files.perry.fyi/hero/" target="_blank">Tour of Heroes Web Application</a>.
Evaluate the functionality of the application and implement a test using <a href="https://www.selenium.dev/" target="_blank">Selenium</a> for browser automation.

The assignment will be graded based on
- Test Coverage
- Code Quality
- Documentation
- Followed Test Practices

## Prepairing the Project in VS Code

### **Prerequisites**
- Visual Studio Code with the python extension
- some important packages

In Python it is common to import external libraries to call important functions. These libraries must be installed before they can be used. By installing Python it is now possible to execute the `pip3 install NAMEOFPACKAGE` or `py -m pip install NAMEOFPACKAGE` command in a terminal to install the needed libraries.

### **Installed Libraries**
- `pip install beautifulsoup4`
- `pip install selenium`
- `pip install webdriver_manager`

## Execution
**Setting up the webdriver:**

To allow the driver to run on multiple OS like windows and IOS it is important to use the webdriver manager `ChromeDriverManager().install`
```py
self.driver = webdriver.Chrome(ChromeDriverManager().install())
```

**Using the Dev Tools:**

Pressing `F12` or `Strg`+`Shift`+`i` allows the user to open the dev tools on any web page and access the html code. Because selenium uses an web driver it has access to the html code as well and generates the possibility to find elements with different properties.

With the help of the combination of finding elements via ID or CLASS....
```py
self.dashboardLink = self.driver.find_element(By.LINK_TEXT, "Dashboard")
```

.... clicking buttons.....
```py
self.addHeroBtn = self.driver.find_element(By.CLASS_NAME, "add-button")
self.addHeroBtn.click()
```

.... and pressing keys.....
```py
self.searchbar = self.driver.find_element(By.ID, "search-box")
self.searchbar.send_keys(wantedHeroName)
```

...it is possible to create any desired web scraper. In this specific case it is used to traverse through the provided web page remotely and tests the code quality.

The following possible interactions are beeing tested:
- get all heroes
- add a hero
- search for a hero
- delete a hero
- show detailed information about a hero
- change the name of a hero