# ConsolePrimeCalculator
App capable of calculating prime numbers (on my PC 10 000 000 prime numbers in 72 s). Created in .NET Core.

## Arguments

##### no arguments
* This will calculate prime numbers on key press
##### primes [positive integer n] [other arguments]
* This will (with no other arguments) calculate n prime numbers and write them into console using '\n' as separator
##### /s [separator]
* Changes the default ('\n') separator
##### /f [path to file]
* This will write the prime numbers into a specified file (creates new file if it doesn't exist)
* This will NOT create the directory in which the file is
* If the file exists it will OVERRIDE all data inside (it will ask you if the file exists)
