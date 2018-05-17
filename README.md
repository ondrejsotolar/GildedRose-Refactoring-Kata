# GildedRose-Refactoring-Kata

This is my take on the GildedRose-Refactoring-Kata. In the dev process, I've leared some things about refactoring legacy code: 
1. Your unit tests are not good enough - Before I started using the textual Approval test, I've thought that my unit tests cover all test cases. But after running the Approval test, I've realized that I've missed some border cases. I've learned that the textual tests are a good way to capture a legacy code execution result.
2. Strategy pattern is a powerfull one.

Some technical notes:
1. I've left the legacy code intact, so I could run it in paralell with the new impl. 
2. I've not bothered putting every single class into a separate file, as this is such a short kata.
